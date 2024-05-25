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
    /// Interaction logic for ChiTietSuDungVTPT.xaml
    /// </summary>
    public partial class ChiTietSuDungVTPT : Window
    {
        string n, MaCT_;
        public ChiTietSuDungVTPT(string maUser, string MaCT)
        {
            InitializeComponent();
            LoadComboBox();
            n = maUser;
            MaCT_ = MaCT;

        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if(cbLoaiTC.SelectedValue == null || txbSL.Text == null)
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin");

            }
            else
            {
                
            }   
            
        }

        void LoadComboBox()
        {
            var List = DataProvider.Ins.DB.TIENCONGs.Select(x => x.TenTienCong).ToList();
            foreach (var item in List)
            {
                cbLoaiTC.Items.Add(item);
            }
        }
    }
}
