using Newtonsoft.Json;
using SmartConnectors.Api.Functions.Salesforce;
using SmartConnectors.Components.Salesforce.Services;
using SmartConnectors.Components.Salesforce.ViewModels;
using SmartConnectors.DataAccess;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Services.Connectors
{
    public class SalesforceService : OperationLogService
    {
        private readonly int _workflowId;
        private readonly string _conStr;

        public SalesforceService(string conStr, int workflowId) : base(conStr, workflowId)
        {
            _workflowId = workflowId;
            _conStr = conStr;
        }

        public async Task<dynamic> ExecuteSourceOperation(string intermediateJson)
        {
            ForceClient client = new ForceClient(await GetClientOptions());

            dynamic result;

            try
            {
                result = await client.Query<dynamic>(intermediateJson, false);
                await LogInfo("Salesforce Query Succesfully Executed");
            }
            catch (Exception ex)
            {
                await LogError("Salesforce Query Execution Failed", ex.Message);
                throw;
            }

            return result;
        }

        public async Task<dynamic> ExecuteTargetOperation(ActiveOperation ops, List<IDictionary<string, object>> sfObject)
        {
            dynamic result = null;

            ForceClient client = new ForceClient(await GetClientOptions());
            var intermediateJson = JsonConvert.DeserializeObject<string>(ops.Content);

            try
            {
                foreach (var obj in sfObject)
                {
                    var id = obj.Where(s => s.Key == "Id").Select(s => s.Value).FirstOrDefault().ToString();
                    obj.Remove("Id");

                    await client.UpdateRecord(intermediateJson, id, obj, null);
                    await LogInfo("Salesforce Updated Succesfully");
                }
            }
            catch (System.Exception ex)
            {
                await LogError("Salesforce Updatation Failed", ex.Message);
                throw ex;
            }

            return result;
        }


        private async Task<QueryRequest> GetClientOptions()
        {
            var sfConnectorId = 0;

            using (var da = new ConnectorDataAccess(_conStr))
            {
                var connectorList = await da.Get();
                sfConnectorId = connectorList.FirstOrDefault(c => c.Name == "Salesforce").Id;
            }

            var authService = new AuthenticationService(_conStr, _workflowId, sfConnectorId);
            var result = await authService.RefreshToken();

            if (result != null)
            {
                var arr = result.MetadataServerUrl.Split('/');

                var req = new QueryRequest
                {
                    Token = result.SessionId,
                    ApiVersion = "v" + arr[6],
                    ServerUri = result.MetadataServerUrl,
                    ClientName = "",
                    InstanceUrl = string.Format("https://{0}", new Uri(result.ServerUrl).Host)
                };

                return req;
            }

            return null;
        }
    }
}
