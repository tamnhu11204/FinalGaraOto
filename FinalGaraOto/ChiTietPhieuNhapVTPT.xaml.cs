using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for ChiTietPhieuNhapVTPT.xaml
    /// </summary>
    public partial class ChiTietPhieuNhapVTPT : Window
    {
        public ChiTietPhieuNhapVTPT(string n, int MaNH)
        {
            InitializeComponent();
            LoadNgayNhap(); //Load lấy dữ liệu của window thêm trong phiếu nhập VTPT
            LoadTenPhuTung();
            tbUserName.Text = n;
            tbMa.Text = MaNH.ToString();

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

/*        public class ChiTietPhieuNhapVatTus
        {
            public int STT {  get; set; }
            public int MaVatTu { get; set; }
            public int MaNhapHang { get; set; }
            public Nullable<int> SoLuong {  get; set; } 
            public Nullable<decimal> GiaNhap { get; set; }
            public Nullable<decimal> TongTien => SoLuong * GiaNhap;
        }*/

        private void BangLSNVTPT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                int maVattu = selected_row.MaVatTu;
                cbbChonVTPT.IsReadOnly = true;
                var l = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung == maVattu).SingleOrDefault();

                cbbChonVTPT.Text = l.VATTUPHUTUNG.TenVTPT;
                txbNhapSLVT.Text = l.SoLuong.ToString();
                txbNhapGiaNhap.Text = l.GiaNhap.ToString();
                txbThanhTien.Text = l.ThanhTien.ToString();
                

            }

        }


        void LoadNgayNhap()
        {


            var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang).ToList();
            foreach (var item in List)
            {
                dtpNgayNhapHang.Text = item.ToString();

                txbDVCC.Text = item.ToString(); 
            }
        }

        void LoadTenPhuTung()
        {
            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.Select(x => x.TenVTPT).ToList();
            foreach (var item in List)
            {
                cbbChonVTPT.Items.Add(item);
            }
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            cbbChonVTPT.Text = null;
            txbNhapSLVT.Clear();
            txbThanhTien.Clear();
            txbNhapGiaNhap.Clear();

        }

        #region hien thi thanh tien mot dong
        private void txbNhapSLVT_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateThanhTien();
        }

        private void txbNhapGiaNhap_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateThanhTien();
        }

        // Khi dữ liệu thay đổi (ví dụ: sau khi người dùng nhập số lượng và giá nhập):
        private void UpdateThanhTien()
        {

            if (decimal.TryParse(txbNhapGiaNhap.Text, out decimal giaTien) &&
            int.TryParse(txbNhapSLVT.Text, out int soLuong))
            {
                decimal tongTien = giaTien * soLuong;
                txbThanhTien.Text = tongTien.ToString();
            }


        }
        /*private void UpdateTongTienThanhToan()
        {
            decimal tongTien = 0;

            // Lấy chỉ số của cột "Thành tiền"
            DataGridColumn column = BangLSNVTPT.Columns.FirstOrDefault(c => c.Header.ToString() == "Thành tiền");
            int columnIndex = column != null ? BangLSNVTPT.Columns.IndexOf(column) : -1;

            if (columnIndex >= 0)
            {
                foreach (var dataItem in BangLSNVTPT.Items)
                {
                    tongTien += dataItem.ThanhTien.Value;
                    
                }
            }

            // Hiển thị tổng tiền hoặc thực hiện các thao tác khác với giá trị này
            txbTongTienNhapHang.Text = tongTien.ToString();

        }*/

            #endregion
            private void btnThem_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(cbbChonVTPT.Text) || string.IsNullOrEmpty(txbNhapGiaNhap.Text)
                    || string.IsNullOrEmpty(txbNhapSLVT.Text))

                {
                    MessageBox.Show("Hãy điền đầy đủ thông tin");
                }
                else
                {

                    var n = new CHITIETPHIEUNHAP();

                    string selectedTenVT = cbbChonVTPT.SelectedItem as string;
                    var tenVT = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == selectedTenVT).SingleOrDefault();
                    if (tenVT != null)
                    {
                        n.MaVatTuPhuTung = tenVT.MaVatTuPhuTung;
                    }

                    n.MaNhapHang=int.Parse(tbMa.Text);
                    n.SoLuong = int.Parse(txbNhapSLVT.Text);
                    n.GiaNhap = decimal.Parse(txbNhapGiaNhap.Text);
                    n.ThanhTien = decimal.Parse(txbThanhTien.Text);

                    DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Add(n);
                    DataProvider.Ins.DB.SaveChanges();


                    MessageBox.Show("Thêm thành công!");
                }
            LoadLichSuNhapVatTuPhuTungList();
                return;
            }



            private void btnDong_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
                VatTuPhuTung vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
                vatTuPhuTung.Show();
            }

            private void btnThanhToan_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
                HoaDonThanhToanPhuTung hoaDonThanhToan = new HoaDonThanhToanPhuTung(tbUserName.Text);
                hoaDonThanhToan.Show();
            }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)BangLSNVTPT;
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
                    LoadLichSuNhapVatTuPhuTungList();
                }
                else return;
            }
        }

        #region Hien thi du lieu len datagrid

        public class ChiTietNhapVatTuPhuTungs //Khong can cung duoc, tai co Class san ben EntityFramework
        {
            public int MaVTPT { get; set; }
            public string TenVT { get; set; }
            public Nullable<int> SL { get; set; }
            
            public Nullable<decimal> GiaNhap { get; set; }
            public Nullable<decimal> ThanhTien { get; set; }
        }

        void LoadLichSuNhapVatTuPhuTungList() //Hien thi nhan vien len datagrid
        {
            ObservableCollection<ChiTietNhapVatTuPhuTungs> chiTietNhapVatTuPhuTungs = new ObservableCollection<ChiTietNhapVatTuPhuTungs>();
            var List = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.ToList();
            foreach (var item in List)
            {
                ChiTietNhapVatTuPhuTungs chiTietNhapVatTuPhuTungs1 = new ChiTietNhapVatTuPhuTungs();
                chiTietNhapVatTuPhuTungs1.MaVTPT = item.MaVatTuPhuTung;
                chiTietNhapVatTuPhuTungs1.TenVT = item.VATTUPHUTUNG.TenVTPT;
                chiTietNhapVatTuPhuTungs1.GiaNhap = item.GiaNhap;
                chiTietNhapVatTuPhuTungs1.SL = item.SoLuong;
                chiTietNhapVatTuPhuTungs.Add(chiTietNhapVatTuPhuTungs1);
                BangLSNVTPT.ItemsSource = chiTietNhapVatTuPhuTungs;
            }


        }

        #endregion

    }

}
