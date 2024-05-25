using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
using static FinalGaraOto.ThongKe;

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
            var List = DataProvider.Ins.DB.BAOCAODOANHTHUs.Select(x => x.NamBaoCao.Year).ToList();
            foreach (var item in List)
            {
                NamCb.Items.Add(item);
            }
        }

        private void LoadDataFromDatabaseThang()
        {

            var List = DataProvider.Ins.DB.BAOCAODOANHTHUs.Select(x => x.NamBaoCao.Month).ToList();
            foreach (var item in List)
            {
                ThangCb.Items.Add(item);
            }
        }

        void LoadDataBCDT()
        {
            ObservableCollection<BCDT> doanhthu = new ObservableCollection<BCDT>();
            
            var List = DataProvider.Ins.DB.BAOCAODOANHTHUs.ToList();
            int stt = 1;
            float sum = 0;
            foreach (var item in List)
            {   
                BCDT doanhthu1 = new BCDT();
                doanhthu1.stt=stt++;
                var mahieuxe = DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu == item.MaBaoCaoDoanhThu).Select(x => x.MaHieuXe).First();
                doanhthu1.hieuxe= DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe== mahieuxe).Select(x => x.TenHieuXe).First();
                doanhthu1.soluotxe =(int) DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.SoLuotSua).First();
                doanhthu1.thanhtien= (float)DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.ThanhTien).First();
                doanhthu1.tile= (float)DataProvider.Ins.DB.CTBAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.TiLe).First();
                doanhthu1.thang= item.ThangBaoCao;
                doanhthu1.nam= Convert.ToDateTime( item.NamBaoCao);
                doanhthu.Add(doanhthu1);
                if ((NamCb.Text== Convert.ToString(doanhthu1.nam.Year)) && (ThangCb.Text== Convert.ToString(doanhthu1.thang.Month)))
                {
                    Dg_Bcdoanhthu.ItemsSource=doanhthu;

                    sum= (float)DataProvider.Ins.DB.BAOCAODOANHTHUs.Where(x => x.MaBaoCaoDoanhThu== item.MaBaoCaoDoanhThu).Select(x => x.TongDoanhThu).First();
                   
                    Lb_tongdt.Content= "Tổng doanh thu: "+ sum + "VND";
                    
                   
                }
            }

        }

        private void Bnt_xembc_Click(object sender, RoutedEventArgs e)
        {
            LoadDataBCDT();
        }

        private void Bnt_dong_Click(object sender, RoutedEventArgs e)
        {
            ThongKe child= new ThongKe(tbUserName.Text);
            child.Show();
            this.Close();
        }

        private void Bnt_xuatfilebc_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel_BCDoanhThu export= new ExportToExcel_BCDoanhThu(Dg_Bcdoanhthu, DateTime.Now);
        }
    }

    public class BCDT
    {
        public int stt { get; set; }
        public string hieuxe { get; set; }
        public int soluotxe { get; set; }
        public float thanhtien { get; set; }
        public float tile { get; set; }

        public DateTime thang { get; set; }
        public DateTime nam { get; set; }
    }
}
