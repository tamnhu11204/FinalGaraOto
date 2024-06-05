using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static FinalGaraOto.HoaDonThanhToanPhuTung;

namespace FinalGaraOto
{
    public class XuatHoaDonVTPT
    {
        public XuatHoaDonVTPT(DataGrid datagrid, string NgayNH, string TenNCC, string DiaChi, string SDT, string Email, string Tien)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Worksheet sheet1 = excel.Workbooks.Add(Missing.Value).Sheets[1];

            sheet1.Range["A1:G1"].Merge();
            sheet1.Range["A2:G2"].Merge();
            sheet1.Range["A3:G3"].Merge();
            sheet1.Range["A4:G4"].Merge();
            sheet1.Range["A5:G5"].Merge();
            sheet1.Range["A6:G6"].Merge();
            sheet1.Range["A7:G7"].Merge();

            string title = "Hóa đơn thanh toán vật tư phụ tùng";

            sheet1.Range["A1:G1"].Value = title;
            sheet1.Range["A1:G1"].Font.Bold = true;
            sheet1.Range["A1:G1"].Font.Size = 15;
            //sheet1.Range["A1:F1"].AutoFit();
            sheet1.Range["A2:G2"].Value = NgayNH;
            sheet1.Range["A3:G3"].Value = TenNCC;
            sheet1.Range["A4:G4"].Value = DiaChi;
            sheet1.Range["A5:G5"].Value = SDT;
            sheet1.Range["A6:G6"].Value = Email;
            sheet1.Range["A7:G7"].Value = Tien;

            for (int i = 0; i < datagrid.Columns.Count; i++)
            {
                Range myrange = (Range)sheet1.Cells[7, i + 1];
                myrange.Font.Bold = true;
                string header = datagrid.Columns[i].Header.ToString();
                sheet1.Columns[i + 1].ColumnWidth = header.Length + 7;
                myrange.Value = header;
                //myrange.AutoFit();
            }

            for (int i = 0; i < datagrid.Items.Count; i++)
            {
                var item = datagrid.Items[i] as ChiTietNhapVatTuPhuTungs;
                if (item != null)
                {
                    sheet1.Cells[i + 7, 1].Value = item.STT;
                    sheet1.Cells[i + 7, 2].Value = item.MaVTPT;
                    sheet1.Cells[i + 7, 3].Value = item.TenVT;
                    sheet1.Cells[i + 7, 4].Value = item.SL;
                    sheet1.Cells[i + 7, 5].Value = item.MaNhapHang;
                    sheet1.Cells[i + 7, 6].Value = item.GiaNhap;
                    sheet1.Cells[i + 7, 7].Value = item.ThanhTien;

                }
            }
        }

    }
}
