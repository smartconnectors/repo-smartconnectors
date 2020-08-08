using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.DataAccess;
using System;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public static class ConnectorApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Connector_Get")]
        public static async Task<IActionResult> ConnectorGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "connectors")] HttpRequest req,
            ILogger log)
        {
            using (var da = new ConnectorDataAccess(conStr))
            {
                return new OkObjectResult(await da.Get());
            }
        }
    }
}
