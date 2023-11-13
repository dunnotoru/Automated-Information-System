using Domain.EntityFramework.Contexts;
using System.Configuration;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() +
                "\\..\\..\\..\\" + "Database.db");

            
            CasshierContext sc = new CasshierContext(connectionString);

        }
    }
}