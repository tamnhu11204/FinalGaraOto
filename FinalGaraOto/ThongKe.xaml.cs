using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinalGaraOto.Action;
using FinalGaraOto.Model;
using static FinalGaraOto.DichVu;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        public ThongKe(string n)
        {
            InitializeComponent();
            LoadComboBoxNamBaoCao();
            LoadComboBoxThangBaoCao();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;
        }

        #region scroll bar button
        public void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region button tab

        private void thongKe_Tab(object sender, RoutedEventArgs e)
        {
            ThongKe thongke_tab = new ThongKe(tbUserName.Text);
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow(tbUserName.Text);
            taikhoan_tab.Show();
            this.Close();
        }

        private void dichVu_Tab(object sender, RoutedEventArgs e)
        {
            DichVu dichvu_tab = new DichVu(tbUserName.Text);
            dichvu_tab.Show();
            this.Close();
        }

        private void nhanVien_Tab(object sender, RoutedEventArgs e)
        {
            NhanVien nhanvien_tab = new NhanVien(tbUserName.Text);
            nhanvien_tab.Show();
            this.Close();
        }

        private void khoHang_Tab(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung khohang_tab = new VatTuPhuTung(tbUserName.Text);
            khohang_tab.Show();
            this.Close();
        }

        private void tuyChon_Tab(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_tab = new TuyChon(tbUserName.Text);
            tuychon_tab.Show();
            this.Close();
        }
        #endregion


        private void Btn_Xuat_Click(object sender, RoutedEventArgs e)
        {

            LoadDataGridLSKD();
            
        }

        private void Doanhthu_Click(object sender, RoutedEventArgs e)
        {
            BCDoanhThu Child = new BCDoanhThu(tbUserName.Text);
            Child.Show();
            this.Close();
        }

        private void Hangton_Click(object sender, RoutedEventArgs e)
        {
            BCTon Child = new BCTon(tbUserName.Text);
            Child.Show();
            this.Close();
        }

        void LoadComboBoxNamBaoCao()
        {
            if (lichsukinhdoanh.TabIndex == 0)
            {
                HashSet<int> uniqueYears = new HashSet<int>();

                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Year).ToList();
                foreach (var item in List)
                {
                    if (!uniqueYears.Contains(item))
                    {
                        uniqueYears.Add(item);
                        Cb_Nam.Items.Add(item);
                    }
                }
            }
           
            
        }

        void LoadComboBoxThangBaoCao()
        {
            if (lichsukinhdoanh.TabIndex == 0)
            {
                HashSet<int> uniqueYears = new HashSet<int>();

                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Month).ToList();
                foreach (var item in List)
                {
                    if (!uniqueYears.Contains(item))
                    {
                        uniqueYears.Add(item);
                        Cb_Thang.Items.Add(item);
                    }
                }
            }
           
            

            /*Cb_Thang.ItemsSource = data;*/

        }

        void LoadDataGridLSKD()
        {
            ObservableCollection<LichSuKD> kinhdoanh = new ObservableCollection<LichSuKD>();
            var List = DataProvider.Ins.DB.PHIEUTHUTIENs.ToList();
            foreach (var item in List)
            {
                LichSuKD kinhdoanh1 = new LichSuKD();
                kinhdoanh1.Mahoadon= item.MaPhieuThuTien;
                kinhdoanh1.Khachhang= item.XE.CHUXE.TenChuXe;
                kinhdoanh1.Biensoxe= item.XE.BienSoXe;
                kinhdoanh1.Ngaythanhtoan= item.NgayThuTien;
                kinhdoanh1.Doanhthu= item.SoTienThu;
               
                if ((Cb_Nam.Text == Convert.ToString(kinhdoanh1.Ngaythanhtoan.Year)) && (Cb_Thang.Text == Convert.ToString(kinhdoanh1.Ngaythanhtoan.Month)))
                {

                    kinhdoanh.Add(kinhdoanh1);
                }
                Dg_LichSuKinhDoanh.ItemsSource = kinhdoanh;

            }
        }


       

        public class LichSuKD
        {
            public int Mahoadon { get; set; }
            public string Khachhang { get; set; }
            public string Biensoxe { get; set; }

            public DateTime Ngaythanhtoan { get; set; }
            public decimal Doanhthu { get; set; }

        }

        private void Hanghoa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}