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
using System.Linq;
using SmartConnectors.Api.Services;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public static class Workflow_Create
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Workflow_Create")]
        public static async Task<IActionResult> WorkflowCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "projects/{projectId}/workflows")] HttpRequest req,
            ILogger log, int projectId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Workflow>(requestBody);

            using (var da = new WorkflowDataAccess(conStr))
            {
                var result = await da.CreateAsync(data);

                if (result != null)
                {
                    await da.MarkWorkflowActiveAsync(result.Id, projectId);
                }
                return new OkObjectResult(result);
            }
        }

        [FunctionName("Workflow_Delete")]
        public static async Task<IActionResult> WorkflowDelete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "workflows/{workflowId}")] HttpRequest req,
            ILogger log, int workflowId)
        {
            using (var da = new WorkflowDataAccess(conStr))
            {
                await da.DeleteAsync(workflowId);
                return new OkObjectResult(new { Message = "Delete successfully" });
            }
        }

        [FunctionName("Workflow_Get")]
        public static async Task<IActionResult> WorkflowGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects/{projectId}/workflows")] HttpRequest req,
            ILogger log, int projectId)
        {
            using (var da = new WorkflowDataAccess(conStr))
            {
                var results = await da.GetByProject(projectId);

                if (results != null)
                {
                    if (results.Count() == 1)
                    {
                        await da.MarkWorkflowActiveAsync(results.FirstOrDefault().Id, projectId);
                    }
                }

                return new OkObjectResult(await da.GetByProject(projectId));
            }
        }

        [FunctionName("Workflow_RunOperation")]
        public static async Task<IActionResult> Workflow_RunOperation(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects/{projectId}/workflows/{workflowId}/run-operation")] HttpRequest req,
            ILogger log, int projectId, int workflowId)
        {
            var service = new WorkflowService(conStr, workflowId);
            dynamic result = await service.RunOperation();

            if (result.localFileSystem)
            {
                Stream fileContent = await new BlobStorageService("uploads").GetBlob(result.fileName);

                MemoryStream ms = (MemoryStream)fileContent;
                byte[] fileBytes = ms.ToArray();                

                return new OkObjectResult(new { data = fileBytes});
            }

            return new OkObjectResult(result);
        }

        [FunctionName("Workflow_ScheduleOperation")]
        public static async Task<IActionResult> Workflow_ScheduleOperation(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects/{projectId}/workflows/{workflowId}/schedule-operation")] HttpRequest req,
            ILogger log, int projectId, int workflowId)
        {
            using (var da = new WorkflowDataAccess(conStr))
            {
                var results = await da.GetOperations(workflowId);

                if (results.Any())
                {


                }

                return new OkObjectResult(await da.GetByProject(projectId));
            }
        }
    }
}
