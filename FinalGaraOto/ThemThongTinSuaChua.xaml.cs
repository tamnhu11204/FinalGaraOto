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
    /// Interaction logic for ThemThongTinSuaChua.xaml
    /// </summary>
    public partial class ThemThongTinSuaChua : Window
    {
        string MaXe_;
        public ThemThongTinSuaChua(string n, string MaXe)
        {
            InitializeComponent();
            tbUserName.Text = n;
            MaXe_ = MaXe;
            LoadComboBox();

        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            ChiTietSuaChuaXe ct_ = new ChiTietSuaChuaXe(tbUserName.Text, MaXe_);
            this.Close();
            ct_.Show();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

        }

        void LoadComboBox ()
        {
            var List = DataProvider.Ins.DB.TIENCONGs.Select(x => x.TenTienCong).ToList();
            foreach (var item in List)
            {
                cbLoaiTC.Items.Add(item);
            }
        }

        private void btnThemVT_Click(object sender, RoutedEventArgs e)
        {
            ChiTietSuDungVTPT ct = new ChiTietSuDungVTPT(tbUserName.Text, MaXe_);
            ct.Show();
        }
    }
}
