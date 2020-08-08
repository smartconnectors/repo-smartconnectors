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
    public static class Schedulers_Create
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Schedulers_Create")]
        public static async Task<IActionResult> SchedulersCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/schedulers")] HttpRequest req,
            ILogger log, int workflowId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Scheduler>(requestBody);
            data.WorkflowId = workflowId;

            using (var da = new SchedulersDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("Schedulers_Get")]
        public static async Task<IActionResult> SchedulersGet(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/schedulers")] HttpRequest req,
           ILogger log, int workflowId)
        {
            using (var da = new SchedulersDataAccess(conStr))
            {
                return new OkObjectResult(await da.GetSchedulers(Convert.ToInt32(workflowId)));
            }
        }

        [FunctionName("Schedulers_Update")]
        public static async Task<IActionResult> SchedulersUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "workflows/{workflowId}/schedulers/{id}")] HttpRequest req,
            ILogger log, int workflowId, int id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Scheduler>(requestBody);
            data.WorkflowId = workflowId;

            using (var da = new SchedulersDataAccess(conStr))
            {
                return new OkObjectResult(await da.UpdateAsync(data));
            }
        }

        [FunctionName("Schedulers_Delete")]
        public static async Task<IActionResult> SchedulersDelete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "workflows/{workflowId}/schedulers/{id}")] HttpRequest req,
            ILogger log, int workflowId, int id)
        {            
            using (var da = new SchedulersDataAccess(conStr))
            {
                return new OkObjectResult(await da.DeleteAsync(id));
            }
        }
    }
}
