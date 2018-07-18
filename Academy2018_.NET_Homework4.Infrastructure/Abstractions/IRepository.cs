using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy2018_.NET_Homework5.Infrastructure.Abstractions
{
    public interface IRepository<TEntity> 
        where TEntity: class
    {
        Task<List<TEntity>> GetAsync();

        Task<TEntity> GetAsync(object id);

        Task<object> CreateAsync(TEntity entity);

        Task UpdateAsync(object id, TEntity entity);

        Task DeleteAsync(object id);

        Task DeleteAsync(TEntity entity);

        Task SaveChangesAsync();

        Task<bool> IsExistAsync(object id);
    }
}
