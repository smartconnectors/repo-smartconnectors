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
using SmartConnectors.DataAccess.SqlTemplates;

namespace SmartConnectors.Api.Functions.HttpTriggers
{
    public class ProjectApi
    {
        private readonly static string conStr = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DatabaseConnection);

        [FunctionName("Project_Create")]
        public static async Task<IActionResult> ProjectCreate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "projects")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Project>(requestBody);


            using (var da = new ProjectDataAccess(conStr))
            {
                return new OkObjectResult(await da.CreateAsync(data));
            }
        }

        [FunctionName("Project_Delete")]
        public static async Task<IActionResult> ProjectDelete(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "projects/{projectId}")] HttpRequest req,
           ILogger log, int projectId)
        {
            using (var da = new ProjectDataAccess(conStr))
            {
                return new OkObjectResult(await da.QueryAsync<Project>(string.Format(SQLStatements.TemplateDelete, "Projects", projectId)));
            }
        }

        [FunctionName("Project_Get")]
        public static async Task<IActionResult> ProjectGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects")] HttpRequest req,
            ILogger log)
        {
            using (var da = new ProjectDataAccess(conStr))
            {
                return new OkObjectResult(await da.Get());
            }
        }
    }
}
