using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        private readonly CashierContext _context;

        public PassportRepository(CashierContext context)
        {
            _context = context;
        }

        public void Add(Passport entity)
        {
            _context.Add(entity);
        }

        public void Delete(Passport entity)
        {
            _context.Remove(entity);
        }

        public Passport? Get(int number, int series)
        {
            return _context.Passports.SingleOrDefault(x=>x.Number == number && x.Series == series);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Passport entity)
        {
            _context.Update(entity);
        }
    }
}
