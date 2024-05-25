using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Window
    {
        public NhanVien(string n)
        {
            InitializeComponent();
            LoadNhanVienList();
            LoadChucVu();
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

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion 
        void LoadNhanVienList() //Hien thi nhan vien len datagrid
        {
            ObservableCollection<NhanViens> nhanViens = new ObservableCollection<NhanViens>();
            var List = DataProvider.Ins.DB.NGUOIDUNGs.ToList();
            foreach (var item in List)
            {
                NhanViens nhanViens1 = new NhanViens();
                nhanViens1.Ma = item.MaNguoiDung;
                nhanViens1.HoVaTen = item.TenNguoiDung;
                nhanViens1.ChucVu = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Where(x => x.MaNhom == item.MaNhom).Select(x => x.TenNhom).First().ToString();
                nhanViens.Add(nhanViens1);
                dtgNhanVien.ItemsSource = nhanViens;
            }
           

        }


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


        #region NhanVien
        private void Btn_ThemNhanVien(object sender, RoutedEventArgs e) //chuyen sang window ThemNhanVien
        {
            ThemNhanVien themNhanVien = new ThemNhanVien(tbUserName.Text);
            themNhanVien.ShowDialog();
            this.Close();
        }

        private void dtgNhanVien_SelectionChanged_1(object sender, SelectionChangedEventArgs e) //Khi chon 1 hang thi se hien thi sang Groupbox Thong tin chi tiet
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                int Ma = selected_row.Ma;
                txbMa.Text = selected_row.Ma.ToString();
                var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaNguoiDung == Ma).SingleOrDefault();
                txbHoVaTen.Text = selected_row.HoVaTen;
                txbNgaySinh.Text = l.NgaySinhNguoiDung.ToString();
                txbCCCD.Text = l.CCCDNguoiDung;
                txbDiaChi.Text = l.DiaChiNguoiDung;
                txbSDT.Text = l.SDTNguoiDung;
                txbTenDangNhap.Text = l.TenDangNhap;
                txbMatKhau.Text = l.MatKhau;
            }
        }

       

        private void BtnCapNhap_Click(object sender, RoutedEventArgs e) //cap nhat thong tin nhan vien
        {
            if (string.IsNullOrEmpty(txbCCCD.Text) || string.IsNullOrEmpty(txbDiaChi.Text) || string.IsNullOrEmpty(txbHoVaTen.Text)
                    || string.IsNullOrEmpty(txbMatKhau.Text) || string.IsNullOrEmpty(txbNgaySinh.Text) || string.IsNullOrEmpty(txbSDT.Text)
                    || string.IsNullOrEmpty(txbTenDangNhap.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                int MaNV = int.Parse(txbMa.Text);
                var n = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaNguoiDung == MaNV).SingleOrDefault();

                n.MaNguoiDung = MaNV;
                n.TenNguoiDung = txbHoVaTen.Text;

                DateTime? ngaySinh = txbNgaySinh.SelectedDate;
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
                n.TenDangNhap = txbTenDangNhap.Text;
                n.MatKhau = txbMatKhau.Text;

                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật thông tin nhân viên?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                    LoadNhanVienList();
                }
                else return;

            }
            return;
        }


        private void BtnXoa_Click(object sender, RoutedEventArgs e) //xoa nhan vien
        {
            int MaNV = int.Parse(txbMa.Text);
            var n = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaNguoiDung == MaNV).SingleOrDefault();
            if (n != null)
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.NGUOIDUNGs.Remove(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadNhanVienList();
                }
                else return;
            }
        }

        private void txbTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<NhanViens> nhanViens = new ObservableCollection<NhanViens>();
            string text = txbTimKiem.Text.ToLower();

            var List = DataProvider.Ins.DB.NGUOIDUNGs.ToList();
            foreach (var item in List)
            {
                string _tenND = item.TenNguoiDung.ToLower();
                if (_tenND.Contains(text))
                {
                    NhanViens nhanViens1 = new NhanViens();
                    nhanViens1.Ma = item.MaNguoiDung;
                    nhanViens1.HoVaTen = item.TenNguoiDung;
                    nhanViens1.ChucVu = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Where(x => x.MaNhom == item.MaNhom).Select(x => x.TenNhom).First().ToString();
                    nhanViens.Add(nhanViens1);
                }
            }
            dtgNhanVien.ItemsSource = nhanViens;
        }
        #endregion

        #region ChucVu
        void LoadChucVu() //Hien thi chucvu len datagrid
        {
            ObservableCollection<NHOMNGUOIDUNG> nnd = new ObservableCollection<NHOMNGUOIDUNG>();
            var List = DataProvider.Ins.DB.NHOMNGUOIDUNGs.ToList();
            foreach (var item in List)
            {
                NHOMNGUOIDUNG nnds = new NHOMNGUOIDUNG();
                nnds.MaNhom = item.MaNhom;
                nnds.TenNhom = item.TenNhom;
                nnd.Add(nnds);
                dtgChucVu.ItemsSource = nnd;
            }
        }

        private void dtgChucVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                int Ma = selected_row.MaNhom;
                var l = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Where(x => x.MaNhom == Ma).SingleOrDefault();
                txbChucVuNV.Text=l.TenNhom.ToString();
                if (Ma == 1)
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
        }
        #endregion
    }
    public class NhanViens //Khong can cung duoc, tai co Class san ben EntityFramework
    {
        public int Ma {  get; set; }
        public string HoVaTen { get; set; }
        public string ChucVu { get; set; }
    }
}
