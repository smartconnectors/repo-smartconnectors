using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace SmartConnectors.Components.Excel
{
    public class GenerateDynamicExcel
    {
        public DataTable ToDataTable<T>(List<dynamic> items)
        {
            DataTable dtDataTable = new DataTable();
            if (items.Count == 0) return dtDataTable;

            ((IEnumerable)items[0]).Cast<dynamic>().Select(p => p.Name).ToList().ForEach(col => {
                    dtDataTable.Columns.Add(col);
            });

            ((IEnumerable)items).Cast<dynamic>().ToList().
                ForEach(data =>
                {
                    DataRow dr = dtDataTable.NewRow();
                    ((IEnumerable)data).Cast<dynamic>().ToList().ForEach(Col => { dr[Col.Name] = Col.Value; });
                    dtDataTable.Rows.Add(dr);
                });
            return dtDataTable;
        }

        public MemoryStream GenerateSpreadSheet(List<dynamic> data)
        {
            ExcelPackage package = new ExcelPackage();
            var ms = new MemoryStream();
            try
            {
                DataSet dsemp = new DataSet();

                dsemp.Tables.Add(ToDataTable<dynamic>(data));
                ExcelWorksheet ExcelWorkSheet = package.Workbook.Worksheets.Add("result.xlxs");
                if (dsemp.Tables.Count > 0)
                {  
                    //Writing Columns Name in Excel Sheet
                    int r = 2; // Initialize Excel Row Start Position = 4
                    int d = 0; // Initialize Data Start Position = 4

                    for (int col = 1; col <= dsemp.Tables[0].Columns.Count; col++)
                    {
                        ExcelWorkSheet.Cells[r, col].Value = dsemp.Tables[0].Columns[d].ColumnName;
                        d++;
                    }
                    r++;

                    for (int row = 0; row < dsemp.Tables[0].Rows.Count; row++)
                    {
                        d = 0; // Initialize Data Start Position = 4
                               // Excel row and column start positions for writing Row=1 and Col=1
                        for (int col = 1; col <= dsemp.Tables[0].Columns.Count; col++)
                        {
                            ExcelWorkSheet.Cells[r, col].Value = dsemp.Tables[0].Rows[row][d];
                            ExcelWorkSheet.Cells[r, col].AutoFitColumns();
                            d++;
                        }
                        r++;
                    }
                }

                if (dsemp.Tables[0].Rows.Count > 0 || dsemp.Tables[1].Rows.Count > 0)
                {
                    package.Workbook.Properties.Title = "Sample";

                    ms = new MemoryStream(package.GetAsByteArray());
                }

                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                package.Dispose();
            }
        }
    }
}
