using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy2018_.NET_Homework5.Infrastructure.Abstractions;
using Academy2018_.NET_Homework5.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Academy2018_.NET_Homework5.Infrastructure.Repositories.Basic
{
    public abstract class BasicRepository<TEntity>: IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        private readonly AirportContext _ctx;

        protected BasicRepository()
        {         
        }

        protected BasicRepository(AirportContext ctx): this()
        {
            _ctx = ctx;
        }

        public virtual async Task<List<TEntity>> GetAsync()
        {
            return await _ctx.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public async Task<object> CreateAsync(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);

            // make save to get created entity id
            await _ctx.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(object id, TEntity entity)
        {
            entity.Id = (int)id;
            var existedEntity = await _ctx.Set<TEntity>().FindAsync(id);
            _ctx.Entry(existedEntity).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _ctx.Set<TEntity>().FindAsync(id);
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _ctx.Set<TEntity>().Remove(entity));
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> IsExistAsync(object id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id) != null;
        }
    }
}
