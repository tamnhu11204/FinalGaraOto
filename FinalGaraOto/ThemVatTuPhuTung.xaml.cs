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
    /// Interaction logic for ThemVatTuPhuTung.xaml
    /// </summary>
    public partial class ThemVatTuPhuTung : Window
    {
        VatTuPhuTung vatTuPhuTung;
        public ThemVatTuPhuTung()
        {
            InitializeComponent();
            
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnLuuVTPT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThoatThemVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
            vatTuPhuTung = new VatTuPhuTung();
            vatTuPhuTung.Visibility= Visibility.Visible;

            
        }
    }
}
