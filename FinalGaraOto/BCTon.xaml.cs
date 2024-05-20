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
        private void LoadDataFromDatabaseNam()
        {
            string connectionString = "Server = DESKTOP-F5DEQJ7\\DIEMNGAN; Initial Catalog = QLGARAOTO; Integrated Security = True"; ; // Thay thế bằng chuỗi kết nối của bạn
            string query = ("SELECT year(NamBaoCaoTon) FROM BAOCAOTON"); // Thay thế bằng truy vấn của bạn

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
            string connectionString = "Server = DESKTOP-F5DEQJ7\\DIEMNGAN; Initial Catalog = QLGARAOTO; Integrated Security = True";  // Thay thế bằng chuỗi kết nối của bạn
            string query = ("SELECT month(ThangBaoCaoTon) FROM BAOCAOTON"); // Thay thế bằng truy vấn của bạn

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

        private void LoadData_Datagrid()
        {
            // Chuỗi kết nối tới cơ sở dữ liệu
            string connectionString = "Server = DESKTOP-F5DEQJ7\\DIEMNGAN; Initial Catalog = QLGARAOTO; Integrated Security = True";

            // Truy vấn SQL để lấy dữ liệu
            string query = "SELECT ";

            // Tạo một đối tượng DataTable để lưu trữ dữ liệu
            DataTable dataTable = new DataTable();

            // Thực hiện kết nối và truy vấn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                // Mở kết nối và đổ dữ liệu vào DataTable
                connection.Open();
                dataAdapter.Fill(dataTable);
            }

            // Gán DataTable làm nguồn dữ liệu cho DataGrid
            Dg_BCTon.ItemsSource= dataTable.DefaultView;
        }

    }

}
