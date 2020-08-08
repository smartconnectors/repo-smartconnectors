using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartConnectors.Models;
using SmartConnectors.DataAccess;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public static class Operations_Create
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Operations_Create")]
        public static async Task<IActionResult> OperationsCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "operations")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Operation>(requestBody);

            data.Content = JsonConvert.SerializeObject(data.Content);
            using (var da = new OperationsDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("Operations_Get")]
        public static async Task<IActionResult> OperationsGet(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "operations")] HttpRequest req,
           ILogger log)
        {
            using (var da = new OperationsDataAccess(conStr))
            {
                var workflowConnectorId = new Microsoft.Extensions.Primitives.StringValues();
                req.Query.TryGetValue("workflowConnectorId", out workflowConnectorId); 

                return new OkObjectResult(await da.GetOperations(Convert.ToInt32(workflowConnectorId)));

            }
        }

        [FunctionName("Operations_Update")]
        public static async Task<IActionResult> OperationsUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "operations/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Operation>(requestBody);

            data.Content = JsonConvert.SerializeObject(data.Content);
            using (var da = new OperationsDataAccess(conStr))
            {
                return new OkObjectResult(await da.UpdateAsync(data));
            }
        }
    }
}
