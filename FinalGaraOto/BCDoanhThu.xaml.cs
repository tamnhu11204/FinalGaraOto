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
                    Cb_Nam.Items.Add(item);
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
                    Cb_Thang.Items.Add(item);
                }
            }
        }

        private void LoadDataBCDT()
        {
            ObservableCollection<BCDT> kinhdoanh = new ObservableCollection<BCDT>();
            var Listhx = DataProvider.Ins.DB.HIEUXEs.Distinct().ToList();
            foreach (var item in Listhx)
            {
                BCDT ct = new BCDT();
                ct.hieuxe = item.MaHieuXe;
                ct.tenhieuxe = item.TenHieuXe;


                kinhdoanh.Add(ct);


            }

            foreach (var item in kinhdoanh)
            {
                var tt = DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.XE.HIEUXE.MaHieuXe == item.hieuxe);

                if (tt != null)
                {
                    int _t1 = Convert.ToInt32(Cb_Thang.Text);
                    int _t2 = Convert.ToInt32(Cb_Nam.Text);
                    item.thanhtien = tt.Where(x => x.NgayThuTien.Month == _t1 && x.NgayThuTien.Year == _t2).Sum(t => t.SoTienThu);

                    int _t3 = Convert.ToInt32(Cb_Thang.Text);
                    int _t4 = Convert.ToInt32(Cb_Nam.Text);
                    item.soluotsua = DataProvider.Ins.DB.PHIEUTHUTIENs.Where(x => x.XE.HIEUXE.MaHieuXe == item.hieuxe && x.NgayThuTien.Month == _t3 && x.NgayThuTien.Year == _t4).Count();

                }
                else
                {
                    item.thanhtien = 0;
                    item.soluotsua = 0;
                }


            }
            decimal sum = 0;
            foreach (var item in kinhdoanh)
            {
                sum += item.thanhtien;
            }

            foreach (var item in kinhdoanh)
            {
                item.tile = Convert.ToDouble(item.thanhtien) / Convert.ToDouble(sum);
            }

            Dg_Bcdoanhthu.ItemsSource = kinhdoanh;

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
        public int hieuxe {  get; set; }
        public string tenhieuxe { get; set; }

        public int soluotsua { get; set; }
        public decimal thanhtien { get; set; }
        public double tile { get; set; }

        public System.DateTime thang { get; set; }
        public System.DateTime nam { get; set; }
    }

  
}

    

