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
    public class SFDC_CreateRecord
    {
        [FunctionName("SFDC_InsertRecord")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "salesforce/objects/createRecord")] HttpRequest req,
             ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<QueryRequest>(requestBody);

            ForceClient client = new ForceClient(data);
            ////var result = await client.GetObjectBasicInfo(data.ObjectTypeName);
            //CreateRecord<T>(string sObjectTypeName, T sObject, Dictionary<string, string> customHeaders = null)
            var result = await client.CreateRecord(data.sObjectTypeName, data.sObject,null);

            return (ActionResult)new OkObjectResult(result);
        }
    }
}
