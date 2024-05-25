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
    /// Interaction logic for ChiTietSuDungVTPT.xaml
    /// </summary>
    public partial class ChiTietSuDungVTPT : Window
    {
        string n, MaXe_;
        public ChiTietSuDungVTPT(string maUser, string MaXe)
        {
            InitializeComponent();
            LoadComboBox();
            n = maUser;
            MaXe_ = MaXe;

        }

        void LoadComboBox()
        {
            var List = DataProvider.Ins.DB.TIENCONGs.Select(x => x.TenTienCong).ToList();
            foreach (var item in List)
            {
                cbLoaiTC.Items.Add(item);
            }
        }
    }
}
