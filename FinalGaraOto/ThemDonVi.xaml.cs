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
    /// Interaction logic for ThemDonVi.xaml
    /// </summary>
    public partial class ThemDonVi : Window
    {
        public ThemDonVi(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;

            
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e) //Them don vi tinh
        {
            if (string.IsNullOrEmpty(txbTenDVT.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                int i = 0;
                var check = DataProvider.Ins.DB.DONVITINHs.ToList();
                foreach (var item in check)
                {
                    if (txbTenDVT.Text.ToLower() == item.TenDVT.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    var n = new DONVITINH();
                    n.TenDVT = txbTenDVT.Text;

                    DataProvider.Ins.DB.DONVITINHs.Add(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm đơn vị tính thành công!");

                    TuyChon tuychon_ = new TuyChon(tbUserName.Text);
                    tuychon_.Show();
                    this.Close();
                }
                else MessageBox.Show("Tên đơn vị này đã tồn tại!");
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
