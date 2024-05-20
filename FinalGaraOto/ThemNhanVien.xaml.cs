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
    /// Interaction logic for ThemNhanVien.xaml
    /// </summary>
    public partial class ThemNhanVien : Window
    {
        public ThemNhanVien()
        {
            InitializeComponent();
            LoadComboBoxChucVu();
        }
        void LoadComboBoxChucVu()
        {
            var List = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Select(x=>x.TenNhom).ToList();
            foreach (var item in List)
            {
                cbbChucVu.Items.Add(item);
            }
        }
        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.Show();
            this.Close();
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbCCCD.Text) || string.IsNullOrEmpty(txbDiaChi.Text) || string.IsNullOrEmpty(txbHoVaTen.Text)
                || string.IsNullOrEmpty(txbMatKhau.Text) || string.IsNullOrEmpty(txbNgaySinh.Text) || string.IsNullOrEmpty(txbSDT.Text)
                || string.IsNullOrEmpty(txbTenDangNhap.Text) || string.IsNullOrEmpty(cbbChucVu.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                string _TenDangNhap = txbTenDangNhap.Text;
                var TaiKhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == _TenDangNhap).Count();
                if(TaiKhoan > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Hãy dùng tên khác.");
                }
                else
                {
                    NGUOIDUNG n = new NGUOIDUNG();
                    n.TenNguoiDung = txbHoVaTen.ToString();

                    DateTime? ngaySinh = txbNgaySinh.SelectedDate;
                    if (ngaySinh.HasValue)
                    {

                        n.NgaySinhNguoiDung = ngaySinh.Value;
                    }
                    else
                    {
                        return;
                    }

                    n.CCCDNguoiDung = txbCCCD.ToString();
                    n.DiaChiNguoiDung = txbDiaChi.ToString();
                    n.SDTNguoiDung = txbSDT.ToString();
                    n.TenDangNhap = txbTenDangNhap.ToString();
                    n.MatKhau = txbMatKhau.ToString();

                    string selectedValue = cbbChucVu.SelectedItem as string;
                    var l = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Where(x => x.TenNhom == selectedValue).First();
                    n.MaNhom = l.MaNhom;

                    using (var dbContext = new QLGARAOTOEntities()) 
                    {
                        dbContext.NGUOIDUNGs.Add(n);
                    }

                    MessageBox.Show("Thêm nhân viên thành công!");
                    this.Close();
                }
            }
            return;
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
