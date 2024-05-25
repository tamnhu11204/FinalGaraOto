using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        
        public VatTuPhuTung(string n)
        {
            InitializeComponent();
            LoadVatTuPhuTungList();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;
        }

        #region Hien thi du lieu len datagrid

        public class VatTuPhuTungs //Khong can cung duoc, tai co Class san ben EntityFramework
        {
            public int MaVTPT { get; set; }
            public string TenVT { get; set; }
            public Nullable<int> SLTon { get; set; }
            public string DVT { get; set; }
            public Nullable<decimal> GiaNhap { get; set; }
            public Nullable<decimal> GiaBan { get; set; }
        }

        void LoadVatTuPhuTungList() //Hien thi nhan vien len datagrid
        {
            ObservableCollection<VatTuPhuTungs> vatTuPhuTungs = new ObservableCollection<VatTuPhuTungs>();
            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.ToList();
            foreach (var item in List)
            {
                VatTuPhuTungs vATTUPHUTUNG = new VatTuPhuTungs();
                vATTUPHUTUNG.MaVTPT = item.MaVatTuPhuTung;
                vATTUPHUTUNG.TenVT = item.TenVTPT;
                int Madvt = item.MaDVT;
                var dVT = DataProvider.Ins.DB.DONVITINHs.Where(x => x.MaDVT == Madvt).SingleOrDefault();
                vATTUPHUTUNG.DVT = dVT.TenDVT;
                vATTUPHUTUNG.GiaNhap = item.DonGiaNhap;
                vATTUPHUTUNG.GiaBan = item.DonGiaBan; 
                vATTUPHUTUNG.SLTon = item.SoLuongTon;
                vatTuPhuTungs.Add(vATTUPHUTUNG);
                BangVTPT.ItemsSource = vatTuPhuTungs;
            }


        }

        #endregion

        #region Hien thi du lieu len textbox

        private void BangVTPT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                int MaVatTu = selected_row.MaVTPT;
                txbMaVT.Text = selected_row.MaVTPT.ToString();
                var l = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == MaVatTu).SingleOrDefault();
                txbTenVatTu.Text = selected_row.TenVT;
                txbSLTon.Text = l.SoLuongTon.ToString();
                txbDonViTinh.Text = l.DONVITINH.TenDVT;
                txbGiaNhap.Text = l.DonGiaNhap.ToString();
                txbGiaBan.Text = l.DonGiaBan.ToString();

            }
        }

        #endregion

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


        #region popUp chuyen tab
        private void btnNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            PhieuNhapVatTuPhuTung phieuNhapVatTuPhuTung = new PhieuNhapVatTuPhuTung(tbUserName.Text);
            phieuNhapVatTuPhuTung.Visibility=Visibility.Visible;
        }

        private void btnLichSuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
            LichSuNhapVatTuPhuTung lichSuNhapVatTuPhuTung = new LichSuNhapVatTuPhuTung(tbUserName.Text);
            lichSuNhapVatTuPhuTung.Visibility = Visibility.Visible;
        }


        #endregion

        #region btn xu ly 
        private void btnThemVTPT_Click(object sender, RoutedEventArgs e)
        {


            ThemVatTuPhuTung themVatTuPhuTung = new ThemVatTuPhuTung(tbUserName.Text);
            themVatTuPhuTung.ShowDialog();
            this.Close();

        }


        #endregion

        #region Sua xoa thong tin tren datagrid
        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaVT.Text) || string.IsNullOrEmpty(txbTenVatTu.Text)
                || string.IsNullOrEmpty(txbSLTon.Text) || string.IsNullOrEmpty(txbDonViTinh.Text)
                || string.IsNullOrEmpty(txbGiaNhap.Text) || string.IsNullOrEmpty(txbGiaBan.Text))

            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                try
                {
                    int MaVT = int.Parse(txbMaVT.Text);
                    var n = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == MaVT).SingleOrDefault();

                    if (n != null)
                    {
                        n.MaVatTuPhuTung = MaVT;
                        n.TenVTPT = txbTenVatTu.Text;
                        n.SoLuongTon = int.Parse(txbSLTon.Text);
                        n.DonGiaNhap = decimal.Parse(txbGiaNhap.Text);
                        n.DonGiaBan = decimal.Parse(txbGiaBan.Text);

                        MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật thông tin vật tư phụ tùng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (r == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.SaveChanges();

                            MessageBox.Show("Cập nhật thông tin vật tư phụ tùng thành công!");
                            LoadVatTuPhuTungList();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy vật tư phụ tùng với mã cung cấp.");
                    }    
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Định dạng nhập vào không hợp lệ: " + fe.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }

            }
            return;
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)BangVTPT;
            dynamic t = grid.SelectedItem;
            int MaVT = t.MaVTPT;
            var n = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == MaVT).SingleOrDefault();
            if (n != null)
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa vật tư?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.VATTUPHUTUNGs.Remove(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Xóa vật tư thành công!");
                    LoadVatTuPhuTungList();
                }
                else return;
            }
        }

        private void btnXuatFile_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
