using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
