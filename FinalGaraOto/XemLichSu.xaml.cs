﻿using FinalGaraOto.Model;
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
using static FinalGaraOto.ChiTietPhieuNhapVTPT;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for XemLichSu.xaml
    /// </summary>
    public partial class XemLichSu : Window
    {
        public XemLichSu(string n, int MaNH)
        {
            InitializeComponent();
            tbUserName.Text = n;
            tbMa.Text = MaNH.ToString();
            LoadTongTien();
            LoadLichSuNhapVatTuPhuTungList();


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
        void LoadLichSuNhapVatTuPhuTungList() //Hien thi nhan vien len datagrid
        {
            int Ma1 = int.Parse(tbMa.Text);
            ObservableCollection<ChiTietNhapVatTuPhuTungs> chiTietNhapVatTuPhuTungs = new ObservableCollection<ChiTietNhapVatTuPhuTungs>();
            var List = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == Ma1).ToList();
            foreach (var item in List)
            {
                ChiTietNhapVatTuPhuTungs chiTietNhapVatTuPhuTungs1 = new ChiTietNhapVatTuPhuTungs();
                chiTietNhapVatTuPhuTungs1.MaVTPT = item.MaVatTuPhuTung;
                chiTietNhapVatTuPhuTungs1.TenVT = item.VATTUPHUTUNG.TenVTPT;
                chiTietNhapVatTuPhuTungs1.GiaNhap = item.GiaNhap;
                chiTietNhapVatTuPhuTungs1.SL = item.SoLuong;
                chiTietNhapVatTuPhuTungs.Add(chiTietNhapVatTuPhuTungs1);
                dtgLSNVTPT.ItemsSource = chiTietNhapVatTuPhuTungs;
            }
        }

        public class ChiTietNhapVatTuPhuTungs //Khong can cung duoc, tai co Class san ben EntityFramework
        {
            public int MaVTPT { get; set; }
            public string TenVT { get; set; }
            public Nullable<int> SL { get; set; }

            public Nullable<decimal> GiaNhap { get; set; }
            public Nullable<decimal> ThanhTien { get; set; }
        }

        void LoadTongTien()
        {
            int Maa = int.Parse(tbMa.Text);
            var m1 = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhapHang == Maa).SingleOrDefault();
            txbTongTienNhapHang.Text = m1.TongTienNhapHang.ToString();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            LichSuNhapVatTuPhuTung l=new LichSuNhapVatTuPhuTung(tbUserName.Text);
            l.Show();
            this.Close();
        }
    }
}
