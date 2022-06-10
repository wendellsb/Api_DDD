using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{
    // T vai utilizar alguma classe que tenha como herança a BaseEntity
    public interface IRepository<T> where T : BaseEntity 
    { 
                // CRUD //
        Task<T>InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectAsyn(Guid id);
        Task<IEnumerable<T>> SelectAsync();
    }
}
