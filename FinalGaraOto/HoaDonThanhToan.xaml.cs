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
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for HoaDonThanhToan.xaml
    /// </summary>
    public partial class HoaDonThanhToan : Window
    {
        string MaXe_;
        public HoaDonThanhToan(string n, string MaCX)
        {
            InitializeComponent();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;
            MaXe_ = MaCX;
            LoadChiTietSuaChua();
            LoadThongTinChuXe();
            LoadPhieuThu();
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

        public class CHITIET
        {
            public int STT { get; set; }

            public string NoiDung { get; set; }
            public string TenVT { get; set; }
            public int SL { get; set; }
            public string Gia { get; set; }
            public string TenTC { get; set; }
            public string TC { get; set; }
            public string ThanhTien { get; set; }

        }

        void LoadChiTietSuaChua()
        {
            ObservableCollection<CHITIET> ChiTiets = new ObservableCollection<CHITIET>();
            var l = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaTiepNhan.ToString() == MaXe_).SingleOrDefault();
            MaXe_ = l.MaSuaChua.ToString();
            var List = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaSuaChua == l.MaSuaChua).ToList();
            int i = 1;
            //decimal TongTien = 0;
            foreach (var item in List)
            {
                CHITIET ct1 = new CHITIET();
                ct1.STT = i;
                ct1.NoiDung = item.NoiDung;
                ct1.TenTC = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == item.MaTienCong).Select(x => x.TenTienCong).First();
                ct1.TC = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == item.MaTienCong).Select(x => x.GiaTienCong.ToString()).First();
                ct1.Gia = item.TongTienVTPT.ToString(); ;
                ct1.TenVT = "";
                ct1.Gia = "";
                decimal ThanhTien = 0;
                var List2 = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaChiTietSuaChua == item.MaChiTietSuaChua).ToList();
                int SL = 0;
                foreach (var item2 in List2)
                {

                    string ten = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == item2.MaVatTuPhuTung).Select(x => x.TenVTPT).First();
                    ct1.TenVT = ct1.TenVT + item2.SoLuong.ToString() + " " + ten + " , ";
                    SL = SL + item2.SoLuong;
                    item2.DonGia = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == item2.MaVatTuPhuTung).Select(x => x.DonGiaBan).First();

                    //decimal Tien = (item2.SoLuong) * decimal.Parse(item2.DonGia.ToString());
                    string t = ((item2.SoLuong) * item2.DonGia).ToString();
                    item2.ThanhTien = (item2.SoLuong) * item2.DonGia;
                    ThanhTien = ThanhTien + Convert.ToDecimal(item2.ThanhTien);
                    var c = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaVatTuPhuTung == item2.MaVatTuPhuTung && x.MaChiTietSuaChua == item2.MaChiTietSuaChua).First();
                   
                }
              
                i++;
                ct1.SL = SL;
                ct1.Gia = ThanhTien.ToString();
                ct1.ThanhTien = (DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == item.MaTienCong).Select(x => x.GiaTienCong).First() + ThanhTien).ToString();
                //ct1.ThanhTien = (int.Parse(ct1.Gia) + int.Parse( ct1.TC)).ToString();
                ChiTiets.Add(ct1);
                dtgChiTiet.ItemsSource = ChiTiets;

                LoadTongTien();

                //TongTien = TongTien + DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == item.MaTienCong).Select(x => x.GiaTienCong).First() + ThanhTien;
            }
            /*if (TongTien == 0)
            {
                txbTongTien.Text = "0";
            }
            else
            {
                txbTongTien.Text = TongTien.ToString();
            }*/

        }

        void LoadTongTien()
        {
            int ma = int.Parse(MaXe_);
            var m1 = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaTiepNhan == ma).SingleOrDefault();
            tbTongTien.Text = m1.TongTienSuaCHua.ToString() + " đồng ";
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            ChiTietSuaChuaXe ct_ = new ChiTietSuaChuaXe(tbUserName.Text, MaXe_);
            this.Close();
            ct_.Show();
        }


        void LoadThongTinChuXe ()
        {
            string y = DataProvider.Ins.DB.XEs.Where(x => x.MaTiepNhan.ToString() == MaXe_).Select(x => x.MaChuXe.ToString()).First();
            var c = DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe.ToString() == y).First();
            txbHoVaTen.Text = c.TenChuXe;
            txbDiaChi.Text = c.DiaChiChuXe;
            txbSDT.Text = c.SDTChuXe;
            txbEmail.Text = c.EmailChuXe;
            dpNgay.SelectedDate = DateTime.Now;
        }

        void LoadPhieuThu()
        {
           
            var l = DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan.ToString() == MaXe_).ToList();
            int i = 0;
            foreach (var item in l)
            {
                i++;
            }
            if (i==0)
            {
                var t = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaTiepNhan.ToString() == MaXe_).SingleOrDefault();
                var s = new PHIEUTHUTIEN();
                s.MaTiepNhan = int.Parse(MaXe_);
                s.NgayThuTien = DateTime.Now;
                s.SoTienThu = Convert.ToDecimal (t.TongTienSuaCHua);
                DataProvider.Ins.DB.PHIEUTHUTIENs.Add(s);
                DataProvider.Ins.DB.SaveChanges();

            }    

        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
