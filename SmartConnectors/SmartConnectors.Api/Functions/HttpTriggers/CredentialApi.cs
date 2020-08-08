using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartConnectors.DataAccess;
using SmartConnectors.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public class CredentialApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Credentials_Create")]
        public static async Task<IActionResult> CredentialsCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/{connectorId}/credentials")] HttpRequest req,
            int workflowId, int connectorId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Credential>(requestBody);
            data.ConnectorId = connectorId;
            data.WorkflowId = workflowId;

            using (var da = new CredentialDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("Credentials_Get")]
        public static async Task<IActionResult> CredentialsGet(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/{connectorId}/credentials")] HttpRequest req,
           int workflowId, int connectorId)
        {
            using (var da = new CredentialDataAccess(conStr))
            {
                return new OkObjectResult(await da.GetByParent<Workflow, Connector>(workflowId, connectorId));
            }
        }

        [FunctionName("Credentials_Update")]
        public static async Task<IActionResult> CredentialsUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "workflows/{workflowId}/{connectorId}/{credentials}/{id}")] HttpRequest req,
           int workflowId, int connectorId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Credential>(requestBody);
            data.ConnectorId = connectorId;
            data.WorkflowId = workflowId;

            using (var da = new CredentialDataAccess(conStr))
            {
                return new OkObjectResult(await da.UpdateAsync(data));
            }
        }

        [FunctionName("Credentials_Delete")]
        public static async Task<IActionResult> CredentialsDelete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "workflows/{workflowId}/{connectorId}/credentials/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            using (var da = new CredentialDataAccess(conStr))
            {
                return new OkObjectResult(await da.DeleteAsync(id));
            }
        }
    }
}
