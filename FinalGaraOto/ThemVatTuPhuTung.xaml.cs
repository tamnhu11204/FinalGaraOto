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
using static FinalGaraOto.VatTuPhuTung;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for ThemVatTuPhuTung.xaml
    /// </summary>
    public partial class ThemVatTuPhuTung : Window
    {
        VatTuPhuTung vatTuPhuTung;
        public ThemVatTuPhuTung(string n)
        {
            InitializeComponent();
            LoadComboBoxDonViTinh();
            tbUserName.Text = n;

        }

        void LoadComboBoxDonViTinh() //Hien thi cac item trong combobox
        {
            var List = DataProvider.Ins.DB.DONVITINHs.Select(x => x.TenDVT).ToList();
            foreach (var item in List)
            {
                cbbDVT.Items.Add(item);
            }
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #region Luu vật tư lên datagrid
        private void btnLuuVTPT_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTenPT.Text) || string.IsNullOrEmpty(cbbDVT.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                string _TenVTPT = tbxTenPT.Text;
                var VatTu = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == _TenVTPT).Count();
                if (VatTu > 0)
                {
                    MessageBox.Show("Tên phụ tùng đã tồn tại! Hãy dùng tên khác.");
                }
                else
                {
                    var n = new VATTUPHUTUNG();

                    n.TenVTPT = tbxTenPT.Text;

                    string selectedValue = cbbDVT.SelectedItem as string;
                    var donVT = DataProvider.Ins.DB.DONVITINHs.Where(x => x.TenDVT == selectedValue).SingleOrDefault();
                    if(donVT != null)
                    {
                        n.MaDVT = donVT.MaDVT;
                        
                    }    

                    DataProvider.Ins.DB.VATTUPHUTUNGs.Add(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm vật tư phụ tùng thành công!");                  

                    VatTuPhuTung vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
                    vatTuPhuTung.Show();
                    this.Close();
                }
            }
            return;
        }

        #endregion

        private void btnThoatThemVTPT_Click(object sender, RoutedEventArgs e)
        {

            VatTuPhuTung vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
            vatTuPhuTung.Show();
            this.Close();
        }



    }
}
