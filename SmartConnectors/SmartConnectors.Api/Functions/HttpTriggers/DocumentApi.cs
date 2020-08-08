using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.DataAccess;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public class DocumentApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Document_Get")]
        public static async Task<IActionResult> ConnectorGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "documents")] HttpRequest req,
            ILogger log)
        {
            using (var da = new DocumentDataAccess(conStr))
            {
                var result = await da.Get();
                return new OkObjectResult(result.ToList().OrderByDescending(p => p.CreatedDate));
            }
        }
    }
}
