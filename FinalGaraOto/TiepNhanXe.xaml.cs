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
    /// Interaction logic for TiepNhanXe.xaml
    /// </summary>
    public partial class TiepNhanXe : Window
    {
        public TiepNhanXe(string n)
        {
            InitializeComponent();
            LoadComboBoxHieuXe();
            tbUserName.Text = n;
        }

        int demo;

        #region Load Hieu xe
        void LoadComboBoxHieuXe() //Hien thi cac item trong combobox
        {
            var List = DataProvider.Ins.DB.HIEUXEs.Select(x => x.TenHieuXe).ToList();
            foreach (var item in List)
            {
                cbbHieuXe.Items.Add(item);
            }
        }
        #endregion

        #region Tiep nhan xe
        private void btnXacNhan_Click(object sender, RoutedEventArgs e) // Xác nhận tiếp nhận xe
        {
            if (string.IsNullOrEmpty(txbTenChuXe.Text) || string.IsNullOrEmpty(txbBienSo.Text) || string.IsNullOrEmpty(cbbHieuXe.Text) || string.IsNullOrEmpty(txbEmail.Text) || string.IsNullOrEmpty(txbDiaChi.Text) ||
                string.IsNullOrEmpty(txbSDT.Text) || string.IsNullOrEmpty(dpNgayTiepNhan.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                var n = new CHUXE();
                n.TenChuXe = txbTenChuXe.Text;
                n.DiaChiChuXe = txbDiaChi.Text;
                n.EmailChuXe = txbEmail.Text;
                n.SDTChuXe = txbSDT.Text;
                DataProvider.Ins.DB.CHUXEs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                var m = new XE();
                m.MaChuXe = n.MaChuXe;
                var l = DataProvider.Ins.DB.HIEUXEs.Where(x => x.TenHieuXe == cbbHieuXe.Text).SingleOrDefault();
                m.MaHieuXe = l.MaHieuXe;
                m.BienSoXe = txbBienSo.Text;
                m.NgayTiepNhan = DateTime.Parse(dpNgayTiepNhan.Text);
                DataProvider.Ins.DB.XEs.Add(m);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Tiếp nhận xe thành công!");

                TuyChon tuychon_ = new TuyChon(tbUserName.Text);
                this.Close();
                tuychon_.Show();
            }
        }
        #endregion

        #region Thoat va di chuyen
        private void btnThoat_Click(object sender, RoutedEventArgs e) // Thoát cửa sổ
        {
            DichVu dv_ = new DichVu(tbUserName.Text);
            this.Close();
            dv_.Show();
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e) // Di chuyển cửa sổ
        {
            this.DragMove();
        }
        #endregion
    }
}
