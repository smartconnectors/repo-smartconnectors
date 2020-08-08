using SmartConnectors.DataAccess;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SmartConnectors.Components.Excel;
using System.IO;

namespace SmartConnectors.Api.Services.Connectors
{
    public class ExcelService : OperationLogService
    {
        private readonly string blobContainer = Environment.GetEnvironmentVariable(EnvironmentVariableNames.BlobContainer);

        private readonly string _conStr;

        public ExcelService(string conStr, int workflowId) : base(conStr, workflowId)
        {
            _conStr = conStr;
        }
        public async Task<IEnumerable<Dictionary<string, object>>> ExecuteSourceOperation(ActiveOperation ops)
        {
            // TODO
            // 1) Read file content from blob storage
            // 2> Return List from excel data

            var intermediateJson = JsonConvert.DeserializeObject<string>(ops.Content);
            var content = JsonConvert.DeserializeObject<ExcelDocument>(intermediateJson);

            using (var da = new DocumentDataAccess(_conStr))
            {
                var doc = await da.Get(Convert.ToInt32(content.DocumentId));

                if (doc != null)
                {
                    var bs = new BlobStorageService(blobContainer);
                    var blobContent = await bs.GetBlob(doc.WebUrl);

                    var excelData = new ExcelManager().ExtractData(blobContent);

                    return excelData;
                }
            }

            return null;
        }

        public async Task<string> ExecuteTargetOperation(string fileName, dynamic data)
        {            
            var bs = new BlobStorageService(blobContainer);
            MemoryStream excelData = new GenerateDynamicExcel().GenerateSpreadSheet(data);
            try
            {
                var blobFileName = await bs.UploadFileToBlobAsync(fileName, excelData.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                await LogInfo("Excel upload succesful to storage");
                return blobFileName;
            }
            catch (Exception ex)
            {
                await LogError("Excel upload Failed", ex.Message);
                throw;
            }            
        }
    }

    public class ExcelDocument
    {
        public string DocumentId { get; set; }
    }

    public class ExcelConfig
    {
        public string FileName { get; set; }
        public bool IsLocalFileSystem { get; set; }
    }
}
