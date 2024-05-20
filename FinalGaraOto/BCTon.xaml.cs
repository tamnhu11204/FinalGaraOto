using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
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
       
        public BCTon()
        {
            InitializeComponent();
            LoadComboBoxNamBaoCao();
            LoadComboBoxThangBaoCao();
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

<<<<<<< HEAD
        void LoadComboBoxNamBaoCao()
=======
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

        private void LoadDataFromDatabaseNam()
>>>>>>> 23e51462c5c2f91ab287f497287fa5a2087072b7
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


          
            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.Select(x => x.TenVTPT).ToList();
            foreach(var item in List)
            {
                
            }
               
        }

       
    }

}
