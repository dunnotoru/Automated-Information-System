namespace Domain.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
