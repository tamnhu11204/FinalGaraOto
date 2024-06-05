using FinalGaraOto.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for ThemThongTinSuaChua.xaml
    /// </summary>
    public partial class ThemThongTinSuaChua : Window
    {
        string MaBC_, MaCT;
        
        public ThemThongTinSuaChua(string n, string MaBC)
        {
            InitializeComponent();
            tbUserName.Text = n;
            MaBC_= MaBC;
            LoadComboBox();
            LoadMa();

        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            var c = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaChiTietSuaChua.ToString() == MaCT).SingleOrDefault();
            var List = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaChiTietSuaChua == c.MaChiTietSuaChua).ToList();
            foreach (var item in List)
            {
                DataProvider.Ins.DB.CT_SUDUNGVTPT.Remove(item);
            }
            DataProvider.Ins.DB.CHITIETSUACHUAs.Remove(c);
            DataProvider.Ins.DB.SaveChanges();
            var d = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaSuaChua.ToString() == MaBC_).SingleOrDefault();
            string m = d.MaTiepNhan.ToString();
            ChiTietSuaChuaXe t = new ChiTietSuaChuaXe(tbUserName.Text, m);
            ChiTietSuaChuaXe ct_ = new ChiTietSuaChuaXe(tbUserName.Text, d.MaTiepNhan.ToString());
            this.Close();
            ct_.Show();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNoiDung.Text) || string.IsNullOrEmpty(cbLoaiTC.SelectedItem.ToString()))
            {
                MessageBox.Show("Cần nhập Nội dung và chọn loại tiền công ");
            }
            else
            {
                var c = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaChiTietSuaChua.ToString() == MaCT).SingleOrDefault();
                c.MaSuaChua = int.Parse(MaBC_);
                c.NoiDung = txbNoiDung.Text;
                c.MaTienCong = DataProvider.Ins.DB.TIENCONGs.Where(x => x.TenTienCong == cbLoaiTC.SelectedItem.ToString()).Select(x => x.MaTienCong).SingleOrDefault();
                var List = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaChiTietSuaChua == c.MaChiTietSuaChua).ToList();
               
                foreach (var item in List)
                {
                    c.TongTienVTPT = c.TongTienVTPT + item.ThanhTien;
                }
                decimal tc = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == c.MaTienCong).Select(x => x.GiaTienCong).SingleOrDefault();
                c.TongCong = c.TongTienVTPT + tc;
                DataProvider.Ins.DB.SaveChanges();

                var psc = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaSuaChua == c.MaSuaChua).SingleOrDefault();
                psc.TongTienSuaCHua = psc.TongTienSuaCHua + c.TongCong;
                DataProvider.Ins.DB.SaveChanges();

                var v = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaSuaChua == c.MaSuaChua).SingleOrDefault();
                var xe = DataProvider.Ins.DB.XEs.Where(x => x.MaTiepNhan == v.MaTiepNhan).SingleOrDefault();
                xe.TienNo = xe.TienNo + c.TongCong;
                DataProvider.Ins.DB.SaveChanges();

                ChiTietSuaChuaXe ct_ = new ChiTietSuaChuaXe(tbUserName.Text, psc.MaTiepNhan.ToString());
                this.Close();
                ct_.Show();
            }
        }

        void LoadComboBox ()
        {
            var List = DataProvider.Ins.DB.TIENCONGs.Select(x => x.TenTienCong).ToList();
            foreach (var item in List)
            {
                cbLoaiTC.Items.Add(item);
            }
        }

        private void btnThemVT_Click(object sender, RoutedEventArgs e)
        {
            ChiTietSuDungVTPT ct = new ChiTietSuDungVTPT(tbUserName.Text, MaCT);
            ct.Show();
        }

        private void cbLoaiTC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaChiTietSuaChua.ToString() == MaCT).SingleOrDefault();
            decimal tc = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == c.MaTienCong).Select(x => x.GiaTienCong).SingleOrDefault();
            txbTC.Text = tc.ToString();
        }

        void LoadMa()
        {
            var c = new CHITIETSUACHUA();
            c.MaSuaChua = int.Parse(MaBC_);
            c.MaTienCong = DataProvider.Ins.DB.TIENCONGs.Select(x=>x.MaTienCong).First();
            DataProvider.Ins.DB.CHITIETSUACHUAs.Add(c);
            DataProvider.Ins.DB.SaveChanges();
            MaCT = c.MaChiTietSuaChua.ToString();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            LoadVatTu();
        }

        void LoadVatTu ()
        {
            var c = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaChiTietSuaChua.ToString() == MaCT).SingleOrDefault();
            var List = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaChiTietSuaChua == c.MaChiTietSuaChua).ToList();
            string t = "";
            int s = 0;
            decimal a = 0;
            foreach (var item in List)
            {
                string ten = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == item.MaVatTuPhuTung).Select(x => x.TenVTPT).First();
                 t = t + item.SoLuong.ToString() + " " + ten + " , ";
                s = s + item.SoLuong;

                var dgb= DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung == item.MaVatTuPhuTung).SingleOrDefault();
                decimal? b = item.SoLuong * dgb.DonGiaBan;  
                    a = a + Convert.ToDecimal(b);
            }
            txbVatTu.Text = t;
            txbSL.Text = s.ToString();
            txbDonGia.Text = a.ToString();
            var v = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaChiTietSuaChua.ToString() == MaCT).SingleOrDefault();
            if (cbLoaiTC.SelectedItem != null)
            {
                decimal tc = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == v.MaTienCong).Select(x => x.GiaTienCong).SingleOrDefault();
                txbThanhTien.Text = (tc + a).ToString();
            }
        }
    }
}
