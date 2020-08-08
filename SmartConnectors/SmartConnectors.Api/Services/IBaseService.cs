using SmartConnectors.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Services
{
    interface IBaseService<T> : IDisposable where T : BaseEntity
    { 
        Task<T> Get(int id);

        Task<IEnumerable<T>> Get();       
    }
}
