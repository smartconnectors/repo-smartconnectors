using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class CredentialDataAccess : BaseDataAccess<Credential>, ICredentialDataAccess
    {
        public CredentialDataAccess(string conStr) : base(conStr)
        {

        }       

        public async Task<int> CreateAsync(Credential entity)
        {
            return await ExecuteScalarAsync<int>(@"insert Credentials 
                                                    (UserName, Password, SecretToken, HostUrl, RememberMe, OrgId, EndPointName, ConnectorId, WorkflowId) 
                                                    output inserted.Id 
                                                    values(@UserName, @Password, @SecretToken, @HostUrl, @RememberMe, @OrgId, @EndPointName, @ConnectorId, @WorkflowId)",
                entity);
        }

        public async Task<int> UpdateAsync(Credential entity)
        {
            return await ExecuteScalarAsync<int>(@"update Credentials 
                                                set UserName = @UserName, 
                                                Password = @Password, 
                                                SecretToken = @SecretToken, 
                                                HostUrl = @HostUrl, 
                                                RememberMe = @RememberMe, 
                                                OrgId = @OrgId, 
                                                EndPointName = @EndPointName,
                                                ConnectorId = @ConnectorId, 
                                                WorkflowId = @WorkflowId 
                                                output inserted.Id 
                                                where Id = @Id ", entity);
        }
    }
}
