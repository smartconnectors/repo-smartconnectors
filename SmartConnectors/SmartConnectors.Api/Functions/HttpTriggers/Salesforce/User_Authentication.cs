using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartConnectors.Components.Salesforce.ViewModels;

namespace SmartConnectors.Api.Functions.Salesforce
{
    public static class User_Authentication
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("sf_user_authentication")]
        public static async Task<IActionResult> UserAuthentication(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "workflows/{workflowId}/{connectorId}/salesforce-authenticate")] HttpRequest req,
            ILogger log, int workflowId, int connectorId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                LoginRequest data = JsonConvert.DeserializeObject<LoginRequest>(requestBody);

                var authService = new AuthenticationService(conStr, workflowId, connectorId);
                var authResult = await authService.Authenticate(data);

                if (authResult != null) {
                    return new OkObjectResult(new JsonResult(new { success = true, token = authResult.SessionId }));
                }

                return new OkObjectResult(new JsonResult(new { success = false,  Message = "Login Failed" }));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new JsonResult(new { success = false,  Message = ex.Message }));                
            }
        }

        [FunctionName("sf_refresh_token")]
        public static async Task<IActionResult> RefreshToken(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "workflows/{workflowId}/{connectorId}/salesforce-refresh-token")] HttpRequest req,
            ILogger log, int workflowId, int connectorId)
        {
            var authService = new AuthenticationService(conStr, workflowId, connectorId);
            var result = await authService.RefreshToken();

            if (result != null)
            {
                return new OkObjectResult(new JsonResult(new { token = result.SessionId }));
            }

            return new OkObjectResult(new JsonResult(new { token = "" }));
        }
    }
}
