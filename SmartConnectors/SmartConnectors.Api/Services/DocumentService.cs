using Microsoft.AspNetCore.Http;
using SmartConnectors.Components.Excel;
using SmartConnectors.DataAccess;
using SmartConnectors.Models;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Services
{
    public class DocumentService : BaseService<Document>, IDocumentService
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        public DocumentService() : base(conStr)
        {

        }

        public async Task<int> Upload(HttpRequest req, int workflowId)
        {
            var file = req.Form.Files[0];
            byte[] bytes = null;

            if (file.Length > 0)
            {

                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fileSize = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Size.ToString();
                string ext = Path.GetExtension(fileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.OpenReadStream().CopyTo(ms);
                    bytes = ms.ToArray();
                }

                return await CreateDocument(fileName, fileSize, bytes, ext, workflowId, "");

            }

            return 0;
        }

        public async Task<int> CreateDocument(string fileName, string fileSize, byte[] bytes, string ext, int workflowId, string webUrl)
        {

            var newDoc = new Document
            {
                FileName = fileName.ToUpper(),
                FileSize = fileSize,
                Content = bytes,
                Type = ext.ToUpper(),
                CreatedDate = DateTime.Now,
                IsProcessed = false,
                WebUrl = webUrl,
                WorkflowId = workflowId
            };

            using (var da = new DocumentDataAccess(conStr))
            {
                try
                {
                    return await da.CreateAsync(newDoc);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> ExtractMetaData(int docId)
        {
            var doc = await Get(docId);
            var bs = new BlobStorageService("uploads");

            if (doc != null)
            {
                var em = new ExcelManager();

                return em.ExtractMetaData(await bs.GetBlob(doc.WebUrl));
            }

            return "";
        }
    }
}
