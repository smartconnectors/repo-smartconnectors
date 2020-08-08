using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using SmartConnectors.DataAccess;
using SmartConnectors.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public class TransformationApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Transformation_Create")]
        public static async Task<IActionResult> TransformationCreate(
                        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/transformations")] HttpRequest req,
                        int workflowId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Transformation>(requestBody);
            data.WorkflowId = workflowId;

            using (var da = new TransformationDataAccess(conStr))
            {               
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("Transformation_Update")]
        public static async Task<IActionResult> TransformationUpdate(
                       [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "workflows/{workflowId}/transformations/{id}")] HttpRequest req,
                       int workflowId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Transformation>(requestBody);
            data.WorkflowId = workflowId;

            using (var da = new TransformationDataAccess(conStr))
            {
                return new OkObjectResult(await da.UpdateAsync(data));
            }
        }

        [FunctionName("Transformations_Get")]
        public static async Task<IActionResult> TransformationsGet(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/transformations")] HttpRequest req,
          int workflowId)
        {
            using (var da = new TransformationDataAccess(conStr))
            {
                return new OkObjectResult(await da.GetByWorkflow(Convert.ToInt32(workflowId)));
            }
        }
    }
}
