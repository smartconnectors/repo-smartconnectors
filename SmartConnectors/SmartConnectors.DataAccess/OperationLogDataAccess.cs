using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class OperationLogDataAccess : BaseDataAccess<OperationLog>, IOperationLogDataAccess
    {
        public OperationLogDataAccess(string conStr) : base(conStr)
        {

        }

        public async Task<IEnumerable<OperationLog>> GetOperationLogs(int workflowId)
        {
            return await QueryAsync<OperationLog>("select * from OperationLogs where WorkflowId=@workflowId", new { workflowId });
        }

        public async Task<int> CreateAsync(OperationLog ops)
        {
            return await ExecuteScalarAsync<int>("insert OperationLogs (Message, Content, Status, CreatedDate, WorkflowId) output inserted.Id values(@Message, @Content, @Status, @CreatedDate, @WorkflowId)",
                ops);
        }
    }
}
