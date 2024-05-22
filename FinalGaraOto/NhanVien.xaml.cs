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
        public NhanVien()
        {
            InitializeComponent();
            LoadNhanVienList();
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
            ThongKe thongke_tab = new ThongKe();
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow();
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

        private void Btn_ThemNhanVien(object sender, RoutedEventArgs e)
        {
            ThemNhanVien themNhanVien = new ThemNhanVien();
            themNhanVien.ShowDialog();
            this.Close();
        }

        private void dtgNhanVien_SelectionChanged_1(object sender, SelectionChangedEventArgs e) //Khi chon 1 hang thi se hien thi sang Groupbox Thong tin chi tiet
        {
            if (dtgNhanVien.SelectedIndex.ToString() != null)
            {
                if ((DataRowView)dtgNhanVien.SelectedItem != null)
                {
                    int Ma = int.Parse(((DataRowView)dtgNhanVien.SelectedItem)[0].ToString());
                    var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaNguoiDung == Ma).FirstOrDefault();
                    txbHoVaTen.Text = l.TenNguoiDung;
                    txbNgaySinh.Text = l.NgaySinhNguoiDung.ToString();
                    txbCCCD.Text = l.CCCDNguoiDung;
                    txbDiaChi.Text = l.DiaChiNguoiDung;
                    txbSDT.Text = l.SDTNguoiDung;
                    txbTenDangNhap.Text = l.TenDangNhap;
                    txbMatKhau.Text = l.MatKhau;
                }
            }
        }

        private void BtnCapNhap_Click(object sender, RoutedEventArgs e)
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

                string _TenDangNhap = txbTenDangNhap.Text;
                var TaiKhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == _TenDangNhap).Count();
                if (TaiKhoan > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Hãy dùng tên khác.");
                }
                else
                {
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
                        NhanVien nhanVien = new NhanVien();
                        nhanVien.Show();
                    }
                    else return;
                }
            }
            return;
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
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
                    NhanVien nhanVien = new NhanVien();
                    this.Close();
                    nhanVien.Show();
                }
                else return;
            }
        }
    }
    public class NhanViens //Khong can cung duoc, tai co Class san ben EntityFramework
    {
        public int Ma {  get; set; }
        public string HoVaTen { get; set; }
        public string ChucVu { get; set; }
    }
}
