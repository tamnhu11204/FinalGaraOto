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
using System.Data;

namespace FinalGaraOto
{
    /// <summary>
    /// Interaction logic for DichVu.xaml
    /// </summary>
    public partial class DichVu : Window
    {
        int s = 0;
        public DichVu(string n)
        {
            InitializeComponent();
            LoaddsXeList();
            LoadComboBoxHieuXe();
            tbUserName.Text = n;

            var l = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == n).SingleOrDefault();
            if (l.MaNhom != 1) btnNhanVien.Visibility = Visibility.Hidden;

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
            ThongKe thongke_tab = new ThongKe(tbUserName.Text);
            thongke_tab.Show();
            this.Close();
        }


        private void taiKhoan_Tab(object sender, RoutedEventArgs e)
        {
            MainWindow taikhoan_tab = new MainWindow(tbUserName.Text);
            taikhoan_tab.Show();
            this.Close();
        }

        private void dichVu_Tab(object sender, RoutedEventArgs e)
        {
            DichVu dichvu_tab = new DichVu(tbUserName.Text);
            dichvu_tab.Show();
            this.Close();
        }

        private void nhanVien_Tab(object sender, RoutedEventArgs e)
        {
            NhanVien nhanvien_tab = new NhanVien(tbUserName.Text);
            nhanvien_tab.Show();
            this.Close();
        }

        private void khoHang_Tab(object sender, RoutedEventArgs e)
        {
            VatTuPhuTung khohang_tab = new VatTuPhuTung(tbUserName.Text);
            khohang_tab.Show();
            this.Close();
        }

        private void tuyChon_Tab(object sender, RoutedEventArgs e)
        {
            TuyChon tuychon_tab = new TuyChon(tbUserName.Text);
            tuychon_tab.Show();
            this.Close();
        }
        #endregion

