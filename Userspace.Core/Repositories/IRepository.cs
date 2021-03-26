using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Userspace.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        ValueTask<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
