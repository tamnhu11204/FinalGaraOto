using FinalGaraOto.Model;
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
using static System.Net.Mime.MediaTypeNames;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for LichSuNhapVatTuPhuTung.xaml
    /// </summary>
    public partial class LichSuNhapVatTuPhuTung : Window
    {
        public LichSuNhapVatTuPhuTung(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;
            LoadLS();
            LoadDonViCC();

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

        void LoadLS()
        {
            ObservableCollection<NhapHang> nh = new ObservableCollection<NhapHang>();
            var List = DataProvider.Ins.DB.PHIEUNHAPs.ToList();
            foreach (var item in List)
            {
                NhapHang nh1 = new NhapHang();
                nh1.MaNH = item.MaNhapHang;
                nh1.NgayNH = item.NgayNhapHang;
                var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == item.MaNhaCungCap).SingleOrDefault();
                nh1.DonViCC = ncc.TenNhaCungCap;
                nh1.TongTien = item.TongTienNhapHang;
                nh.Add(nh1);
                dtgLS.ItemsSource = nh;
            }
        }
        void LoadDonViCC()
        {
            var List = DataProvider.Ins.DB.NHACUNGCAPs.Select(x => x.TenNhaCungCap).ToList();
            foreach (var item in List)
            {
                cbbDonViCC.Items.Add(item);
            }
        }
        public class NhapHang
        {
            public int MaNH { get; set; }
            public DateTime? NgayNH { get; set; }
            public string DonViCC { get; set; }
            public decimal? TongTien { get; set; }
        }

        private void dpNgayNH_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<NhapHang> h = new ObservableCollection<NhapHang>();
            DateTime? dt = dpNgayNH.SelectedDate;

            var List = DataProvider.Ins.DB.PHIEUNHAPs.ToList();
            foreach (var item in List)
            {
                if (dt == item.NgayNhapHang)
                {
                    NhapHang nh1 = new NhapHang();
                    nh1.MaNH = item.MaNhapHang;
                    nh1.NgayNH = item.NgayNhapHang;
                    var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == item.MaNhaCungCap).SingleOrDefault();
                    nh1.DonViCC = ncc.TenNhaCungCap;
                    nh1.TongTien = item.TongTienNhapHang;
                    h.Add(nh1);
                    dtgLS.ItemsSource = h;
                }
            }
        }

        private void cbbDonViCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<NhapHang> h = new ObservableCollection<NhapHang>();
            string ncc = cbbDonViCC.Text;

            var List = DataProvider.Ins.DB.PHIEUNHAPs.ToList();
            foreach (var item in List)
            {
                var ncc1 = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == item.MaNhaCungCap).SingleOrDefault();
                string text = ncc1.TenNhaCungCap;
                if (ncc.Contains(text))
                {
                    NhapHang nh1 = new NhapHang();
                    nh1.MaNH = item.MaNhapHang;
                    nh1.NgayNH = item.NgayNhapHang;
                    nh1.DonViCC = cbbDonViCC.Text;
                    nh1.TongTien = item.TongTienNhapHang;
                    h.Add(nh1);
                    dtgLS.ItemsSource = h;
                }
            }
        }

        private void BtnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            dpNgayNH.Text = "";
            cbbDonViCC.Text = "";
            LoadDonViCC();
        }
    }
}