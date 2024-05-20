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

        private void btnClosing_Click(object sender, RoutedEventArgs e)
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

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            ThongKe thongKe = new ThongKe();
            thongKe.Show();
            this.Close();
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        void LoadComboBoxNamBaoCao()
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
