using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BCDoanhThu.xaml
    /// </summary>
    public partial class BCDoanhThu : Window
    {
        private string _message;
        public BCDoanhThu()
        {
            InitializeComponent();
            LoadDataFromDatabaseNam();
            LoadDataFromDatabaseThang();
        }

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
           ThongKe thongKe = new ThongKe();
            thongKe.Show();
            this.Close();
        }

        private void Bnt_bcton_Click(object sender, RoutedEventArgs e)
        {
           BCTon HangTon = new BCTon();
            HangTon.Show();
            this.Close();
        }
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
    }
}
