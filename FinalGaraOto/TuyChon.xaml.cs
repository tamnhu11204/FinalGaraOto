﻿
using FinalGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TuyChon.xaml
    /// </summary>
    public partial class TuyChon : Window
    {
        public TuyChon(string n)
        {
            InitializeComponent();
            LoadSoXeTiepNhan();
            LoadPhamTramLoi();
            LoadDVT();
            LoadTienCong();
            LoadHieuXe();
            LoadNCC();
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

        #region ThongTinGara va DonViVTPT va Ti Le loi

        int Ma;
        dynamic g;
        void LoadSoXeTiepNhan() //Hien thi so xe tiep nhan
        {
            var S = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "X").SingleOrDefault();
            txbSoXeTiepNhan.Text = S.GiaTri.ToString();
        }

        void LoadPhamTramLoi() //Hien thi phan tram loi cua vat tu
        {
            var S = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "L").SingleOrDefault();
            txbPhanTramLoi.Text = S.GiaTri.ToString();
        }

        private void BtnCapNhatSoXe_Click(object sender, RoutedEventArgs e) //cap nhat so xe
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

                    LoadSoXeTiepNhan();
                }
                else return;
            }
        }

        private void BtnCapNhatPhanTramLoi_Click(object sender, RoutedEventArgs e) //cap nhat phan tram loi
        {
            if (string.IsNullOrEmpty(txbPhanTramLoi.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
            }
            else
            {
                MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật phần trăm lời của vật tư?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    var S = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "L").SingleOrDefault();
                    S.GiaTri = int.Parse(txbPhanTramLoi.Text);

                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!");

                    LoadPhamTramLoi();
                }
                else return;
            }
        }
        private void txbSoXeTiepNhan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
        private void txbPhanTramLoi_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
        void LoadDVT() //Hien thi don vi VTPT len datagrid
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
                int i = 0;
                var check = DataProvider.Ins.DB.DONVITINHs.ToList();
                foreach(var item in check)
                {
                    if (txbTenDVT.Text.ToLower() == item.TenDVT.ToLower())
                    {
                        
                        i++;
                    } 
                }
                if (i == 0)
                {
                    int MaDVT = int.Parse(txbMaDVT.Text);
                    var n = DataProvider.Ins.DB.DONVITINHs.Where(x => x.MaDVT == MaDVT).SingleOrDefault();

                    n.MaDVT = MaDVT;
                    n.TenDVT = txbTenDVT.Text;
                    MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật đơn vị vật tư phụ tùng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Cập nhật thành công!");

                        LoadDVT();
                    }
                    else return;
                }
                else MessageBox.Show("Tên đơn vị này đã tồn tại!");
            }
        }

        private void BtnXoaDVT_Click(object sender, RoutedEventArgs e) //Xoa don vi tinh
        {
            if (string.IsNullOrEmpty(txbMaDVT.Text) )
            {
                MessageBox.Show("Hãy chọn mã đơn vị muốn xóa");
            }
            else
            {
                int MaDVT = int.Parse(txbMaDVT.Text);
                var list=DataProvider.Ins.DB.VATTUPHUTUNGs.Where(x=>x.MaDVT== MaDVT).Count();
                if (list != 0)
                {
                    MessageBox.Show("Đơn vị này đã được sử dụng. Không được xóa!");
                }
                else
                {
                    var n = DataProvider.Ins.DB.DONVITINHs.Where(x => x.MaDVT == MaDVT).SingleOrDefault();
                    if (n != null)
                    {
                        MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa đơn vị tính?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (r == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.DONVITINHs.Remove(n);
                            DataProvider.Ins.DB.SaveChanges();

                            MessageBox.Show("Xóa đơn vị tính thành công!");

                            LoadDVT();
                        }
                        else return;
                    }
                }
            }
               
        }
        private void BtnThemDVT_Click(object sender, RoutedEventArgs e) //chuyen sang window ThemDVT
        {
            ThemDonVi themDonVi = new ThemDonVi(tbUserName.Text);
            themDonVi.ShowDialog();
            this.Close();
        }
        private void dtgDonVi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                txbMaDVT.Text = selected_row.MaDVT.ToString();
                txbTenDVT.Text = selected_row.TenDVT.ToString();
            }
        }

        #endregion


        #region TienCong
        void LoadTienCong() //Hien thi tien cong len datagrid
        {
            ObservableCollection<TIENCONG> tiencong = new ObservableCollection<TIENCONG>();
            var List = DataProvider.Ins.DB.TIENCONGs.ToList();
            foreach (var item in List)
            {
                TIENCONG tIENCONG = new TIENCONG();
                tIENCONG.MaTienCong = item.MaTienCong;
                tIENCONG.TenTienCong = item.TenTienCong;
                tIENCONG.GiaTienCong = item.GiaTienCong;
                tiencong.Add(tIENCONG);
                dtgTienCong.ItemsSource = tiencong;

            }
        }
        private void txbTenTienCong_TextChanged(object sender, TextChangedEventArgs e) //tim kiem khi txbTenTC thay doi
        {
            ObservableCollection<TIENCONG> tiencong = new ObservableCollection<TIENCONG>();
            string text = txbTenTienCong.Text.ToLower();

            var List = DataProvider.Ins.DB.TIENCONGs.ToList();
            foreach (var item in List)
            {
                string _tenTC = item.TenTienCong.ToLower();
                if (_tenTC.Contains(text))
                {
                    tiencong.Add(item);
                }
            }
            dtgTienCong.ItemsSource = tiencong;
        }
        private void txbGiaTienCong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
        private void BtnLamMoiTC_Click(object sender, RoutedEventArgs e) //Reset lai du lieu 
        {
            txbTenTienCong.Text = "";
            txbMaTienCong.Text = "";
            txbGiaTienCong.Text = "";
            LoadTienCong();
        }

        private void BtnThemTC_Click(object sender, RoutedEventArgs e) //chuyen sang window ThemTienCong
        {
            ThemTienCong themTienCong = new ThemTienCong(tbUserName.Text);
            themTienCong.ShowDialog();
            this.Close();
        }
        private void BtnSuaTC_Click(object sender, RoutedEventArgs e) //cap nhat thong tin tien cong
        {
            if (string.IsNullOrEmpty(txbMaTienCong.Text) || string.IsNullOrEmpty(txbTenTienCong.Text) || string.IsNullOrEmpty(txbGiaTienCong.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ mã và tên hiệu xe");
            }
            else
            {
                int i = 0;
                var check = DataProvider.Ins.DB.TIENCONGs.ToList();
                foreach (var item in check)
                {
                    if (txbTenTienCong.Text.ToLower() == item.TenTienCong.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    int MATC = int.Parse(txbMaTienCong.Text);
                    var n = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == MATC).SingleOrDefault();

                    n.MaTienCong = MATC;
                    n.TenTienCong = txbTenTienCong.Text;
                    n.GiaTienCong = decimal.Parse(txbGiaTienCong.Text);
                    MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật tiền công?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Cập nhật thành công!");

                        LoadTienCong();
                    }
                    else return;
                }
                else MessageBox.Show("Tên tiền công này đã tồn tại!");
               
            }
        }
        private void BtnXoaTC_Click(object sender, RoutedEventArgs e) //xoa tien cong
        {
            if (string.IsNullOrEmpty(txbMaTienCong.Text))
            {
                MessageBox.Show("Hãy chọn mã tiền công muốn xóa!");
            }
            else
            {
                int MaTC = int.Parse(txbMaTienCong.Text);

                var list = DataProvider.Ins.DB.CHITIETSUACHUAs.Where(x => x.MaTienCong == MaTC).Count();
                if (list != 0)
                {
                    MessageBox.Show("Loại tiền công này đã được sử dụng. Không được xóa!");
                }
                else
                {
                    var n = DataProvider.Ins.DB.TIENCONGs.Where(x => x.MaTienCong == MaTC).SingleOrDefault();
                    if (n != null)
                    {
                        MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa tiền công?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (r == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.TIENCONGs.Remove(n);
                            DataProvider.Ins.DB.SaveChanges();

                            MessageBox.Show("Xóa tiền công thành công!");

                            LoadTienCong();
                        }
                        else return;
                    }
                }
            }
        }

        private void dtgTienCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                txbMaTienCong.Text = selected_row.MaTienCong.ToString();
                txbTenTienCong.Text = selected_row.TenTienCong.ToString();
                txbGiaTienCong.Text = selected_row.GiaTienCong.ToString();
            }
        }
        #endregion


        #region HieuXe
        void LoadHieuXe()
        {
            ObservableCollection<HIEUXE> h = new ObservableCollection<HIEUXE>();
            var List = DataProvider.Ins.DB.HIEUXEs.ToList();
            foreach (var item in List)
            {
                HIEUXE hieuxe = new HIEUXE();
                hieuxe.MaHieuXe = item.MaHieuXe;
                hieuxe.TenHieuXe = item.TenHieuXe;
                hieuxe.GhiChu = item.GhiChu;
                h.Add(hieuxe);
                dtgHieuXe.ItemsSource = h;

            }
        }
        private void txbTenHangXe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<HIEUXE> h = new ObservableCollection<HIEUXE>();
            string text = txbTenHieuXe.Text.ToLower();

            var List = DataProvider.Ins.DB.HIEUXEs.ToList();
            foreach (var item in List)
            {
                string _tenHX = item.TenHieuXe.ToLower();
                if (_tenHX.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgHieuXe.ItemsSource = h;
        }

        private void BtnLamMoiHX_Click(object sender, RoutedEventArgs e)
        {
            txbTenHieuXe.Text = "";
            txbGhiChu.Text = "";
            txbMaHieuXe.Text = "";
            LoadHieuXe();
        }

        private void BtnThemHieuXe_Click(object sender, RoutedEventArgs e)
        {
            ThemHangXe themHangXe = new ThemHangXe(tbUserName.Text);
            themHangXe.ShowDialog();
            this.Close();
        }
        private void BtnXoaHX_Click(object sender, RoutedEventArgs e) //xoa hieu xe
        {
            if (string.IsNullOrEmpty(txbMaHieuXe.Text) )
            {
                MessageBox.Show("Hãy chọn mã xe muốn xóa!");
            }
            else
            {
                int MaHX = int.Parse(txbMaHieuXe.Text);

                var list = DataProvider.Ins.DB.XEs.Where(x => x.MaChuXe == MaHX).Count();
                if (list != 0)
                {
                    MessageBox.Show("Hiệu xe này đã được sử dụng. Không được xóa!");
                }
                else
                {
                    var n = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == MaHX).SingleOrDefault();
                    if (n != null)
                    {
                        MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa hiệu xe?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (r == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.HIEUXEs.Remove(n);
                            DataProvider.Ins.DB.SaveChanges();

                            MessageBox.Show("Xóa hiệu xe thành công!");

                            LoadHieuXe();
                        }
                        else return;
                    }
                }
            }
        }
        private void BtnSuaHX_Click(object sender, RoutedEventArgs e) //sua hieu xe
        {
            if (string.IsNullOrEmpty(txbMaHieuXe.Text) || string.IsNullOrEmpty(txbTenHieuXe.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ mã và tên hiệu xe");
            }
            else
            {
                int i = 0;
                var check = DataProvider.Ins.DB.HIEUXEs.ToList();
                foreach (var item in check)
                {
                    if (txbTenHieuXe.Text.ToLower() == item.TenHieuXe.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    int MaHX = int.Parse(txbMaHieuXe.Text);
                    var n = DataProvider.Ins.DB.HIEUXEs.Where(x => x.MaHieuXe == MaHX).SingleOrDefault();

                    n.MaHieuXe = MaHX;
                    n.TenHieuXe = txbTenHieuXe.Text;
                    if (string.IsNullOrEmpty(txbGhiChu.Text))
                    {
                        n.GhiChu = "Không có";
                    }
                    else
                    {
                        n.GhiChu = txbGhiChu.Text;
                    }
                    MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật hiệu xe?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Cập nhật thành công!");

                        LoadHieuXe();
                    }
                    else return;
                }
                else MessageBox.Show("Tên tiền công này đã tồn tại!");
                
            }
        }
        private void dtgHieuXe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                txbMaHieuXe.Text = selected_row.MaHieuXe.ToString();
                txbTenHieuXe.Text = selected_row.TenHieuXe.ToString();
                txbGhiChu.Text=selected_row.GhiChu.ToString();
            }
        }
        private void txbGhiChu_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<HIEUXE> h = new ObservableCollection<HIEUXE>();
            string text = txbGhiChu.Text.ToLower();

            var List = DataProvider.Ins.DB.HIEUXEs.ToList();
            foreach (var item in List)
            {
                string gc = item.GhiChu.ToLower();
                if (gc.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgHieuXe.ItemsSource = h;
        }
        #endregion


        #region NCC
        void LoadNCC()
        {
            ObservableCollection<NHACUNGCAP> h = new ObservableCollection<NHACUNGCAP>();
            var List = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
            foreach (var item in List)
            {
                NHACUNGCAP ncc = new NHACUNGCAP();
                ncc.MaNhaCungCap = item.MaNhaCungCap;
                ncc.TenNhaCungCap = item.TenNhaCungCap;
                ncc.SDTNhaCungCap = item.SDTNhaCungCap;
                ncc.EmailNhaCungCap = item.EmailNhaCungCap;
                ncc.DiachiNhaCungCap = item.DiachiNhaCungCap;
                h.Add(ncc);
                dtgNCC.ItemsSource = h;

            }
        }
        private void dtgNCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            dynamic selected_row = grid.SelectedItem;
            if (selected_row != null)
            {
                txbMaNCC.Text = selected_row.MaNhaCungCap.ToString();
                txbTenNCC.Text = selected_row.TenNhaCungCap.ToString();
                txbSDT.Text = selected_row.SDTNhaCungCap.ToString();
                txbEmail.Text = selected_row.EmailNhaCungCap.ToString();
                txbDiaChi.Text = selected_row.DiachiNhaCungCap.ToString();
            }
        }

        private void BtnLamMoiNCC_Click(object sender, RoutedEventArgs e)
        {
            txbMaNCC.Text = "";
            txbTenNCC.Text = "";
            txbSDT.Text = "";
            txbEmail.Text = "";
            txbDiaChi.Text = "";
            LoadNCC();
        }

        private void BtnThemNCC_Click(object sender, RoutedEventArgs e)
        {
            ThemNhaCungCap themNCC = new ThemNhaCungCap(tbUserName.Text);
            themNCC.ShowDialog();
            this.Show();
        }

        private void BtnXoaNCC_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaNCC.Text))
            {
                MessageBox.Show("Hãy chọn mã nhà cung cấp muốn xóa!");
            }
            else
            {
                int MaNCC = int.Parse(txbMaNCC.Text);

                var list = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MaNhaCungCap == MaNCC).Count();
                if (list != 0)
                {
                    MessageBox.Show("Nhà cung cấp này đã được sử dụng. Không được xóa!");
                }
                else
                {
                    var n = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == MaNCC).SingleOrDefault();
                    if (n != null)
                    {
                        MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn xóa nhà cung cấp?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (r == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.NHACUNGCAPs.Remove(n);
                            DataProvider.Ins.DB.SaveChanges();

                            MessageBox.Show("Xóa nhà cung cấp thành công!");

                            LoadNCC();
                        }
                        else return;
                    }
                }
            }
        }

        private void BtnSuaNCC_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaNCC.Text) || string.IsNullOrEmpty(txbTenNCC.Text) || string.IsNullOrEmpty(txbSDT.Text)
                || string.IsNullOrEmpty(txbEmail.Text)
                || string.IsNullOrEmpty(txbDiaChi.Text))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin!");
            }
            else
            {
                int i = 0;
                var check = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
                foreach (var item in check)
                {
                    if (txbTenNCC.Text.ToLower() == item.TenNhaCungCap.ToLower())
                    {

                        i++;
                    }
                }
                if (i == 0)
                {
                    int MaNCC = int.Parse(txbMaNCC.Text);
                    var n = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == MaNCC).SingleOrDefault();

                    n.MaNhaCungCap = MaNCC;
                    n.TenNhaCungCap = txbTenNCC.Text;
                    n.SDTNhaCungCap = txbSDT.Text;
                    n.EmailNhaCungCap = txbEmail.Text;
                    n.DiachiNhaCungCap = txbDiaChi.Text;

                    MessageBoxResult r = MessageBox.Show("Bạn chắc chắn muốn cập nhật nhà cung cấp?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Cập nhật thành công!");

                        LoadNCC();
                    }
                    else return;
                }
                else MessageBox.Show("Tên tiền công này đã tồn tại!");

                
            }
        }
        private void txbTenNCC_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<NHACUNGCAP> h = new ObservableCollection<NHACUNGCAP>();
            string text = txbTenNCC.Text.ToLower();

            var List = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
            foreach (var item in List)
            {
                string gc = item.TenNhaCungCap.ToLower();
                if (gc.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgNCC.ItemsSource = h;
        }

        private void txbSDT_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<NHACUNGCAP> h = new ObservableCollection<NHACUNGCAP>();
            string text = txbSDT.Text.ToLower();

            var List = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
            foreach (var item in List)
            {
                string gc = item.SDTNhaCungCap.ToLower();
                if (gc.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgNCC.ItemsSource = h;
        }

        private void txbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<NHACUNGCAP> h = new ObservableCollection<NHACUNGCAP>();
            string text = txbEmail.Text.ToLower();

            var List = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
            foreach (var item in List)
            {
                string gc = item.EmailNhaCungCap.ToLower();
                if (gc.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgNCC.ItemsSource = h;
        }

        private void txbDiaChi_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<NHACUNGCAP> h = new ObservableCollection<NHACUNGCAP>();
            string text = txbDiaChi.Text.ToLower();

            var List = DataProvider.Ins.DB.NHACUNGCAPs.ToList();
            foreach (var item in List)
            {
                string gc = item.DiachiNhaCungCap.ToLower();
                if (gc.Contains(text))
                {
                    h.Add(item);
                }
            }
            dtgNCC.ItemsSource = h;
        }


        private void txbSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }



        #endregion

    }
}
