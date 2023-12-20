using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
    }
}
