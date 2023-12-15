using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.Services;
using System.Windows;
using UI.Services;
using UI.Stores;
using UI.ViewModel;
using UI.ViewModel.Factories;

namespace UI
{
    internal class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<OrderStore>());
            container.Register(Component.For<AccountStore>());
            container.Register(Component.For<NavigationStore>());
            container.Register(Component.For<IViewModelFactory>()
                .UsingFactoryMethod(() => CreateViewModelFactory(container)));
            container.Register(Component.For<NavigationService>());

            container.Register(Component.For<IMessageBoxService>().ImplementedBy<MessageBoxService>());

            RegisterRepositories(container);
            
            container.Register(Component.For<AuthenticationService>());
            container.Register(Component.For<RegistrationService>());
            container.Register(Component.For<LoginViewModel>());

            container.Register(Component.For<ITicketPriceCalculator>().ImplementedBy<TicketPriceCalculator>());

            container.Register(Component.For<StationMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RouteMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RunManagerViewModel>().LifestyleTransient());
            container.Register(Component.For<DriverManagerViewModel>().LifestyleTransient());
            container.Register(Component.For<VehicleManagerViewModel>().LifestyleTransient());

            container.Register(Component
                .For<DispatcherViewModel>()
                .UsingFactoryMethod(() => CreateDispatcherManagerViewModel(container))
                .LifestyleTransient());

            container.Register(Component.For<RunSearchViewModel>());
            container.Register(Component.For<PassengerRegistrationViewModel>());
            container.Register(Component.For<CertificateViewModel>());
            container.Register(Component.For<UpdatePasswordViewModel>());

            container.Register(Component
                .For<ShellViewModel>()
                .UsingFactoryMethod(() => CreateShell(container)));
        }

        private void RegisterRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>());
            container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>());
            container.Register(Component.For<IStationRepository>().ImplementedBy<StationRepository>());
            container.Register(Component.For<IRouteRepository>().ImplementedBy<RouteRepository>());
            container.Register(Component.For<IRunRepository>().ImplementedBy<RunRepository>());
            container.Register(Component.For<IVehicleRepository>().ImplementedBy<VehicleRepository>());
            container.Register(Component.For<IDriverRepository>().ImplementedBy<DriverRepository>());
        }

        private ViewModelFactory CreateViewModelFactory(IWindsorContainer container)
        {
            return new ViewModelFactory(container);
        }

        private DispatcherViewModel CreateDispatcherManagerViewModel(IWindsorContainer container)
        {
            DispatcherMenuCompositor compositor = new DispatcherMenuCompositor();
            return new DispatcherViewModel(compositor.ComposeMenu(container));
        }
        
        private ShellViewModel CreateShell(IWindsorContainer container)
        {
            MenuCompositor mc = new MenuCompositor(true,true,true,true);

            return new ShellViewModel(
                container.Resolve<NavigationStore>(),
                mc.ComposeMenu(container));
        }
    }
}
