using System.Windows;
using UI.View;
using UI.ViewModel;
using System.IO;
using Domain.RepositoryInterfaces;
using Domain.Models;
using System;
using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Repositories;
using Domain.Services;
using Domain.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.Services;
using UI.Services.Abstractions;
using UI.Stores;
using UI.ViewModel.Books;
using UI.ViewModel.Dispatcher;
using UI.ViewModel.Factories;
using UI.ViewModel.Sales;
using UI.ViewModel.Settings;

namespace UI;

public partial class App : Application
{
    private string GetOrCreateContentRoot()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var h = Host.CreateDefaultBuilder()
            .UseEnvironment("Development")
            .UseContentRoot(
                GetOrCreateContentRoot()
            )
            .ConfigureServices(Configure)
            .Build();

        IAccountRepository repo = h.Services.GetRequiredService<IAccountRepository>();

        int accountCount = repo.Count();
        if (accountCount == 0)
        {
            LoadRegistration(h.Services);
        }
        else
        {
            LoadLogin(h.Services);
        } 

        base.OnStartup(e);
    }

    private void LoadLogin(IServiceProvider provider)
    {
        LoginViewModel vm = provider.GetRequiredService<LoginViewModel>();
        EventHandler? handler = null;
        handler = (sender, e) =>
        {
            SwitchWindowToMain(provider);
            vm.AuthenticationDone -= handler;
        };
        
        vm.AuthenticationDone += handler;
        MainWindow = new LoginWindow
        {
            DataContext = vm,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        MainWindow.Show();
    }

    private void LoadRegistration(IServiceProvider provider)
    {
        RegistrationViewModel vm = provider.GetRequiredService<RegistrationViewModel>();
        EventHandler? handler = null;
        handler = (sender, e) =>
        {
            SwitchWindowToMain(provider);
            vm.RegistrationDone -= handler;
        };
        vm.RegistrationDone += handler;
        MainWindow = new RegistrationWindow
        {
            DataContext = vm,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        MainWindow.Show();
    }

    private void SwitchWindowToMain(IServiceProvider provider)
    {
        ShellWindow window = new ShellWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            WindowState= WindowState.Maximized
        };

        MainWindow?.Close();
        window.Show();
        MainWindow = window;
    }

    private void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlite(context.Configuration["ConnectionStrings:ApplicationDatabase"]
                        ?? throw new NullReferenceException());
            })
            .AddDbContext<AccountContext>(options =>
            {
                options.UseSqlite(context.Configuration["ConnectionStrings:DomainDatabase"] 
                        ?? throw new NullReferenceException());
            });
        
        services
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IPasswordValidator, PasswordValidator>()
            .AddSingleton<IAccountRepository, AccountRepository>()
            .AddSingleton<IStationRepository, StationRepository>()
            .AddSingleton<IRouteRepository, RouteRepository>()
            .AddSingleton<IRunRepository, RunRepository>()
            .AddSingleton<IVehicleRepository, VehicleRepository>()
            .AddSingleton<IDriverRepository, DriverRepository>()
            .AddSingleton<ITicketRepository, TicketRepository>()
            .AddSingleton<ITicketTypeRepository, TicketTypeRepository>()
            .AddSingleton<IPassportRepository, PassportRepository>()
            .AddSingleton<ICategoryRepository, CategoryRepository>()
            .AddSingleton<IBrandRepository, BrandRepository>()
            .AddSingleton<IVehicleModelRepository, VehicleModelRepository>()
            .AddSingleton<IRepairTypeRepository, RepairTypeRepository>()
            .AddSingleton<IFreighterRepository, FreighterRepository>()
            .AddSingleton<IScheduleRepository, ScheduleRepository>();

        services
            .AddSingleton<OrderStore>()
            .AddSingleton<AccountStore>()
            .AddSingleton<NavigationStore>()
            .AddSingleton<IViewModelFactory>(provider => new ViewModelFactory(provider))
            .AddSingleton<NavigationService>()
            .AddSingleton<IMessageBoxService, MessageBoxService>()
            .AddSingleton<IArrivalTimeCalculator, ArrivalTimeCalculator>()
            .AddSingleton<AuthenticationService>()
            .AddSingleton<RegistrationService>()
            .AddSingleton<RunSearchService>()
            .AddTransient<OrderProcessService>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<RegistrationViewModel>()
            .AddSingleton<ScheduleService>()
            .AddSingleton<IDocumentFormatter<Ticket>, TicketFormatter>()
            .AddSingleton<IDocumentFormatter<Receipt>, ReceiptFormatter>();

        services.AddSingleton<ITicketPrintService>(provider => new TicketPrintService(
            context.Configuration["Directories:TicketDirectory"] ?? throw new InvalidOperationException(),
            provider.GetService<IDocumentFormatter<Ticket>>() ?? throw new InvalidOperationException()
        ));

        services.AddSingleton<IReceiptPrintService>(provider => new ReceiptPrintService(
            context.Configuration["Directories:ReceiptDirectory"] ?? throw new InvalidOperationException(),
            provider.GetService<IDocumentFormatter<Receipt>>() ?? throw new InvalidOperationException()
        ));

        services.AddSingleton<CertificateViewModel>(_ => new CertificateViewModel(
            context.Configuration["Directories:CertificatePath"] ?? throw new InvalidOperationException()
        ));

        services.AddSingleton<ITicketPriceCalculator, TicketPriceCalculator>();

        services.AddTransient<StationMenuViewModel>()
            .AddTransient<RouteMenuViewModel>()
            .AddTransient<RunMenuViewModel>()
            .AddTransient<DriverMenuViewModel>()
            .AddTransient<VehicleMenuViewModel>()
            .AddTransient<TicketMenuViewModel>()
            .AddTransient<CategoryMenuViewModel>()
            .AddTransient<BrandMenuViewModel>()
            .AddTransient<TicketMenuViewModel>()
            .AddTransient<RepairTypeMenuViewModel>()
            .AddTransient<VehicleMenuViewModel>()
            .AddTransient<FreighterMenuViewModel>()
            .AddTransient<RunSearchViewModel>()
            .AddTransient<PassengerRegistrationViewModel>()
            .AddTransient<UpdatePasswordViewModel>()
            .AddTransient<ScheduleDataViewModel>()
            .AddTransient<AboutViewModel>();
        
        services.AddSingleton<ShellViewModel>(provider =>
        {
            MenuCompositor compositor = new MenuCompositor(provider);

            return new ShellViewModel(
                provider.GetService<NavigationStore>() ?? throw new InvalidOperationException(),
                compositor.ComposeMenu(),
                provider.GetService<ScheduleService>() ?? throw new InvalidOperationException()
                );
        });
    }
}