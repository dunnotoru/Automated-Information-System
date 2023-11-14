using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Repositories;
using Domain.Models;
using Domain.Models.Drivers;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext context = new ApplicationContext();

            List<Station> stations = new List<Station>()
            {
                new Station(null,"name", "asd"),
                new Station(null,"GAY", "asd"),
                new Station(null,"name", "FLEX"),
            };
            Route r = new Route(null, "KAIF", stations);
            Driver d = new Driver();
            d.PayrollNumber = "penis";
            d.Name = "gay";

            Repository<Driver> repos = new Repository<Driver>(context);
            repos.Add(d);
            repos.Save();
        }
    }
}