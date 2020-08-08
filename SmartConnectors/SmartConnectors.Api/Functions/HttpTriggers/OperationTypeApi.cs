using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.Api.Services;
using SmartConnectors.Models;

namespace SmartConnectors.Api.Functions.HttpTriggers
{

    public static class OperationTypeApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);


        [FunctionName("OperationTypes_Get")]
        public static async Task<IActionResult> OperationTypesGet(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "operationTypes")] HttpRequest req,
           ILogger log)
        {
            using (var service = new BaseService<OperationType>(conStr))
            {
                return new OkObjectResult(await service.Get());
            }
        }
    }
}
