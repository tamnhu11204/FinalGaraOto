using FinalGaraOto.Model;
using MaterialDesignThemes.Wpf;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HoaDonThanhToanPhuTung.xaml
    /// </summary>
    /// 
    
    public partial class HoaDonThanhToanPhuTung : Window
    {

        public HoaDonThanhToanPhuTung(string n, string MaHDPT)
        {
            InitializeComponent();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;

            tbMa.Text = MaHDPT;
            //MessageBox.Show(tbMa.Text);
            LoadChiTietThanhToanPhuTung();
            LoadThongTinNCC();
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

        public void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        public void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        #region chi tiet thanh toan phu tung

        void LoadThongTinNCC()
        {
            int mancc = 0;
            int _ma = int.Parse(tbMa.Text);
            //var maNH = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == ).SingleOrDefault();
            var maNCC = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhapHang == _ma).SingleOrDefault();
            if (maNCC != null)
            {
                mancc = maNCC.MaNhaCungCap;

                if (mancc != 0)
                {
                    var ma = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == mancc).SingleOrDefault();

                    txbTenNCC.Text = ma.TenNhaCungCap;
                    txbDiaChi.Text = ma.DiachiNhaCungCap;
                    txbEmail.Text = ma.EmailNhaCungCap;
                    txbSDT.Text = ma.SDTNhaCungCap;

                }
            }
            dtpNgayNhapHang.Text = maNCC.NgayNhapHang.ToString();



        }

        public class ChiTietNhapVatTuPhuTungs //Khong can cung duoc, tai co Class san ben EntityFramework
        {
            public int STT { get; set; }
            public int MaVTPT { get; set; }
            public string TenVT { get; set; }
            public Nullable<int> SL { get; set; }
            public int MaNhapHang { get; set; }
            public Nullable<decimal> GiaNhap { get; set; }
            public Nullable<decimal> ThanhTien { get; set; }
        }

        void LoadChiTietThanhToanPhuTung()
        {
            int _ma = int.Parse(tbMa.Text);
            ObservableCollection<ChiTietNhapVatTuPhuTungs> ChiTiets = new ObservableCollection<ChiTietNhapVatTuPhuTungs>();
            var List = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == _ma).ToList();
            int i = 1;
            foreach (var item in List)
            {
                ChiTietNhapVatTuPhuTungs ct1 = new ChiTietNhapVatTuPhuTungs();
                ct1.STT = i;
                ct1.MaVTPT = item.MaVatTuPhuTung;
                ct1.MaNhapHang = item.MaNhapHang;
                ct1.TenVT = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == item.MaVatTuPhuTung).Select(x => x.TenVTPT).First();
                ct1.SL = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.SoLuong == item.SoLuong).Select(x => x.SoLuong).First();
                ct1.GiaNhap = item.GiaNhap;
                ct1.ThanhTien = item.ThanhTien;



            }
        }

        #endregion

    }
}
