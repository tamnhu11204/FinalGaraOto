﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for VatTuPhuTung.xaml
    /// </summary>
    public partial class VatTuPhuTung : Window
    {
        public VatTuPhuTung vatTuPhuTung;
        public VatTuPhuTung()
        {
            InitializeComponent();
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
            ThongKe thongke_tab = new ThongKe();
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow(null);
            taikhoan_tab.Show();
            this.Close();
        }

        private void dichVu_Tab(object sender, RoutedEventArgs e)
        {
            DichVu dichvu_tab = new DichVu();
            dichvu_tab.Show();
            this.Close();
        }

        private void nhanVien_Tab(object sender, RoutedEventArgs e)
        {
            NhanVien nhanvien_tab = new NhanVien();
            nhanvien_tab.Show();
            this.Close();
        }

        private void khoHang_Tab(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung khohang_tab = new VatTuPhuTung();
            khohang_tab.Show();
            this.Close();
        }

        private void tuyChon_Tab(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_tab = new TuyChon();
            tuychon_tab.Show();
            this.Close();
        }




        #endregion


        #region popUp chuyen tab
        private void btnNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            PhieuNhapVatTuPhuTung phieuNhapVatTuPhuTung = new PhieuNhapVatTuPhuTung();
            phieuNhapVatTuPhuTung.Visibility=Visibility.Visible;
        }

        private void btnLichSuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
            LichSuNhapVatTuPhuTung lichSuNhapVatTuPhuTung = new LichSuNhapVatTuPhuTung();
            lichSuNhapVatTuPhuTung.Visibility = Visibility.Visible;
        }

        private void btnDVT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ThemDonVi themDonVi = new ThemDonVi();
            themDonVi.Visibility=Visibility.Visible;
        }

        #endregion

        #region btn xu ly 
        private void btnThemVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ThemVatTuPhuTung themVatTuPhuTung = new ThemVatTuPhuTung();
            themVatTuPhuTung.Visibility =(Visibility) Visibility.Visible;
        }

        #endregion
    }
}
