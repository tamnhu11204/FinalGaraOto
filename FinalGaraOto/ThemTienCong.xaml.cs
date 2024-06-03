using FinalGaraOto.Model;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for ThemTienCong.xaml
    /// </summary>
    public partial class ThemTienCong : Window
    {
        public ThemTienCong(string n)
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
            if (string.IsNullOrEmpty(txbTenTienCong.Text) || string.IsNullOrEmpty(txbGiaTienCong.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                int i = 0;
                var check = DataProvider.Ins.DB.TIENCONGs.ToList();
                foreach (var item in check)
                {
                    if (txbTenTienCong.Text.ToLower() == item.TenTienCong.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    var n = new TIENCONG();
                    n.TenTienCong = txbTenTienCong.Text;
                    n.GiaTienCong = Decimal.Parse(txbGiaTienCong.Text);

                    DataProvider.Ins.DB.TIENCONGs.Add(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm tiền công thành công!");

                    TuyChon tuychon_ = new TuyChon(tbUserName.Text);
                    this.Close();
                    tuychon_.Show();
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

        private void txbGiaTienCong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
