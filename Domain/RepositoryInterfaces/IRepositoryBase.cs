using System.Linq.Expressions;

namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Save();
    }
}
