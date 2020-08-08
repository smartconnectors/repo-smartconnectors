using System;
using System.Linq;
using System.Threading.Tasks;
using SmartConnectors.Components.Salesforce.Model.Xml;
using SmartConnectors.Components.Salesforce.Services;
using SmartConnectors.Components.Salesforce.ViewModels;
using SmartConnectors.DataAccess;
using SmartConnectors.Models;

namespace SmartConnectors.Api.Functions.Salesforce
{
    public class AuthenticationService
    {
        private readonly string _conStr;
        private readonly int _workflowId;
        private readonly int _connectorId;

        public AuthenticationService(string conStr, int workflowId, int connectorId)
        {
            _conStr = conStr;
            _workflowId = workflowId;
            _connectorId = connectorId;
        }

        public async Task<LoginResult> Authenticate(LoginRequest data)
        {
            SalesforceClient_Authentication client = new SalesforceClient_Authentication();

            var result = await client.LoginAsync(data);

            if (result != null)
            {
                await SaveCredentials(data, _workflowId, _connectorId);

                return result;
            }

            return result;
        }

        public async Task<LoginResult> RefreshToken()
        {
            try
            {
                using (var da = new CredentialDataAccess(_conStr))
                {
                    var savedCred = (await da.GetByParent<Workflow, Connector>(_workflowId, _connectorId)).FirstOrDefault();
                        
                    if (savedCred != null)
                    {
                        LoginRequest data = new LoginRequest
                        {
                            Username = savedCred.UserName,
                            Password = savedCred.Password,
                            SecurityToken = savedCred.SecretToken
                        };

                        SalesforceClient_Authentication client = new SalesforceClient_Authentication();

                        var result = await client.LoginAsync(data);

                        return result;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task SaveCredentials(LoginRequest data, int workflowId, int connectorId)
        {
            using (var da = new CredentialDataAccess(_conStr))
            {
                var existingCred = (await da.GetByParent<Workflow, Connector>(workflowId, connectorId)).FirstOrDefault();

                if (existingCred != null)
                {
                    existingCred.UserName = data.Username;
                    existingCred.Password = data.Password;
                    existingCred.SecretToken = data.SecurityToken;

                    await da.UpdateAsync(existingCred);
                }
                else
                {
                    var cred = new Credential
                    {
                        UserName = data.Username,
                        Password = data.Password,
                        SecretToken = data.SecurityToken,
                        HostUrl = data.ServerHost,
                        OrgId = data.OrgId,
                        EndPointName = data.EndpointName,
                        WorkflowId = workflowId,
                        ConnectorId = connectorId
                    };

                    await da.CreateAsync(cred);
                }
            }
        }
    }
}