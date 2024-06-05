using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for BCTon.xaml
    /// </summary>
    public partial class BCTon : Window
    {

        public BCTon(string n)
        {
            InitializeComponent();

            LoadComboBoxNamBaoCao();
            LoadComboBoxThangBaoCao();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;
        }

        #region scroll bar button
        private void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        private void LoadComboBoxNamBaoCao()
        {
            HashSet<int> uniqueYears = new HashSet<int>();

            var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Year).ToList();
            foreach (var item in List)
            {
                if (!uniqueYears.Contains(item))
                {
                    uniqueYears.Add(item);
                    Cb_Nam.Items.Add(item);
                }
            }
        }

        void LoadComboBoxThangBaoCao()
        {
            HashSet<int> uniqueYears = new HashSet<int>();

            var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Month).ToList();
            foreach (var item in List)
            {
                if (!uniqueYears.Contains(item))
                {
                    uniqueYears.Add(item);
                    Cb_Thang.Items.Add(item);
                }
            }
        }

        private void Bnt_xembc_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<BaoCaoTon> bcton = new ObservableCollection<BaoCaoTon>();

            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.ToList();

            foreach (var item in List)
            {
                BaoCaoTon baocaoton1 = new BaoCaoTon();
                baocaoton1.mavtpt= item.MaVatTuPhuTung;
                baocaoton1.tenvtpt= item.TenVTPT;
                if (item.DonGiaNhap!= null)
                    baocaoton1.dongia= Convert.ToDecimal(item.DonGiaNhap);
                else
                    baocaoton1.dongia=0;
                bcton.Add(baocaoton1);
            }
            foreach (var item in bcton)
            {
                int _t1 = Convert.ToInt32(Cb_Thang.Text);
                int _t2 = Convert.ToInt32(Cb_Nam.Text);
                var sl = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung== item.mavtpt  && x.PHIEUNHAP.NgayNhapHang.Value.Month== _t1
                    && x.PHIEUNHAP.NgayNhapHang.Value.Year== _t2).ToList();
                int soluongnhap = 0;
                decimal tiennhap = 0;

                if (sl!=null)
                {
                    foreach (var i in sl)
                    {
                        int a = Convert.ToInt32(i.SoLuong);

                        soluongnhap+= a;

                        decimal t2 = Convert.ToDecimal(i.ThanhTien);
                        tiennhap+= t2;
                    }

                }
                item.soluong= soluongnhap;

                item.tongtien= tiennhap;
            }
            Dg_BCTon.ItemsSource= bcton;
        }

        public class BaoCaoTon
        { 
           public int mavtpt {  get; set; }
            public string tenvtpt { get; set; }
            public decimal dongia { get; set; }
            public int soluong { get; set; }
            
            public decimal tongtien { get; set; }
        }

        private void Bnt_dong_Click(object sender, RoutedEventArgs e)
        {
            ThongKe child= new ThongKe(tbUserName.Text);
            child.Show();
            this.Close();
        }

        private void Bnt_xuatfilebc_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel_BCTon export = new ExportToExcel_BCTon(Dg_BCTon, DateTime.Now);
        }

        private void Bnt_bcdoanhthu_Click(object sender, RoutedEventArgs e)
        {
            BCDoanhThu bCDT = new BCDoanhThu(tbUserName.Text);
            bCDT.Show();
            this.Close();
        }
    }

}