        #region Load du lieu
        void LoaddsXeList() //Hien thi  xe len datagrid
        {
            ObservableCollection<Xes> Xe = new ObservableCollection<Xes>();
            var List = DataProvider.Ins.DB.XEs.ToList();
            foreach (var item in List)
            {
                if (item.BienSoXe != null)
                {
                    Xes Xe1 = new Xes();
                    Xe1.BienSo = item.BienSoXe;
                    Xe1.HieuXe = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == item.MaHieuXe).Select(x => x.TenHieuXe).First();
                    Xe1.ChuXe = DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe == item.MaChuXe).Select(x => x.TenChuXe).First();
                    Xe1.Ngay = item.NgayTiepNhan.ToString();
                    Xe1.No = item.TienNo.ToString();
                    Xe.Add(Xe1);
                    dtgdsXe.ItemsSource = Xe;
                }
            }
            var List2 = DataProvider.Ins.DB.XEs.Where(x => x.NgayTiepNhan == DateTime.Today).ToList();
            int i = 0;
            foreach (var item2 in List2)
            {
                i++;
            }
            var t = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "X").SingleOrDefault();
            tbXE.Text = "Số xe đã tiếp nhận hôm nay : " + i.ToString() + "/" + t.GiaTri.ToString();
            s = i;
        }

        void LoadComboBoxHieuXe() //Hien thi cac item trong combobox
        {
            var List = DataProvider.Ins.DB.HIEUXEs.Select(x => x.TenHieuXe).ToList();
            foreach (var item in List)
            {
                cbbHieuXe.Items.Add(item);
                
            }

        }
        public class Xes
        {
         
            public string BienSo { get; set; }
            public string HieuXe { get; set; }
            public string ChuXe { get; set; }
            public string Ngay { get; set; }
            public string No { get; set; }
            
        }
        #endregion

        #region btn TiepNhan
        private void btnTiepNhanXe_Click(object sender, RoutedEventArgs e)
        {
            var t = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "X").SingleOrDefault();
            if (s >= t.GiaTri)
            {
                MessageBox.Show("Số xe tiếp nhận đã đủ với quy định.Xin điều chỉnh số lượng xe nếu muốn tiếp nhận thêm ");
            }
            else
            {
                TiepNhanXe tiepnhan = new TiepNhanXe(tbUserName.Text);
                tiepnhan.ShowDialog();
                this.Close();
            }
        }
        #endregion

       
        #region Tim kiem
        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            txbBienSo.Text = "";
            txbChuXe.Text = "";
            txbTienNo.Text = "";
            cbbHieuXe.Text = "";
            dpNgay.Text = "";
            LoaddsXeList();
        }

        private void txbBienSo_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimKiem();
        }

        private void cbbHieuXe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimKiem();
        }

        private void txbChuXe_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimKiem();
        }

        private void txbTienNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimKiem();
        }

        private void dpNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TimKiem();
        }
        void TimKiem()
        {
            ObservableCollection<Xes> xe = new ObservableCollection<Xes>();
            string tenCX = txbChuXe.Text.ToLower();
            string BSX = txbBienSo.Text.ToLower();
            string t = "";
            if (cbbHieuXe.SelectedItem != null)
            {
                t = cbbHieuXe.SelectedItem.ToString();
            }
            string HieuXe = t.ToLower();
            string TienNo = txbTienNo.Text.ToLower();
            string NgayTN = dpNgay.Text.ToLower();


            var List = DataProvider.Ins.DB.XEs.ToList();
            foreach (var item in List)
            {
                string tenCX_ = DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe == item.MaChuXe).Select(x => x.TenChuXe.ToLower()).First();
                string BSX_ = item.BienSoXe.ToLower();
                string TienNo_ = item.TienNo.ToString().ToLower();
                string HieuXe_ = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == item.MaHieuXe).Select(x => x.TenHieuXe.ToLower()).First();
                string NgayTN_ = item.NgayTiepNhan.ToString().ToLower();
                if (tenCX_.Contains(tenCX) && BSX_.Contains(BSX) && HieuXe_.Contains(HieuXe) && HieuXe_.Contains(HieuXe_) && TienNo_.Contains(TienNo) 
                    && NgayTN_.Contains(NgayTN) )
                {
                    Xes Xe1 = new Xes();
                    Xe1.BienSo = item.BienSoXe;
                    Xe1.HieuXe = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == item.MaHieuXe).Select(x => x.TenHieuXe).First();
                    Xe1.ChuXe = DataProvider.Ins.DB.CHUXEs.Where(x => x.MaChuXe == item.MaChuXe).Select(x => x.TenChuXe).First();
                    Xe1.Ngay = item.NgayTiepNhan.ToString();
                    Xe1.No = item.TienNo.ToString();
                    xe.Add(Xe1);
                }
            }

            dtgdsXe.ItemsSource = xe;
        }
        #endregion
        string MaXe = null;
        private void dtgdsXe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null && selected_row.BienSo != null)
            {
                string b = selected_row.BienSo.ToString();
                var m = DataProvider.Ins.DB.XEs.Where(x => x.BienSoXe.ToString() == b).SingleOrDefault();
                MaXe = m.MaTiepNhan.ToString();
            }    
        }

        private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if(MaXe != null)
            {
                ChiTietSuaChuaXe ct_ = new ChiTietSuaChuaXe(tbUserName.Text,MaXe);
                this.Close();
                ct_.Show();
            }
            else
            {
                MessageBox.Show("Chưa chọn xe muốn xem chi tiết");
            }

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MaXe != null)
            {
                DataGrid grid = (DataGrid)dtgdsXe;
                dynamic t = grid.SelectedItem;
                string BS = t.BienSo.ToString();
                var n = DataProvider.Ins.DB.XEs.Where(x => x.BienSoXe.ToString() == BS).SingleOrDefault();
                var m = DataProvider.Ins.DB.PHIEUSUACHUAs.Where(x => x.MaTiepNhan == n.MaTiepNhan).SingleOrDefault();
                var c = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaSuaChua == m.MaSuaChua).ToList();
                
                if (n != null)
                {
                    MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa xe?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        foreach (var item in c)
                        {
                            var d = DataProvider.Ins.DB.CT_SUDUNGVTPT.Where(x => x.MaChiTietSuaChua == item.MaChiTietSuaChua).ToList();
                            foreach (var item1 in d)
                            {
                                DataProvider.Ins.DB.CT_SUDUNGVTPT.Remove(item1);
                            }
                            DataProvider.Ins.DB.CHITIETSUACHUAs.Remove(item);
                        }
                        DataProvider.Ins.DB.PHIEUSUACHUAs.Remove(m);
                        DataProvider.Ins.DB.XEs.Remove(n);
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Xóa xe thành công!");
                        LoaddsXeList();
                    }
                    else return;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn xe muốn xóa");
            }
        }
    }
}
