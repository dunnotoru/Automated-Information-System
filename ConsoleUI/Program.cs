using Domain.EntityFramework.Repositories;
using Domain.Models;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StationRepository repository = new StationRepository();
            repository.Add(new Station(1, "nigger", "PRISON")); 
        }
    }
}