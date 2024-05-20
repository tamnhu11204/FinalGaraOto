using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Window
    {
        public NhanVien()
        {
            InitializeComponent();
            LoadNhanVienList();
        }
        #region stackbutton
        private void BtnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion 
        void LoadNhanVienList() //Hien thi nhan vien len datagrid
        {
            ObservableCollection<NhanViens> nhanViens = new ObservableCollection<NhanViens>();
            var List = DataProvider.Ins.DB.NGUOIDUNGs.ToList();
            foreach (var item in List)
            {
                NhanViens nhanViens1 = new NhanViens();
                nhanViens1.Ma = item.MaNguoiDung;
                nhanViens1.HoVaTen = item.TenNguoiDung;
                nhanViens1.ChucVu = DataProvider.Ins.DB.NHOMNGUOIDUNGs.Where(x => x.MaNhom == item.MaNhom).Select(x => x.TenNhom).First().ToString();
                nhanViens.Add(nhanViens1);
                dtgNhanVien.ItemsSource = nhanViens;
            }

        }
        private void Btn_ThemNhanVien(object sender, RoutedEventArgs e) //Chuyen qua Window ThemNhanVien
        {
            ThemNhanVien themNhanVien = new ThemNhanVien();
            themNhanVien.ShowDialog();
            this.Close();
        }
       
        /*private void dtgNV_SelectionChanged(object sender, SelectionChangedEventArgs e) //Khi chon 1 hang thi se hien thi sang Groupbox Thong tin chi tiet
        {
            if(dtgNhanVien.SelectedIndex.ToString()!=null)
            {
                DataGridRow dtr = (DataGridRow)dtgNhanVien.SelectedItem;
                if (dtr != null)
                {
                    int Ma = int.Parse(dtr.ToString());
                    var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MaNguoiDung == Ma).FirstOrDefault();
                    txbHoVaTen.Text=l.TenNguoiDung;
                    txbNgaySinh.Text=l.NgaySinhNguoiDung.ToString();
                    txbCCCD.Text = l.CCCDNguoiDung;
                    txbDiaChi.Text = l.DiaChiNguoiDung;
                    txbSDT.Text=l.SDTNguoiDung;
                    txbTenDangNhap.Text=l.TenDangNhap;
                    txbMatKhau.Text=l.MatKhau;
                }
            }
        }*/
    }
    public class NhanViens
    {
        public int Ma {  get; set; }
        public string HoVaTen { get; set; }
        public string ChucVu { get; set; }
    }
}
