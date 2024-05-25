using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for BCTon.xaml
    /// </summary>
    public partial class BCTon : Window
    {

        public BCTon(string n)
        {
            InitializeComponent();
            LoadComboBoxNamBaoCao();
            LoadComboBoxThangBaoCao();
            tbUserName.Text = n;
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
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
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

        private void LoadComboBoxNamBaoCao()
        {
            var List = DataProvider.Ins.DB.BAOCAOTONs.Select(x => x.NamBaoCaoTon.Year).ToList();
            foreach (var item in List)
            {
                NamCb.Items.Add(item);
            }
        }

        void LoadComboBoxThangBaoCao()
        {
            var List = DataProvider.Ins.DB.BAOCAOTONs.Select(x => x.ThangBaoCaoTon.Month).ToList();
            foreach (var item in List)
            {
                ThangCb.Items.Add(item);
            }
        }

        private void Bnt_xembc_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<BaoCaoTon> bcton = new ObservableCollection<BaoCaoTon>();

            var List = DataProvider.Ins.DB.BAOCAOTONs.ToList();
            int stt = 0;
            foreach (var item in List)
            {
                BaoCaoTon baocaoton1 = new BaoCaoTon();
                baocaoton1.tevtpt=DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.MaVatTuPhuTung== item.MaVatTuPhuTung).Select(x => x.TenVTPT).First();
                baocaoton1.tondau= item.TonDau;
                baocaoton1.phatsinh= item.PhatSinh;
                baocaoton1.toncuoi= item.TonCuoi;
                baocaoton1.nam= item.NamBaoCaoTon;
                baocaoton1.thang = item.ThangBaoCaoTon;
                bcton.Add(baocaoton1);
                if ((NamCb.Text== Convert.ToString(baocaoton1.nam.Year)) && ThangCb.Text== Convert.ToString(baocaoton1.thang.Month))
                {
                    Dg_BCTon.ItemsSource= bcton;
                }


            }



        }

        public class BaoCaoTon
        {
            public int stt { get; set; }
            public string tevtpt { get; set; }
            public int tondau { get; set; }
            public int phatsinh { get; set; }
            public int toncuoi { get; set; }
            public DateTime thang { get; set; }
            public DateTime nam { get; set; }
        }

        private void Bnt_dong_Click(object sender, RoutedEventArgs e)
        {
            ThongKe child= new ThongKe(tbUserName.Text);
            child.Show();
            this.Close();
        }


    }

}
