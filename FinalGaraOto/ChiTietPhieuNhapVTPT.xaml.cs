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
            LoadTenPhuTung();
            tbUserName.Text = n;
            tbMa.Text = MaNH.ToString();
            LoadTongTien();

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

        /*private void BangLSNVTPT_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        }*/

        void LoadTongTien()
        {
            int Maa = int.Parse(tbMa.Text);
            var m1 = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhapHang == Maa).SingleOrDefault();
            txbTongTienNhapHang.Text = m1.TongTienNhapHang.ToString();
        }
        void LoadTenPhuTung()
        {
            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.Select(x => x.TenVTPT).ToList();
            foreach (var item in List)
            {
                cbbChonVTPT.Items.Add(item);
            }
        }

        void LoadGiaNhap()
        {

            int Maa = 0;



            string selectedValue = cbbChonVTPT.SelectedItem as string;

            //decimal giaNhap = decimal.Parse(txbNhapGiaNhap.Text);
            var gnh = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == selectedValue).FirstOrDefault();
            if (gnh != null)
            {
                Maa = gnh.MaVatTuPhuTung;
            }
            var gnh1 = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung == Maa).FirstOrDefault();

            gnh.DonGiaNhap = gnh1.GiaNhap;

        }

        /*void LoadSoLuongTon()
        {
            int Maa = 0;
            string selectedValue = cbbChonVTPT.SelectedItem as string;
            //decimal giaNhap = decimal.Parse(txbNhapGiaNhap.Text);
            //var gnh = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == Maa).SingleOrDefault();
            var gnh = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == selectedValue).FirstOrDefault();
            if (gnh != null)
            {
                Maa = gnh.MaVatTuPhuTung;
            }
            var gnh1 = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung == Maa).FirstOrDefault();

            gnh.SoLuongTon = gnh1.SoLuong;
        }*/

        void LoadGiaBan()
        {
            decimal Loi = 0;
            var giaban = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "L").FirstOrDefault();
            Loi = decimal.Parse(giaban.GiaTri.ToString());

            int Maa = 0;
            string selectedValue = cbbChonVTPT.SelectedItem as string;
            //decimal giaNhap = decimal.Parse(txbNhapGiaNhap.Text);
            //var gnh = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == Maa).SingleOrDefault();
            var gnh2 = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == selectedValue).FirstOrDefault();
            if (gnh2 != null)
            {
                Maa = gnh2.MaVatTuPhuTung;
            }
            var gnh3 = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung == Maa).FirstOrDefault();

            gnh2.DonGiaBan = gnh3.GiaNhap * (1 + Loi / 100);
        }

        void LamMoi()
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
                n.MaVatTuPhuTung = tenVT.MaVatTuPhuTung;

                n.MaNhapHang = int.Parse(tbMa.Text);
                n.SoLuong = int.Parse(txbNhapSLVT.Text);

                n.GiaNhap = decimal.Parse(txbNhapGiaNhap.Text);
                n.ThanhTien = decimal.Parse(txbThanhTien.Text);
                DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                var m = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhapHang == n.MaNhapHang).SingleOrDefault();
                m.TongTienNhapHang = m.TongTienNhapHang + n.ThanhTien;
                DataProvider.Ins.DB.SaveChanges();
                LoadTongTien();

                var slg = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == n.MaVatTuPhuTung).SingleOrDefault();
                slg.SoLuongTon += n.SoLuong;
                //LoadSoLuongTon();
                DataProvider.Ins.DB.SaveChanges();

                var gnh = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == n.MaVatTuPhuTung).SingleOrDefault();
                gnh.DonGiaNhap = n.GiaNhap;
                DataProvider.Ins.DB.SaveChanges();
                LoadGiaNhap();


                decimal loi = 0;
                var t = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "L").SingleOrDefault();
                loi = decimal.Parse(t.GiaTri.ToString());
                var gban = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == n.MaVatTuPhuTung).SingleOrDefault();
                gban.DonGiaBan = n.GiaNhap * ( 1 + loi/100);
                LoadGiaBan();

                MessageBox.Show("Thêm thành công!");
                LamMoi();
            }
            LoadLichSuNhapVatTuPhuTungList();
            return;
        }



        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
            vatTuPhuTung.Show();
            this.Close();
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {

            HoaDonThanhToanPhuTung hoaDonThanhToan = new HoaDonThanhToanPhuTung(tbUserName.Text, tbMa.Text);
            hoaDonThanhToan.Show();
            this.Close();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)BangLSNVTPT;
            dynamic t = grid.SelectedItem;
            int MaVT = t.MaVTPT;
            int MaNH = int.Parse(tbMa.Text);

            int SL1 = t.SL;
            decimal T1 = t.ThanhTien;

            var n = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaVatTuPhuTung == MaVT && x.MaNhapHang == MaNH).SingleOrDefault();
            if (n != null)
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa vật tư?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Remove(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Xóa vật tư thành công!");


                    var vtpt = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == MaVT).SingleOrDefault();
                    vtpt.SoLuongTon = vtpt.SoLuongTon - SL1;
                    DataProvider.Ins.DB.SaveChanges();

                    var pn = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhapHang == MaNH).SingleOrDefault();
                    pn.TongTienNhapHang = pn.TongTienNhapHang - T1;
                    DataProvider.Ins.DB.SaveChanges();
                    LoadTongTien();
                    LoadLichSuNhapVatTuPhuTungList();
                }
                else return;
            }
            //LoadLichSuNhapVatTuPhuTungList();
            return;
        }

        #region Hien thi du lieu len datagrid

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

        void LoadLichSuNhapVatTuPhuTungList() //Hien thi nhan vien len datagrid
        {
            int Ma1 = int.Parse(tbMa.Text);
            ObservableCollection<ChiTietNhapVatTuPhuTungs> chiTietNhapVatTuPhuTungs = new ObservableCollection<ChiTietNhapVatTuPhuTungs>();
            var List = DataProvider.Ins.DB.CHITIETPHIEUNHAPs.Where(x => x.MaNhapHang == Ma1).ToList();

            int stt = 1;

            foreach (var item in List)
            {

                ChiTietNhapVatTuPhuTungs chiTietNhapVatTuPhuTungs1 = new ChiTietNhapVatTuPhuTungs();

                chiTietNhapVatTuPhuTungs1.STT = stt++;
                chiTietNhapVatTuPhuTungs1.MaVTPT = item.MaVatTuPhuTung;
                chiTietNhapVatTuPhuTungs1.MaNhapHang = item.MaNhapHang;
                chiTietNhapVatTuPhuTungs1.TenVT = item.VATTUPHUTUNG.TenVTPT;
                chiTietNhapVatTuPhuTungs1.GiaNhap = item.GiaNhap;
                chiTietNhapVatTuPhuTungs1.SL = item.SoLuong;
                chiTietNhapVatTuPhuTungs1.ThanhTien = item.ThanhTien;
                chiTietNhapVatTuPhuTungs.Add(chiTietNhapVatTuPhuTungs1);
                BangLSNVTPT.ItemsSource = chiTietNhapVatTuPhuTungs;
            }


        }

        #endregion

        private void cbbChonVTPT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // Lấy tên vật tư đã chọn
            string selectedVatTu = cbbChonVTPT.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedVatTu))
            {
                // Truy vấn cơ sở dữ liệu để lấy giá của vật tư
                var vatTu = DataProvider.Ins.DB.VATTUPHUTUNGs.FirstOrDefault(x => x.TenVTPT == selectedVatTu);

                if (vatTu != null && vatTu.DonGiaNhap != null)
                {
                    // Hiển thị giá trong textBox NhapGiaNhap
                    txbNhapGiaNhap.Text = vatTu.DonGiaNhap.ToString();
                    txbNhapGiaNhap.IsReadOnly = true;
                }
                else
                {
                    // Cho phép người dùng nhập giá
                    txbNhapGiaNhap.Text = string.Empty;
                    txbNhapGiaNhap.IsReadOnly = false;
                }
            }

        }

    }



}
