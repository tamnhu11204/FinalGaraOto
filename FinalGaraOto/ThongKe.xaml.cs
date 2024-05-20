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
            
        }
<<<<<<< HEAD
        private void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc là muốn thoát không?", "Quản Lý Quán Cafe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
=======

        public void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
>>>>>>> 5ad231a714a68ace9b0115bdc9013f30195ba1ea
            {
                this.Close();
            }
        }

<<<<<<< HEAD
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

        private void LoadDataFromDatabaseNam()
        {
            string connectionString = "DESKTOP-F5DEQJ7\\DIEMNGAN"; // Thay thế bằng chuỗi kết nối của bạn
            string query = ("SELECT NamBaoCaoTon FROM BAOCAOTON"); // Thay thế bằng truy vấn của bạn

            List<string> data = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader["NamBaoCao"].ToString()); // Thay thế ColumnName bằng tên cột bạn muốn lấy
                }

                reader.Close();
            }

            NamCb.ItemsSource = data;
        }

        private void LoadDataFromDatabaseThang()
        {
            string connectionString = "DESKTOP-F5DEQJ7\\DIEMNGAN"; // Thay thế bằng chuỗi kết nối của bạn
            string query = ("SELECT BAOCAOTON.ThangBaoCaoTon, BAOCAODOANHTHU.FROM BAOCAOTON"); // Thay thế bằng truy vấn của bạn

            List<string> data = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader["ThangBaoCao"].ToString()); // Thay thế ColumnName bằng tên cột bạn muốn lấy
                }

                reader.Close();
            }

            ThangCb.ItemsSource = data;

        }
=======
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

>>>>>>> 5ad231a714a68ace9b0115bdc9013f30195ba1ea
    }

        

}
