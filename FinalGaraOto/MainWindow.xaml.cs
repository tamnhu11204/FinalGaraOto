using FinalGaraOto.Model;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FinalGaraOto.DangNhap;



namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string tenDN)
        {
            InitializeComponent();
            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tenDN).SingleOrDefault();

            txbTenDangNhap.Text = l.TenDangNhap;
            txbCCCD.Text = l.CCCDNguoiDung;
            txbDiaChi.Text = l.DiaChiNguoiDung;
            txbHoVaTen.Text = l.TenNguoiDung;
            txbMatKhau.Text = l.MatKhau;
            txbSDT.Text = l.SDTNguoiDung;
            dpNgaySinh.Text = l.NgaySinhNguoiDung.ToString();

            LoadPhanQuyen();
        }

        #region scroll button
        private void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
            if ( result == MessageBoxResult.Yes)
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
            ThongKe thongke_tab = new ThongKe(txbTenDangNhap.Text);
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow(txbTenDangNhap.Text);
            taikhoan_tab.Show();
            this.Close();
        }

        private void dichVu_Tab(object sender, RoutedEventArgs e)
        {
            DichVu dichvu_tab = new DichVu(txbTenDangNhap.Text);
            dichvu_tab.Show();
            this.Close();
        }

        private void nhanVien_Tab(object sender, RoutedEventArgs e)
        {
            NhanVien nhanvien_tab = new NhanVien(txbTenDangNhap.Text);
            nhanvien_tab.Show();
            this.Close();
        }

        private void khoHang_Tab(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung khohang_tab = new VatTuPhuTung(txbTenDangNhap.Text);
            khohang_tab.Show();
            this.Close();
        }

        private void tuyChon_Tab(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_tab = new TuyChon(txbTenDangNhap.Text);
            tuychon_tab.Show();
            this.Close();
        }

        #endregion

        private void BtnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbCCCD.Text) || string.IsNullOrEmpty(txbDiaChi.Text) || string.IsNullOrEmpty(txbHoVaTen.Text)
                    || string.IsNullOrEmpty(txbMatKhau.Text) || string.IsNullOrEmpty(dpNgaySinh.Text) || string.IsNullOrEmpty(txbSDT.Text)
                    || string.IsNullOrEmpty(txbTenDangNhap.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                string tdn = txbTenDangNhap.Text;
                var n = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tdn).SingleOrDefault();
                n.MaNguoiDung = n.MaNguoiDung;
                n.TenDangNhap = txbTenDangNhap.Text;
                n.TenNguoiDung = txbHoVaTen.Text;

                DateTime? ngaySinh = dpNgaySinh.SelectedDate;
                if (ngaySinh.HasValue)
                {
                    n.NgaySinhNguoiDung = ngaySinh.Value;
                }
                else
                {
                    return;
                }

                n.CCCDNguoiDung = txbCCCD.Text;
                n.DiaChiNguoiDung = txbDiaChi.Text;
                n.SDTNguoiDung = txbSDT.Text;
                n.MatKhau = txbMatKhau.Text;

                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật thông tin cá nhân?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else return;

            }
            return;
        }

        void LoadPhanQuyen()
        {
            string tdn = txbTenDangNhap.Text;
            var n = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tdn).SingleOrDefault();
            int MaND = n.MaNhom;
            if (MaND == 1)
            {
                ck1.IsChecked = true;
                ck2.IsChecked = true;
                ck3.IsChecked = true;
                ck4.IsChecked = true;
                ck5.IsChecked = true;
                ck6.IsChecked = true;
                ck7.IsChecked = true;
                ck8.IsChecked = true;
                ck9.IsChecked = true;
                ck10.IsChecked = true;
                ck11.IsChecked = true;
            }
            else
            {
                ck1.IsChecked = true;
                ck2.IsChecked = true;
                ck3.IsChecked = true;
                ck4.IsChecked = false;
                ck5.IsChecked = false;
                ck6.IsChecked = true;
                ck7.IsChecked = true;
                ck8.IsChecked = true;
                ck9.IsChecked = true;
                ck10.IsChecked = true;
                ck11.IsChecked = true;
            }
        }

        private void BtnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            DangNhap dangnhap=new DangNhap();
            dangnhap.Show();
            this.Close();
        }
    }
}
