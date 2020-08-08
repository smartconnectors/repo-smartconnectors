using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartConnectors.Models;
using SmartConnectors.Api.Services;
using System.Linq;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public static class LookupApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Lookups_Get")]
        public static async Task<IActionResult> LookupsGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "lookups/{type}")] HttpRequest req,
            ILogger log, string type)
        {
            using (var service = new BaseService<Lookup>(conStr))
            {
                return new OkObjectResult((await service.Get()).Where(l => l.Type == type));
            }
        }       
    }
}
