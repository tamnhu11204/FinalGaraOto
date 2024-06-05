﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FinalGaraOto
{
    public class ExportToExcel_BCDoanhThu
    {
        public ExportToExcel_BCDoanhThu(DataGrid datagrid, DateTime dt)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Worksheet sheet1 = excel.Workbooks.Add(Missing.Value).Sheets[1];

            sheet1.Range["A1:D1"].Merge();
            sheet1.Range["A2:D2"].Merge();

            string title = "Báo cáo doanh thu";
            sheet1.Range["A1:D1"].Value= title;
            sheet1.Range["A1:D1"].Font.Bold= true;
            sheet1.Range["A1:D1"].Font.Size= 15;
            sheet1.Range["A1:D1"].AutoFit();
            sheet1.Range["A2:C2"].Value= dt.ToShortDateString();

            for (int i = 0; i< datagrid.Columns.Count; i++)
            {
                Range myrange = (Range)sheet1.Cells[4, i+1];
                myrange.Font.Bold= true;
                string header = datagrid.Columns[i].Header.ToString();
                sheet1.Columns[i+1].ColumnWidth= header.Length+5;
                myrange.Value= header;
                myrange.AutoFit();
            }

            for (int i = 0; i<datagrid.Items.Count; i++)
            {
                var item = datagrid.Items[i] as BCDT;
                if (item != null)
                {
                    sheet1.Cells[i+5, 1].Value= item.hieuxe;
                    sheet1.Cells[i+5, 2].Value= item.tenhieuxe;
                    sheet1.Cells[i+5, 3].Value= item.soluotsua;
                    sheet1.Cells[i+5, 4].Value= item.thanhtien;
                    sheet1.Cells[i+5, 5].Value= item.tile;

                }
            }
        }

    }
}
