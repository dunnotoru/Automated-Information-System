using Domain.Models;
using System.Linq.Expressions;

namespace Domain.RepositoryInterfaces
{
    public interface IStationRepository
    {
        bool Add(Station run);
        bool Update(Station run);
        bool Delete(int id);
        Station Get(int id);
        IEnumerable<Station> Get(Expression<Func<Station, bool>> where);
        IEnumerable<Station> GetAll();
    }
}
