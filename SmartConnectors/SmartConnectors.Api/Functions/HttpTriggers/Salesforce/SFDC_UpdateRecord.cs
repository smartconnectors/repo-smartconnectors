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
    public class SFDC_UpdateRecord
    {
        [FunctionName("SFDC_UpdateRecord")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "salesforce/objects/update")] HttpRequest req,
             ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<QueryRequest>(requestBody);

            ForceClient client = new ForceClient(data);
            ////var result = await client.GetObjectBasicInfo(data.ObjectTypeName);
           //UpdateRecord<T>(string sObjectTypeName, string objectId, T sObject, Dictionary<string, string> customHeaders = null)

            await client.UpdateRecord(data.ObjectTypeName, data.ObjectId , data.SfObject, null);

            return (ActionResult)new OkObjectResult(new { Message = "Updated succesfully" });
        }
    }
}
