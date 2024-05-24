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
    /// Interaction logic for PhieuNhapVatTuPhuTung.xaml
    /// </summary>
    public partial class PhieuNhapVatTuPhuTung : Window
    {
        VatTuPhuTung vatTuPhuTung;
        public PhieuNhapVatTuPhuTung(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnThoatLapPhieuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
            vatTuPhuTung.Visibility = Visibility.Visible;
        }

        private void btnXacNhanLapPhieuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ChiTietPhieuNhapVTPT chiTietPhieuNhapVTPT = new ChiTietPhieuNhapVTPT(tbUserName.Text);
            chiTietPhieuNhapVTPT.Show();
        }
    }
}
