using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static FinalGaraOto.ChiTietSuaChuaXe;

namespace FinalGaraOto
{
    public class XuatFileHoaDonThanhToan
    {
        public XuatFileHoaDonThanhToan(DataGrid datagrid, string Ngay, string Ten, string DiaChi, string SDT, string Email,string Tien)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Worksheet sheet1 = excel.Workbooks.Add(Missing.Value).Sheets[1];

            sheet1.Range["A1:F1"].Merge();
            sheet1.Range["A2:F2"].Merge();

            string title = "Hóa đơn sửa chữa xe";

            sheet1.Range["A1:F1"].Value = title;
            sheet1.Range["A1:F1"].Font.Bold = true;
            sheet1.Range["A1:F1"].Font.Size = 15;
            sheet1.Range["A1:F1"].AutoFit();
            sheet1.Range["A2:F2"].Value = Ngay;
            sheet1.Range["A3:F3"].Value = Ten;
            sheet1.Range["A4:F4"].Value = DiaChi;
            sheet1.Range["A5:F5"].Value = SDT;
            sheet1.Range["A6:F6"].Value = Email;
            sheet1.Range["A7:F7"].Value = Tien;

            for (int i = 0; i < datagrid.Columns.Count; i++)
            {
                Range myrange = (Range)sheet1.Cells[4, i + 1];
                myrange.Font.Bold = true;
                string header = datagrid.Columns[i].Header.ToString();
                sheet1.Columns[i + 1].ColumnWidth = header.Length + 5;
                myrange.Value = header;
                myrange.AutoFit();
            }

            for (int i = 0; i < datagrid.Items.Count; i++)
            {
                var item = datagrid.Items[i] as CHITIET;
                if (item != null)
                {
                    sheet1.Cells[i + 5, 1].Value = item.STT;
                    sheet1.Cells[i + 5, 2].Value = item.NoiDung;
                    sheet1.Cells[i + 5, 3].Value = item.TenVT;
                    sheet1.Cells[i + 5, 4].Value = item.SL;
                    sheet1.Cells[i + 5, 5].Value = item.Gia;
                    sheet1.Cells[i + 5, 5].Value = item.TenTC;
                    sheet1.Cells[i + 5, 5].Value = item.TC;
                    sheet1.Cells[i + 5, 5].Value = item.ThanhTien;

                }
            }
        }

    }
}
