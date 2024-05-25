using FinalGaraOto.Model;
using Google.Protobuf.WellKnownTypes;
using Google.Type;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static FinalGaraOto.BCTon;
using static FinalGaraOto.ThongKe;
using static FinalGaraOto.VatTuPhuTung;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for BCDoanhThu.xaml
    /// </summary>
    public partial class BCDoanhThu : Window
    {
        private int stt = 0;
        public BCDoanhThu(string n)
        {

            InitializeComponent();
            // load_ctBCDT();
            // LOAD_bcdt();

            LoadDataFromDatabaseNam();
            LoadDataFromDatabaseThang();
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
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        #endregion

        #region button tab

        private void Bnt_bcton_Click(object sender, RoutedEventArgs e)
        {
            BCTon HangTon = new BCTon(tbUserName.Text);
            HangTon.Show();
            this.Close();
        }



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



        private void LoadDataFromDatabaseNam()
        {
            HashSet<int> uniqueYears = new HashSet<int>();
            var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Year).ToList();
            foreach (var item in List)
            {


                if (!uniqueYears.Contains(item))
                {
                    uniqueYears.Add(item);
                    NamCb.Items.Add(item);
                }
            }
        }

        private void LoadDataFromDatabaseThang()
        {
            HashSet<int> uniqueYears = new HashSet<int>();

            var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Month).ToList();
            foreach (var item in List)
            {
                if (!uniqueYears.Contains(item))
                {
                    uniqueYears.Add(item);
                    ThangCb.Items.Add(item);
                }
            }
        }

        void LoadDataBCDT()
        {
            ObservableCollection<BCDT> doanhthu = new ObservableCollection<BCDT>();

            var List = DataProvider.Ins.DB.BAOCAODOANHTHUs.ToList();
            int stt = 1;
            decimal sum = 0;
            foreach (var item in List)
            {
                BCDT doanhthu1 = new BCDT();
                doanhthu1.stt=stt++;
                doanhthu1.hieuxe= DataProvider.Ins.DB.HIEUXEs.Select(x => x.TenHieuXe).First();
                var xe = DataProvider.Ins.DB.XEs.Select(x => x.MaTiepNhan).First();
                doanhthu1.soluotxe = DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan==xe).Count();

                var ma = DataProvider.Ins.DB.HIEUXEs.Select(x => x.MaHieuXe).First();
                var a = (int)DataProvider.Ins.DB.XEs.Where(x => x.MaHieuXe== ma).Select(x => x.MaTiepNhan).First();
                doanhthu1.thanhtien= Convert.ToDecimal(DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan== (int)DataProvider.Ins.DB.XEs.Where(y => y.MaHieuXe==ma).Select(y => y.MaTiepNhan).First()).Select(x => x.SoTienThu));
                // doanhthu1.thanhtien= (float)DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.ThanhTien).First();
                doanhthu1.tile= (float)DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.TiLe).First();
                doanhthu1.thang= Convert.ToDateTime(DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan== (int)DataProvider.Ins.DB.XEs.Where(y => y.MaHieuXe==ma).Select(y => y.MaTiepNhan).First()).Select(z => z.NgayThuTien.Month).First());
                doanhthu1.nam= Convert.ToDateTime(DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.MaTiepNhan== (int)DataProvider.Ins.DB.XEs.Where(y => y.MaHieuXe==ma).Select(y => y.MaTiepNhan).First()).Select(z => z.NgayThuTien.Month).First());


                Lb_tongdt.Content= "Tong tien: "+ (sum+doanhthu1.thanhtien);
                if ((NamCb.Text== Convert.ToString(doanhthu1.nam.Year)) && ThangCb.Text== Convert.ToString(doanhthu1.thang.Month))
                {
                    doanhthu.Add(doanhthu1);

                }
                Dg_Bcdoanhthu.ItemsSource= doanhthu;

            }



        }


        public void load_ctBCDT()
        {

            /* var b= DataProvider.Ins.DB.BAOCAODOANHTHUs.ToList();
             var c= DataProvider.Ins.DB.HIEUXEs.ToList();
             foreach(var x in b)
             {
                 //CTBAOCAODOANHTHU ct = new CTBAOCAODOANHTHU();
                 //ct.MaBaoCaoDoanhThu= x.MaBaoCaoDoanhThu;
                 foreach(var y in c)
                 {
                     CTBAOCAODOANHTHU ct1 = new CTBAOCAODOANHTHU();
                     ct1.MaHieuXe= y.MaHieuXe;
                     ct1.MaBaoCaoDoanhThu = x.MaBaoCaoDoanhThu;
                     DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Add(ct1);

                 }

             }
             DataProvider.Ins.DB.CTBAOCAODOANHTHUs.
             DataProvider.Ins.DB.SaveChanges();*/

            ObservableCollection<CT_BCDT> ctBCDT = new ObservableCollection<CT_BCDT>();
            var List = DataProvider.Ins.DB.CTBAOCAODOANHTHUs.ToList();
            var List2 = DataProvider.Ins.DB.HIEUXEs.ToList();
            foreach (var item in List)
            {
                CT_BCDT ctbcdt = new CT_BCDT();
                foreach (var item2 in List2)
                {
                    ctbcdt.mabc= item.MaBaoCaoDoanhThu;
                    ctbcdt.tenhieuxe= DataProvider.Ins.DB.HIEUXEs.Select(x => x.TenHieuXe).First();
                    ctbcdt.mahieuxe= DataProvider.Ins.DB.HIEUXEs.Select(x => x.MaHieuXe).First();
                }



            }
        }

        public void loadbcdt()
        {

        }
        private void Bnt_xembc_Click(object sender, RoutedEventArgs e)
        {
            LoadDataBCDT();
        }

        private void Bnt_dong_Click(object sender, RoutedEventArgs e)
        {
            ThongKe child = new ThongKe(tbUserName.Text);
            child.Show();
            this.Close();
        }

        private void Bnt_xuatfilebc_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel_BCDoanhThu export = new ExportToExcel_BCDoanhThu(Dg_Bcdoanhthu, System.DateTime.Now);
        }





    }





    /* public void load_()
      {
          decimal sum = 0;
          vbcdt = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien).SingleOrDefault();
          var tongdt = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(X => X.SoTienThu);
          var a = new BAOCAODOANHTHU();
          a.ThangBaoCao = bcdt;
          a.NamBaoCao=bcdt;
          foreach (var item in tongdt)
          {
              if (bcdt.Year== a.NamBaoCao.Year && bcdt.Month== a.ThangBaoCao.Month)
              {
                  sum+=item;
              }
          }
          a.TongDoanhThu = sum;
          DataProvider.Ins.DB.SaveChanges();

      }*/




    public class BCDT
    {
        public int stt { get; set; }
        public string hieuxe { get; set; }
        public int soluotxe { get; set; }
        public decimal thanhtien { get; set; }
        public float tile { get; set; }

        public System.DateTime thang { get; set; }
        public System.DateTime nam { get; set; }
    }

    public class CT_BCDT
    {

        public int mabc { get; set; }
        public int mahieuxe { get; set; }
        public string tenhieuxe { get; set; }
        public int soluotsua { get; set; }
        public decimal thanhtien { get; set; }
        public float tile { get; set; }
    }
}

    

