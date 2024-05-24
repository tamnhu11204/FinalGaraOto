using FinalGaraOto.Model;
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
using System.Windows.Shapes;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for ThemNhaCungCap.xaml
    /// </summary>
    public partial class ThemNhaCungCap : Window
    {
        public ThemNhaCungCap()
        {
            InitializeComponent();
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnThem_Click(object sender, RoutedEventArgs e) //Them ncc
        {
            if (string.IsNullOrEmpty(txbTen.Text) || string.IsNullOrEmpty(txbSDT.Text)
                || string.IsNullOrEmpty(txbEmail.Text)
                || string.IsNullOrEmpty(txbDiaChi.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                var n = new NHACUNGCAP();
                n.TenNhaCungCap = txbTen.Text;
                n.SDTNhaCungCap = txbSDT.Text;
                n.EmailNhaCungCap = txbEmail.Text;
                n.DiachiNhaCungCap = txbDiaChi.Text;

                DataProvider.Ins.DB.NHACUNGCAPs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Thêm nhà cung cấp thành công!");

                TuyChon tuychon_ = new TuyChon();
                this.Close();
                tuychon_.Show();
            }
        }
        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_ = new TuyChon();
            this.Close();
            tuychon_.Show();
        }
    }
}
