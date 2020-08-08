using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class OperationsDataAccess : BaseDataAccess<Operation>, IOperationsDataAccess
    {
        public OperationsDataAccess(string conStr) : base(conStr)
        {

        }

        public async Task<IEnumerable<Operation>> GetOperations(int workflowConnectorId)
        {
            return await QueryAsync<Operation>("select * from Operations where WorkflowConnectorId=@workflowConnectorId", new { workflowConnectorId });
        }

        public async Task<int> CreateAsync(Operation ops)
        {
            return await ExecuteScalarAsync<int>("insert Operations (OperationTypeId, Content, WorkflowConnectorId, StepCount) output inserted.Id values(@OperationTypeId, @Content, @WorkflowConnectorId, @StepCount)",
                ops);
        }

        public async Task<int> UpdateAsync(Operation ops)
        {
            return await ExecuteScalarAsync<int>("update Operations set OperationTypeId = @OperationTypeId , Content = @Content,  WorkflowConnectorId = @WorkflowConnectorId, StepCount = @StepCount output inserted.Id where Id = @Id ", ops);
        }
    }
}
