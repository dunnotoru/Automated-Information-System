using System;
using System.IO;
using System.Windows;
using InformationSystem.Domain;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services;
using InformationSystem.Services.Abstractions;
using InformationSystem.Stores;
using InformationSystem.View;
using InformationSystem.ViewModel;
using InformationSystem.ViewModel.Factories;
using InformationSystem.ViewModel.Menu;
using InformationSystem.ViewModel.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InformationSystem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        IHost h = Host.CreateDefaultBuilder()
            .UseEnvironment("Development")
            .ConfigureServices(Configure)
            .Build();

        SwitchWindowToMain(h.Services);

        base.OnStartup(e);
    }
    
    private static string GetOrCreateContentRoot()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
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
            WindowState= WindowState.Maximized,
            DataContext = provider.GetRequiredService<ShellViewModel>()
        };
        window.Show();
        MainWindow = window;
    }

    private static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddDbContextFactory<DomainContext>(builder =>
            {
                string connection = context.Configuration["ConnectionStrings:DomainDatabase"]
                                    ?? throw new NullReferenceException("No connection string provided");
                connection = "Data Source=" + Path.Join(Directory.GetCurrentDirectory(), connection);
                builder.UseSqlite(connection);
            });
        
        services
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IPasswordValidator, PasswordValidator>()
            .AddSingleton<OrderStore>()
            .AddSingleton<AccountStore>()
            .AddSingleton<NavigationStore>()
            .AddSingleton<IViewModelFactory>(provider => 
                new ViewModelFactory(provider, provider.GetRequiredService<IDbContextFactory<DomainContext>>())
            )
            .AddSingleton<NavigationService>()
            .AddSingleton<IMessageBoxService, MessageBoxService>()
            .AddSingleton<IArrivalTimeCalculator, ArrivalTimeCalculator>()
            .AddSingleton<AuthenticationService>()
            .AddSingleton<RegistrationService>()
            .AddSingleton<RunSearchService>()
            .AddTransient<OrderProcessService>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<RegistrationViewModel>()
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
            .AddTransient<VehicleModelMenuViewModel>()
            .AddTransient<VehicleMenuViewModel>()
            .AddTransient<TicketMenuViewModel>()
            .AddTransient<CategoryMenuViewModel>()
            .AddTransient<BrandMenuViewModel>()
            .AddTransient<TicketTypeMenuViewModel>()
            .AddTransient<RepairTypeMenuViewModel>()
            .AddTransient<FreighterMenuViewModel>()
            .AddTransient<RunSearchViewModel>()
            .AddTransient<PassengerRegistrationViewModel>()
            .AddTransient<ScheduleDataViewModel>();
        
        services.AddSingleton<ShellViewModel>(provider =>
        {
            MenuCompositor compositor = new MenuCompositor(provider);

            return new ShellViewModel(
                provider.GetService<NavigationStore>() ?? throw new InvalidOperationException(),
                compositor.ComposeMenu());
        });
    }
}