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
using System;

namespace SmartConnectors.Api.Functions.Salesforce
{
    public static class Get_SObject_Metadata
    {
        [FunctionName("Get_SObject_Metadata")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "salesforce/objects/describe")] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<QueryRequest>(requestBody);

            try
            {
                ForceClient client = new ForceClient(data);

                //var result = await client.GetObjectBasicInfo(data.ObjectTypeName);
                var result = await client.GetObjectDescribe(data.ObjectTypeName);

                return (ActionResult)new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return (ActionResult)new BadRequestObjectResult(ex.Message);
                throw;
            }
            
        }
    }
}
