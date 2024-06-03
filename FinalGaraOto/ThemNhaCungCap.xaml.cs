using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ThemNhaCungCap(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;
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
                int i = 0;
                var check = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
                foreach (var item in check)
                {
                    if (txbTen.Text.ToLower() == item.TenNhaCungCap.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    var n = new NHACUNGCAP();
                    n.TenNhaCungCap = txbTen.Text;
                    n.SDTNhaCungCap = txbSDT.Text;
                    n.EmailNhaCungCap = txbEmail.Text;
                    n.DiachiNhaCungCap = txbDiaChi.Text;

                    DataProvider.Ins.DB.NHACUNGCAPs.Add(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm nhà cung cấp thành công!");

                    TuyChon tuyChon = new TuyChon(tbUserName.Text);
                    tuyChon.Show();
                    this.Close();
                }
                else MessageBox.Show("Tên tiền công này đã tồn tại!");

            }
        }
        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_ = new TuyChon(tbUserName.Text);
            this.Close();
            tuychon_.Show();
        }

        private void txbSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
