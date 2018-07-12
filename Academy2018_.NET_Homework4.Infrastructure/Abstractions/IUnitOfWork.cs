using System.Threading.Tasks;

namespace Academy2018_.NET_Homework4.Infrastructure.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
