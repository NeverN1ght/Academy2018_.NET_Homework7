using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Academy2018_.NET_Homework4.Infrastructure.Abstractions
{
    public interface IRepository<TEntity> 
        where TEntity: class, IEntity
    {
        IEnumerable<TEntity> Get(Func<TEntity, bool> filter = null);

        void Create(TEntity entity, string createdBy = null);

        void Update(TEntity entity, string updatedBy = null);

        void Delete(object id);

        void Delete(TEntity entity);
    }
}
