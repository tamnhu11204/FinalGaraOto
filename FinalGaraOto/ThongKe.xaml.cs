using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
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
using FinalGaraOto.Action;
using FinalGaraOto.Model;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        public ThongKe()
        {
            InitializeComponent();
            LoadComboBoxNamBaoCao();
            LoadComboBoxThangBaoCao();
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


        private void Btn_Xuat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Doanhthu_Click(object sender, RoutedEventArgs e)
        {
            BCDoanhThu Child = new BCDoanhThu();
            Child.Show();
            this.Close();
        }

        private void Hangton_Click(object sender, RoutedEventArgs e)
        {
            BCTon Child = new BCTon();
            Child.Show();
            this.Close();
        }

        void LoadComboBoxNamBaoCao()
        {
            if (Bangthongke.TabIndex == 0)
            {
                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Year).ToList();
                
                foreach (var item in List)
                {
                   
                    
                    NamCb.Items.Add(item);
                }
            }
        else
            {
               
                var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Year).ToList();

                foreach (var item in List)
                {
                   

                    NamCb.Items.Add(item);
                }
            }
        }

        void LoadComboBoxThangBaoCao()
        {
            if (Bangthongke.TabIndex ==0 )
            {
                var List = DataProvider.Ins.DB.PHIEUTHUTIENs.Select(x => x.NgayThuTien.Month).ToList();

                foreach (var item in List)
                {
                   

                    ThangCb.Items.Add(item);
                }
            }
            else
            {
                var List = DataProvider.Ins.DB.PHIEUNHAPs.Select(x => x.NgayNhapHang.Value.Month).ToList();

<<<<<<< HEAD
                foreach (var item in List)
                {
                  

                    ThangCb.Items.Add(item);
                }
            }
        }


        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

=======
            ThangCb.ItemsSource = data;
        }
>>>>>>> 23e51462c5c2f91ab287f497287fa5a2087072b7
    }
}
