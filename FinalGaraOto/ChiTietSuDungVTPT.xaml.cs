using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if(cbVTPT.SelectedValue == null || txbSL.Text == null)
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin");
            }
            else 
            {
                var v = DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x => x.TenVTPT == cbVTPT.SelectedItem.ToString()).SingleOrDefault();
                if (v.SoLuongTon < int.Parse(txbSL.Text))
                {
                    string a = v.TenVTPT;
                    string b = v.SoLuongTon.ToString();
                    MessageBox.Show("Số lượng yêu cầu sử dụng đã quá số lượng tồn !" +
                        a + "còn tồn " + b);
                }
                else
                {
                    var n = new CT_SUDUNGVTPT();

                    n.MaVatTuPhuTung = v.MaVatTuPhuTung;
                    n.MaChiTietSuaChua = int.Parse(MaCT_);
                    n.SoLuong = int.Parse(txbSL.Text);
                    n.ThanhTien = v.DonGiaBan * n.SoLuong;
                    v.SoLuongTon = v.SoLuongTon - n.SoLuong;
                    DataProvider.Ins.DB.CT_SUDUNGVTPT.Add(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm vật tư phụ tùng và số lượng thành công ");
                    txbSL.Text = null;
                    cbVTPT.SelectedIndex = 0;
                }
            }   
            
        }

        void LoadComboBox()
        {
            var List = DataProvider.Ins.DB.VATTUPHUTUNGs.Select(x => x.TenVTPT).ToList();
            foreach (var item in List)
            {
                cbVTPT.Items.Add(item);
            }
        }

        private void txbSoXeTiepNhan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

    }
}
