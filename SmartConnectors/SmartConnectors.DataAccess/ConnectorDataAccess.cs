using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class ConnectorDataAccess : BaseDataAccess<Connector>, IConnectorDataAccess
    {
        public ConnectorDataAccess(string conStr) : base(conStr)
        {
           
        }
        public async Task<int> CreateAsync(Connector entity)
        {
            return await ExecuteScalarAsync<int>(
                "insert Connector(Name, CreatedDate, ModifiedDate) output inserted.Id values(@Name, @CreatedDate, @ModifiedDate)",
                entity);
        }
    }
}
