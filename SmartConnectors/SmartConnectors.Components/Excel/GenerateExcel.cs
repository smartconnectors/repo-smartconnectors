using System.Collections.Generic;
using System.IO;

using OfficeOpenXml;
using System.Reflection;

namespace SmartConnectors.Components.Excel
{
    public class GenerateExcel
    {
        /*
       public void SaveToExcel(List<Response> responses)
       {
           string responsePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Responses";

           DataTable table = new DataTable();
           table.Columns.Add("Response", typeof(string));
           table.Columns.Add("ResponseDate", typeof(DateTime));
           table.Columns.Add("CellNumber", typeof(string));

           foreach (var response in responses)
               table.Rows.Add(response.Response, response.ResponseDate, response.CellNumber);

           string filename = DateTime.Now.ToString("yyyy-MM-dd HHmmssfff") + ".xlsx";
           filename = Path.Combine(responsePath, filename);
           Directory.CreateDirectory(responsePath);

           using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 0x2000, false))
           {
               using (ExcelPackage pck = new ExcelPackage(stream))
               {
                   ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Responses");
                   ws.Cells["A1"].LoadFromDataTable(table, true);
                   pck.Save();
               }
           }
       }*/
        public void WriteListToExcel<T>(List<T> objectData)
        {
            //Declarations of our Excel package, workbook and sheet
            var package = new ExcelPackage();
            var workbook = package.Workbook;
            var workSheet = workbook.Worksheets.Add("MyData");

            //Retrieval of our objects properties
            var objectProperties = typeof(T).GetProperties();

            var headerCount = 0;

            //Write out the object properties as headers in the excel sheet
            foreach (var heading in objectProperties)
            {
                headerCount++;
                workSheet.Cells[1, headerCount].Value = heading.Name;
            }

            var rowCounter = 1;
            var columnCounter = 0;
            //Write the data for each item in the list to our spreadsheet
            foreach (var item in objectData)
            {
                rowCounter++;
                columnCounter = 0;
                WriteEachLine(workSheet, rowCounter, columnCounter, objectProperties, item);
            }

            //For the purpose of this example I save the file to my Desktop
            const string path = @"C:\Users\PPanday\Desktop\test1.xlsx";
            var stream = File.Create(path);
            package.SaveAs(stream);
            stream.Close();

        }
        //Function for writing each line to the spreadsheet
        public void WriteEachLine<T>(ExcelWorksheet excelWorksheet, int rowCounter, int columnCounter, PropertyInfo[] propertyInfos, T objectData)
        {
            foreach (var property in propertyInfos)
            {
                columnCounter++;
                var val = property.GetValue(objectData, null);
                excelWorksheet.Cells[rowCounter, columnCounter].Value = val ?? "";
            }
        }
    }
}
