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
    /// Interaction logic for ThemHangXe.xaml
    /// </summary>
    public partial class ThemHangXe : Window
    {
        public ThemHangXe(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnThem_Click(object sender, RoutedEventArgs e) //Them tien cong
        {
            if (string.IsNullOrEmpty(txbTenHieuXe.Text))
            {
                MessageBox.Show("Hãy điền tên hiệu xe!");
            }
            else
            {
                var n = new HIEUXE();
                n.TenHieuXe = txbTenHieuXe.Text;
                if (string.IsNullOrEmpty(txbGhiChu.Text))
                {
                    n.GhiChu = "Không có";
                }
                else
                {
                    n.GhiChu = txbGhiChu.Text;
                }

                DataProvider.Ins.DB.HIEUXEs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Thêm hiệu xe thành công!");

                TuyChon tuychon_ = new TuyChon(tbUserName.Text);
                this.Close();
                tuychon_.Show();
            }
        }
        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_ = new TuyChon(tbUserName.Text);
            this.Close();
            tuychon_.Show();
        }
    }
}
