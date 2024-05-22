using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TuyChon.xaml
    /// </summary>
    public partial class TuyChon : Window
    {
        public TuyChon()
        {
            InitializeComponent();
            LoadSoXeTiepNhan();
            LoadNhanVienList();
        }


        #region scroll bar button
        public void btnClosing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        public void btnMaximize_Click(object sender, RoutedEventArgs e)
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

        public void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion


        #region button tab

        private void thongKe_Tab(object sender, RoutedEventArgs e)
        {
            ThongKe thongke_tab = new ThongKe();
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow();
            taikhoan_tab.Show();
            this.Close();
        }

        private void dichVu_Tab(object sender, RoutedEventArgs e)
        {
            DichVu dichvu_tab = new DichVu();
            dichvu_tab.Show();
            this.Close();
        }

        private void nhanVien_Tab(object sender, RoutedEventArgs e)
        {
            NhanVien nhanvien_tab = new NhanVien();
            nhanvien_tab.Show();
            this.Close();
        }

        private void khoHang_Tab(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung khohang_tab = new VatTuPhuTung();
            khohang_tab.Show();
            this.Close();
        }

        private void tuyChon_Tab(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_tab = new TuyChon();
            tuychon_tab.Show();
            this.Close();
        }
        #endregion

        #region ThongTinGara va DonViVTPT
        void LoadSoXeTiepNhan() //Hien thi so xe tiep nhan
        {
            var S = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo=="X").SingleOrDefault();
            txbSoXeTiepNhan.Text=S.GiaTri.ToString();
        }

        private void BtnCapNhatSoXe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbSoXeTiepNhan.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật số xe tiếp nhận trong ngày?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    var S = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "X").SingleOrDefault();
                    S.GiaTri = int.Parse(txbSoXeTiepNhan.Text);

                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!");

                    TuyChon tuychon_=new TuyChon();
                    tuychon_.Show();
                    this.Close();
                }
                else return;
            }
        }
        void LoadNhanVienList() //Hien thi don vi VTPT len datagrid
        {
            ObservableCollection<DONVITINH> donvi = new ObservableCollection<DONVITINH>();
            var List = DataProvider.Ins.DB.DONVITINHs.ToList();
            foreach (var item in List)
            {
                DONVITINH d = new DONVITINH();
                d.MaDVT = item.MaDVT;
                d.TenDVT = item.TenDVT;
                donvi.Add(d);
                dtgDonVi.ItemsSource = donvi;
            }
        }

        private void BtnCapNhapDVT_Click(object sender, RoutedEventArgs e) //Cap nhat don vi tinh
        {
            if (string.IsNullOrEmpty(txbMaDVT.Text) || string.IsNullOrEmpty(txbTenDVT.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                int MaDVT = int.Parse(txbMaDVT.Text);
                var n = DataProvider.Ins.DB.DONVITINHs.Where(x => x.MaDVT == MaDVT).SingleOrDefault();

                n.MaDVT = MaDVT;
                n.TenDVT=txbTenDVT.Text;
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật đơn vị vật tư phụ tùng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!");

                    TuyChon tuychon_ = new TuyChon();
                    tuychon_.Show();
                    this.Close();
                }
                else return;
            }
        }

        private void BtnXoaDVT_Click(object sender, RoutedEventArgs e) //Xoa don vi tinh
        {
            int MaDVT = int.Parse(txbMaDVT.Text);
            var n = DataProvider.Ins.DB.DONVITINHs.Where(x => x.MaDVT == MaDVT).SingleOrDefault();
            if (n != null)
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa đơn vị tính?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.DONVITINHs.Remove(n);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Xóa đơn vị tính thành công!");

                    TuyChon tuychon_ = new TuyChon();
                    tuychon_.Show();
                    this.Close();
                }
                else return;
            }
        }
        private void BtnThemDVT_Click(object sender, RoutedEventArgs e)
        {
            ThemDonVi themDonVi = new ThemDonVi();
            themDonVi.ShowDialog();
            this.Close();
        }
        #endregion
    }
}
