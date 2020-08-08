using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess.Contracts
{
    public interface IOperationsDataAccess : IBaseDataAccess<Operation> 
    {
        Task<IEnumerable<Operation>> GetOperations(int workflowId);
    }
}
