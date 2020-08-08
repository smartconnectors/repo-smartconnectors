using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess.Contracts
{
    public interface IOperationLogDataAccess : IBaseDataAccess<OperationLog> 
    {
        Task<IEnumerable<OperationLog>> GetOperationLogs(int workflowId);
    }
}
