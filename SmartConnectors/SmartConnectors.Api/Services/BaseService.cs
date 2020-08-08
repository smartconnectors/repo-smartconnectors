using SmartConnectors.DataAccess;
using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseDataAccess<T> _dataAccess;

        public BaseService(string connStr)
        {
            _dataAccess = new BaseDataAccess<T>(connStr);
        }      

        public virtual async Task<T> Get(int id)    
        {
            return await _dataAccess.Get(id);
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _dataAccess.Get();
        }

        public void Dispose()
        {
            _dataAccess.Dispose();
        }
    }
}
