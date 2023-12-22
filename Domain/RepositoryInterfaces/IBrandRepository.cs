using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IBrandRepository : IRepositoryBase<Brand>
    {
        IEnumerable<Brand> GetAll();
    }
}
