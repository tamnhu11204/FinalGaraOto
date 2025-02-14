﻿using FinalGaraOto.Model;
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
        public PhieuNhapVatTuPhuTung(string n)
        {
            InitializeComponent();
            LoadComboBoxDonViCungCap();
            InitializeComponent();
            tbUserName.Text = n;
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

            VatTuPhuTung vatTuPhuTung = new VatTuPhuTung(tbUserName.Text);

            vatTuPhuTung.Show();
            this.Close();

        }

        private void btnXacNhanLapPhieuNhapVTPT_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dtpNgayNhapHang.Text) || string.IsNullOrEmpty(cbbDVCC.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                var n = new PHIEUNHAP();

                string selectedValue = cbbDVCC.SelectedItem as string;
                var nhaCC = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.TenNhaCungCap == selectedValue).SingleOrDefault();
                if (nhaCC != null)
                {
                    n.MaNhaCungCap = nhaCC.MaNhaCungCap;

                }

                DateTime? ngayNhap = dtpNgayNhapHang.SelectedDate;
                if (ngayNhap.HasValue)
                {

                    n.NgayNhapHang = ngayNhap.Value;
                }
                else
                {
                    return;
                }
                n.TongTienNhapHang = 0;
               
                DataProvider.Ins.DB.PHIEUNHAPs.Add(n);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Lập phiếu nhập thành công!");
               
                var GetMa=DataProvider.Ins.DB.PHIEUNHAPs.Where(x=>x.MaNhapHang==n.MaNhapHang).SingleOrDefault();
                
                ChiTietPhieuNhapVTPT chiTietPhieuNhapVTPT = new ChiTietPhieuNhapVTPT(tbUserName.Text,GetMa.MaNhapHang);
                chiTietPhieuNhapVTPT.Show();
                this.Close();
            }
        }
    }
}
