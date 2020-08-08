using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.Api.Services;
using System.Net.Http.Headers;

namespace SmartConnectors.Api.Functions
{
    public static class FileUploadApi
    {
        [FunctionName("FileUpload_Post")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/upload")] HttpRequest req,
            ILogger log, int workflowId)
        {
            BlobStorageService objBlobService = new BlobStorageService("uploads");

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

                var filePath = await objBlobService.UploadFileToBlobAsync(fileName, bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                using (var ds = new DocumentService()) {
                    return new OkObjectResult(await ds.CreateDocument(fileName, fileSize, bytes, ext, workflowId, filePath));
                }                    
            }

            return new OkObjectResult(new { Message = "No file selected" });

        }
    }
}
