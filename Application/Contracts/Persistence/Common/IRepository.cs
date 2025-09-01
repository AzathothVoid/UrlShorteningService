using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence.Common
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task<T> Exists(T entity);
    }
}
