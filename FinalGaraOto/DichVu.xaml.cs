using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for DichVu.xaml
    /// </summary>
    public partial class DichVu : Window
    {
        public DichVu()
        {
            InitializeComponent();
            LoaddsXeList();
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


        void LoaddsXeList() //Hien thi nhan vien len datagrid
        {
            ObservableCollection<Xes> Xe = new ObservableCollection<Xes>();
            var List = DataProvider.Ins.DB.XEs.ToList();
            foreach (var item in List)
            {
                Xes Xe1 = new Xes() ;
                Xe1.BienSo = item.BienSoXe;
                Xe1.HieuXe = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == item.MaHieuXe).Select(x => x.TenHieuXe).First();
                Xe1.TenChu = DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe == item.MaChuXe).Select(x => x.TenChuXe).First();
                Xe1.Ngay = item.NgayTiepNhan.ToString();
                Xe1.No = item.TienNo.ToString();
                Xe.Add(Xe1);
                dtgdsXe.ItemsSource = Xe;
            }

        }

        /*void LoadNhanVienList() //Hien thi nhan vien len datagrid
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

        } */
        public class Xes
        {
         
            public string BienSo { get; set; }
            public string HieuXe { get; set; }
            public string TenChu { get; set; }
            public string Ngay { get; set; }
            public string No { get; set; }
            
        }

    }
}
