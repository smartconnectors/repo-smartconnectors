using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartConnectors.Components.Salesforce.ViewModels;
using SmartConnectors.Components.Salesforce.Services;

namespace SmartConnectors.Api.Functions.Salesforce
{
    public static class Query_Get
    {
        [FunctionName("Query_Get")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "salesforce/objects")] HttpRequest req,
            ILogger log)
        {         

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<QueryRequest>(requestBody);
            /*
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");*/
            ForceClient client = new ForceClient(data);           

        //var result = await client.GetObjectBasicInfo(data.ObjectTypeName);
        var result = await client.DescribeGlobal();

            return (ActionResult)new OkObjectResult(result);
        }
    }
}
