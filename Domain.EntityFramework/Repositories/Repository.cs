using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.EntityFramework.Repositories
{
    public class Repository<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext _context;
        public bool IsDisposed { get; private set; }

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
        }

        public void Add(T entity)
        {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            _context.Add<T>(entity);
        }

        public void Delete(int id)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            T entity = _context.Find<T>(id);
            _context.Remove<T>(entity);
        }

        public void Update(T entity)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            _context.Update<T>(entity);
        }

        public T Get(int id)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return _context.Find<T>(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return _context.Set<T>().Where(where);
        }

        public IEnumerable<T> GetAll()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return _context.Set<T>();
        }

        public void Save()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(IsDisposed) return;
            _context.Dispose();
            IsDisposed = true;
        }
    }
}
