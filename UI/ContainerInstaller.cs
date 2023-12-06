using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.Services;
using Domain.UseCases.AccountUseCases;
using Domain.UseCases.CashierUseCases;
using UI.Services;
using UI.Stores;
using UI.ViewModel;

namespace UI
{
    internal class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AccountStore>());
            container.Register(Component.For<NavigationStore>().Named("MainNavigationStore"));
            container.Register(Component.For<NavigationStore>().Named("TicketSaleNavigationStore"));

            container.Register(Component.For<AccountContext>().LifestyleTransient());
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>());
            container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>());
            container.Register(Component.For<AuthenticationUseCase>());
            container.Register(Component.For<RegistrationUseCase>());
            container.Register(Component.For<LoginViewModel>());

            container.Register(Component.For<ApplicationContext>().LifestyleTransient());
            container.Register(Component.For<IStationRepository>().ImplementedBy<StationRepository>());
            container.Register(Component.For<IRouteRepository>().ImplementedBy<RouteRepository>());
            container.Register(Component.For<IRunRepository>().ImplementedBy<RunRepository>());
            container.Register(Component.For<FindRunsUseCase>());
            container.Register(Component.For<GetStationsUseCase>());

            container.Register(Component.For<ITicketPriceCalculator>().ImplementedBy<TicketPriceCalculator>());
            container.Register(Component.For<IPassportRepository>().ImplementedBy<PassportRepository>());
            container.Register(Component.For<ITicketRepository>().ImplementedBy<TicketRepository>());
            container.Register(Component.For<SellTicketUseCase>());

            container.Register(Component.For<StationService>().LifestyleTransient());
            container.Register(Component.For<RouteService>().LifestyleTransient());
            container.Register(Component.For<RouteViewModel>().LifestyleTransient());
            container.Register(Component.For<StationViewModel>().LifestyleTransient());
            container.Register(Component
                .For<DispatcherViewModel>()
                .UsingFactoryMethod(()=>CreateDispatcherManagerViewModel(container))
                .LifestyleTransient());

            container.Register(Component
                .For<RunSearchViewModel>()
                .UsingFactoryMethod(() => CreateRunSearchViewModel(container))
                );

            container.Register(Component.For<PassengerRegistrationViewModel>());
            container.Register(Component.For<CertificateViewModel>());

            container.Register(Component
                .For<UpdatePasswordViewModel>()
                .UsingFactoryMethod(() => CreateUpdatePasswordViewModel(container)));

            container.Register(Component
                .For<TicketSaleParentViewModel>()
                .UsingFactoryMethod(() => CreateTicketSaleParentViewModel(container))
                .LifestyleTransient());

            container.Register(Component
                .For<ShellViewModel>()
                .UsingFactoryMethod(() => CreateShell(container)));
        }

        private DispatcherViewModel CreateDispatcherManagerViewModel(IWindsorContainer container)
        {
            DispatcherMenuCompositor compositor = new DispatcherMenuCompositor();
            return new DispatcherViewModel(compositor.ComposeMenu(container));
        }

        private NavigationService CreateShellNavigationService(IWindsorContainer container)
        {
            return new NavigationService(
                container.Resolve<NavigationStore>(),
                () => CreateShell(container));
        }

        private UpdatePasswordViewModel CreateUpdatePasswordViewModel(IWindsorContainer container)
        {
            return new UpdatePasswordViewModel(
                container.Resolve<RegistrationUseCase>(),
                CreateShellNavigationService(container),
                container.Resolve<AccountStore>()
                );
        }
        
        private TicketSaleParentViewModel CreateTicketSaleParentViewModel(IWindsorContainer container)
        {
            return new TicketSaleParentViewModel(
                container.Resolve<NavigationStore>("TicketSaleNavigationStore"),
                CreateRunSearchNavigationService(container));
        }
        
        private NavigationService CreateRunSearchNavigationService(IWindsorContainer container)
        {
            return new NavigationService(
                container.Resolve<NavigationStore>("TicketSaleNavigationStore"),
                () => CreateRunSearchViewModel(container));
        }
        private NavigationService CreatePassengerRegistrationNavigationService(IWindsorContainer container)
        {
            return new NavigationService(
                container.Resolve<NavigationStore>("TicketSaleNavigationStore"),
                () => CreateTicketSaleViewModel(container));
        }
        private PassengerRegistrationViewModel CreateTicketSaleViewModel(IWindsorContainer container)
        {
            return new PassengerRegistrationViewModel(
                CreateRunSearchNavigationService(container),
                container.Resolve<SellTicketUseCase>());
        }
        private RunSearchViewModel CreateRunSearchViewModel(IWindsorContainer container)
        {
            return new RunSearchViewModel(
                container.Resolve<GetStationsUseCase>(),
                container.Resolve<FindRunsUseCase>(),
                CreatePassengerRegistrationNavigationService(container)
                );
        }
        
        private ShellViewModel CreateShell(IWindsorContainer container)
        {
            MenuCompositor mc = new MenuCompositor(true,true,true,true);

            return new ShellViewModel(
                container.Resolve<NavigationStore>("MainNavigationStore"),
                mc.ComposeMenu(container));
        }
    }
}
