using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ExcelDataReader;

namespace SmartConnectors.Components.Excel
{
    public class ExcelManager
    {
        public ExcelManager()
        {

        }
      
        public string ExtractMetaData(Stream excelContent)
        {
            var serializeOptions = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var excelData = ParseExcel(excelContent);
            string json = JsonConvert.SerializeObject(excelData, serializeOptions);

            return json;
        }

        public string ExtractMetaData(byte[] excelContent)
        {
            var serializeOptions = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var excelData = ParseExcel(new MemoryStream(excelContent));
            string json = JsonConvert.SerializeObject(excelData, serializeOptions);

            return json;
        }

        public TableMetaData ParseExcel(Stream document)
        {
            using (var reader = ExcelReaderFactory.CreateReader(document))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = true,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });
                return MapDataset(result.Tables.Cast<DataTable>().First());
            }
        }

        public TableMetaData MapDataset(DataTable dt)
        {
            var tblMetaData = new TableMetaData();

            tblMetaData.TableName = dt.Rows[0].ItemArray.FirstOrDefault().ToString();

            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName != "attributes__type" && col.ColumnName != "attributes__url")
                {
                    tblMetaData.Columns.Add(col.ColumnName);
                }
            }

            return tblMetaData;
        }

        public IEnumerable<Dictionary<string, object>> ExtractData(Stream excelContent)
        {
            var excelData = ParseExcelData(excelContent);
            //string json = JsonConvert.SerializeObject(excelData);

            return excelData;
        }

        public IEnumerable<Dictionary<string, object>> ParseExcelData(Stream document)
        {
            using (var reader = ExcelReaderFactory.CreateReader(document))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = true,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });
                return MapDatasetData(result.Tables.Cast<DataTable>().First());
            }
        }

        public IEnumerable<Dictionary<string, object>> MapDatasetData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                yield return row;
            }
        }
    }

    public class TableMetaData
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }

        public TableMetaData()
        {
            Columns = new List<string>();
        }
    }
}
