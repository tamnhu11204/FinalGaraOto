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
                this.WindowState= WindowState.Normal;
            }
            else
            {
                this.WindowState= WindowState.Maximized;
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
            LoadDataGridLSNH();
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
            if (lichsukinhdoanh.TabIndex==0)
            {
                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Year).ToList();

                foreach (var item in List)
                {


                    NamCb.Items.Add(item);
                }
            }
            if (lichsunhaphang.TabIndex==0)
            {

                var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Year).ToList();

                foreach (var item in List)
                {


                    NamCb.Items.Add(item);
                }
            }
        }

        void LoadComboBoxThangBaoCao()
        {
            if (lichsukinhdoanh.IsSelected== true)
            {
                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Month).ToList();

                foreach (var item in List)
                {


                    ThangCb.Items.Add(item);
                }
            }
             if(lichsunhaphang.IsSelected)
            {
                var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Month).ToList();
                foreach (var item in List)
                {

                    ThangCb.Items.Add(item);
                }
            }

            /*ThangCb.ItemsSource = data;*/

        }

        void LoadDataGridLSKD()
        {
            ObservableCollection<LichSuKD> kinhdoanh = new ObservableCollection<LichSuKD>();
            var List = DataProvider.Ins.DB.PHIEUTHUTIENs.ToList();
            foreach (var item in List)
            {
                LichSuKD kinhdoanh1 = new LichSuKD();
                kinhdoanh1.Mahoadon = item.MaPhieuThuTien;
                var makh = DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan == item.MaTiepNhan).Select(x => x.XE.MaChuXe).First();
                kinhdoanh1.Khachhang= DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe== makh).Select(x => x.TenChuXe).First();
                kinhdoanh1.Biensoxe = DataProvider.Ins.DB.XEs.Where(x => x.MaTiepNhan== item.MaTiepNhan).Select(x => x.BienSoXe).First();
                kinhdoanh1.Ngaythanhtoan= DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaPhieuThuTien == item.MaPhieuThuTien).Select(x => x.NgayThuTien).First();
                kinhdoanh1.Doanhthu= DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaPhieuThuTien== item.MaPhieuThuTien).Select(x => x.SoTienThu).First();
                kinhdoanh.Add(kinhdoanh1);
                if ((NamCb.Text== Convert.ToString(kinhdoanh1.Ngaythanhtoan.Year)) && (ThangCb.Text== Convert.ToString(kinhdoanh1.Ngaythanhtoan.Month)))
                {
                    LichSuKinhDoanh.ItemsSource=kinhdoanh;
                }
            }

        }


        void LoadDataGridLSNH()
        {
            ObservableCollection<LichSuNH> nhaphang = new ObservableCollection<LichSuNH>();
            var List = DataProvider.Ins.DB.PHIEUNHAPs.ToList();
            foreach (var item in List)
            {
                LichSuNH nhaphang1 = new LichSuNH();
                var mavtpt = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == item.MaNhapHang).Select(x => x.MaVatTuPhuTung).First();
                nhaphang1.Madonnhap= item.MaNhapHang;
                nhaphang1.Vattu = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung== mavtpt).Select(x => x.TenVTPT).First();
                nhaphang1.Soluong= (int)DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == item.MaNhapHang).Select(x => x.SoLuong).First();
                nhaphang1.Nhacungcap= DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap== item.MaNhaCungCap).Select(x => x.MaNhaCungCap).First();
                nhaphang1.Dongia = (decimal)DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang== item.MaNhapHang).Select(x => x.GiaNhap).First();
                nhaphang1.Ngaynhaphang= Convert.ToDateTime(item.NgayNhapHang);
                nhaphang1.Tongchiphi= (decimal)DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang== item.MaNhapHang).Select(x => x.ThanhTien).First();
                nhaphang.Add(nhaphang1);
                if (((NamCb.Text== Convert.ToString(nhaphang1.Ngaynhaphang.Year)) && (ThangCb.Text== Convert.ToString(nhaphang1.Ngaynhaphang.Month))))
                {
                   Lichsunhaphang.ItemsSource=nhaphang;
                }
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

        public class LichSuNH
        {
            public string Vattu {  get; set; }
            public int Soluong { get; set; }
            public decimal Dongia { get; set; }
            public int Nhacungcap {  get; set; }
            public DateTime Ngaynhaphang { get; set; }
            public int Madonnhap { get; set; }
            public decimal Tongchiphi { get; set; }
        }
    }
}
