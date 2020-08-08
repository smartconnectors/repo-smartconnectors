using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.Api.Services;

namespace SmartConnectors.Api.Functions.Excel
{
    public static class ExtractExcel_Metadata_Get
    {
        [FunctionName("ExtractExcel_Metadata_Get")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "documents/{docId}/extract-excel-metadata")] HttpRequest req,
            ILogger log, int docId)
        {
            return new OkObjectResult(await new DocumentService().ExtractMetaData(docId));
        }
    }
}
