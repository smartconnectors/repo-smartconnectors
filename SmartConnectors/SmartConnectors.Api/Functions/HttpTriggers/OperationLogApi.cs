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
    public static class OperationLogs_Create
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("OperationLogs_Create")]
        public static async Task<IActionResult> OperationLogsCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/operation-logs")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<OperationLog>(requestBody);

            data.Content = JsonConvert.SerializeObject(data.Content);
            using (var da = new OperationLogDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("OperationLogs_Get")]
        public static async Task<IActionResult> OperationLogsGet(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/operation-logs")] HttpRequest req,
           ILogger log, string workflowId)
        {
            using (var da = new OperationLogDataAccess(conStr))
            {
                //var workflowConnectorId = new Microsoft.Extensions.Primitives.StringValues();
               // req.Query.TryGetValue("workflowConnectorId", out workflowConnectorId); 

                return new OkObjectResult(await da.GetOperationLogs(Convert.ToInt32(workflowId)));
            }
        }       
    }
}
