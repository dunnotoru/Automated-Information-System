using Domain.Models;
using System.Linq.Expressions;

namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity station);
        void Update(TEntity station);
        void Delete(int id);
        void Save();
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
    }
}
