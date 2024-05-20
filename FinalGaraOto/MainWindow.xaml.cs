using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
            if ( result == MessageBoxResult.Yes)
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

        private void thongke_Tab(object sender, RoutedEventArgs e)
        {
            ThongKe thongke_tab = new ThongKe();
            thongke_tab.Show();
            this.Close();
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
