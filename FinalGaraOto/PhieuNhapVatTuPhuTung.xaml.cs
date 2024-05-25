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
    /// Interaction logic for PhieuNhapVatTuPhuTung.xaml
    /// </summary>
    public partial class PhieuNhapVatTuPhuTung : Window
    {
<<<<<<< HEAD
        
        public PhieuNhapVatTuPhuTung()
        {
            InitializeComponent();
            LoadComboBoxDonViCungCap();
=======
        VatTuPhuTung vatTuPhuTung;
        public PhieuNhapVatTuPhuTung(string n)
        {
            InitializeComponent();
            tbUserName.Text = n;
>>>>>>> e9ac6bcbb8120c00268e80150ea02ed8f53704b5
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        void LoadComboBoxDonViCungCap() //Hien thi cac item trong combobox
        {
            var List = DataProvider.Ins.DB.NHACUNGCAPs.Select(x => x.TenNhaCungCap).ToList();
            foreach (var item in List)
            {
                cbbDVCC.Items.Add(item);
            }
        }

        private void btnThoatLapPhieuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
<<<<<<< HEAD
            VatTuPhuTung vatTuPhuTung = new VatTuPhuTung();
=======
            vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);
>>>>>>> e9ac6bcbb8120c00268e80150ea02ed8f53704b5
            vatTuPhuTung.Visibility = Visibility.Visible;
        }

        private void btnXacNhanLapPhieuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (string.IsNullOrEmpty(dtpNgayNhapHang.Text) || string.IsNullOrEmpty(cbbDVCC.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                var n = new PHIEUNHAP();

                DateTime? ngayNhap = dtpNgayNhapHang.SelectedDate;
                if (ngayNhap.HasValue)
                {

                    n.NgayNhapHang = ngayNhap.Value;
                }
                else
                {
                    return;
                }

                string selectedValue = cbbDVCC.SelectedItem as string;
                var nhaCC = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.TenNhaCungCap == selectedValue).SingleOrDefault();
                if (nhaCC != null)
                {
                    n.MaNhaCungCap = nhaCC.MaNhaCungCap;

                }

                DataProvider.Ins.DB.PHIEUNHAPs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                //MessageBox.Show("Lập phiếu nhập thành công!");
               

                this.Close();
                ChiTietPhieuNhapVTPT chiTietPhieuNhapVTPT = new ChiTietPhieuNhapVTPT();
                chiTietPhieuNhapVTPT.Show();
            }
=======
            this.Close();
            ChiTietPhieuNhapVTPT chiTietPhieuNhapVTPT = new ChiTietPhieuNhapVTPT(tbUserName.Text);
            chiTietPhieuNhapVTPT.Show();
>>>>>>> e9ac6bcbb8120c00268e80150ea02ed8f53704b5
        }
    }
}
