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
using System.Collections.Generic;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public static class WorkflowConnectorApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("WorkflowConnector_Create")]
        public static async Task<IActionResult> WorkflowConnectorCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/connectors")] HttpRequest req,
            ILogger log, int workflowId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<WorkflowConnector>(requestBody);

            using (var da = new WorkflowConnectorDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateMultiAsync(data));
            }
        }        

        [FunctionName("WorkflowConnector_Get")]
        public static async Task<IActionResult> WorkflowConnectorGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/connectors")] HttpRequest req,
            ILogger log, int workflowId)
        {
            using (var da = new WorkflowDataAccess(conStr))
            {
                var workflow = await da.Get(workflowId);

                if (workflow != null)
                {
                    await da.MarkWorkflowActiveAsync(workflow.Id, workflow.ProjectId);
                }
            }

            using (var da = new WorkflowConnectorDataAccess(conStr))
            {
                return new OkObjectResult(await da.GetByWorkflow(workflowId));
            }
        }

        [FunctionName("WorkflowConnector_Update")]
        public static async Task<IActionResult> WorkflowConnectorUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "workflows/{workflowId}/connectors/update-position")] HttpRequest req,
            ILogger log, int workflowId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<ConnectionPosition>(requestBody);

            using (var da = new WorkflowConnectorDataAccess(conStr))
            {
                await da.UpdateConnectorPositionAsync(workflowId, data);
                return new OkObjectResult(new { Message = "Updated Succesfully" });
            }
        }
    }

    public class ConnectionPosition
    {
       public List<int> connectorIds { get; set; }
    }
}
