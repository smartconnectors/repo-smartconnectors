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
    public class SObject_QueryString
    {
        [FunctionName("SObject_QueryString")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "salesforce/objects/query")] HttpRequest req,
             ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<QueryRequest>(requestBody);

            ForceClient client = new ForceClient(data);

            try
            {
                //var result = await client.GetObjectBasicInfo(data.ObjectTypeName);
                var result = await client.Query<dynamic>(data.QueryString, data.QueryAll = false);

                //     RemoveAttribute.Strip(result);
                return (ActionResult)new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }
    }
}
