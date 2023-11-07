using ConsoleUI.Controllers;
using Domain.EntityFramework.Entities;
using Domain.EntityFramework.Repositories.AccountRepositoryImpl;
using Domain.RepositoryInterfaces.AccountRepository;
using Domain.Services;
using Domain.Services.Authentication;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            
        }

        static async Task MainAsync(string[] args)
        {
            List<AccountEntity> a = new List<AccountEntity>();
            string connectionString = "Data Source=C:\\Users\\DavlaD\\source\\repos\\Automated Information System\\Domain.EntityFramework\\DataBase.db";

            IAccountRepository repos = new AccountRepository(connectionString);
            IPasswordHasher hasher = new PasswordHasher();
            IAuthenticationService service =
                new AuthenticationService(repos,hasher);
            AuthenticationController controller 
                = new AuthenticationController(service);

            bool result = await controller.Authenticate("Ryan", "Gosling");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}