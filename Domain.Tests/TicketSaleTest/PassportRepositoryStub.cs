using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Diagnostics;

namespace Domain.Tests.TicketSaleTest
{
    internal class PassportRepositoryStub : IPassportRepository
    {
        HashSet<Passport> _passports;

        public PassportRepositoryStub(HashSet<Passport> passports)
        {
            _passports = passports;
        }

        public void Add(Passport entity)
        {
            _passports.Add(entity);
        }

        public void Delete(Passport entity)
        {
            throw new NotImplementedException();
        }

        public Passport? Get(int number, int series)
        {
            return _passports.SingleOrDefault(x => x.Number == number && x.Series == series);
        }

        public void Save()
        {
            Debug.WriteLine("Passport repository saved");
        }

        public void Update(Passport entity)
        {
            throw new NotImplementedException();
        }
    }
}
