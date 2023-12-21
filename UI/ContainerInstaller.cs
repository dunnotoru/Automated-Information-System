using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.Services;
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
            container.Register(Component.For<IDocumentPrintService>().ImplementedBy<DocumentPrintService>());
            RegisterRepositories(container);

            container.Register(Component.For<OrderStore>());
            container.Register(Component.For<AccountStore>());
            container.Register(Component.For<NavigationStore>());
            container.Register(Component.For<IViewModelFactory>()
                .UsingFactoryMethod(() => CreateViewModelFactory(container)));
            container.Register(Component.For<NavigationService>());

            container.Register(Component.For<IMessageBoxService>().ImplementedBy<MessageBoxService>());


            container.Register(Component.For<AuthenticationService>());
            container.Register(Component.For<RegistrationService>());
            container.Register(Component.For<LoginViewModel>());

            container.Register(Component.For<ScheduleService>());

            container.Register(Component.For<ITicketPriceCalculator>().ImplementedBy<TicketPriceCalculator>());

            container.Register(Component.For<CategoryMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<TicketTypeMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<StationMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RouteMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RunMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<DriverMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<VehicleMenuViewModel>().LifestyleTransient());

            container.Register(Component
                .For<DispatcherViewModel>()
                .UsingFactoryMethod(() => CreateDispatcherViewModel(container))
                .LifestyleTransient());

            container.Register(Component.For<RegistrationViewModel>().LifestyleTransient());

            container.Register(Component
                .For<SettingsViewModel>()
                .UsingFactoryMethod(() => CreateSettingsViewModel(container))
                .LifestyleTransient());

            container.Register(Component.For<RunSearchViewModel>().LifestyleTransient());
            container.Register(Component.For<PassengerRegistrationViewModel>().LifestyleTransient());
            container.Register(Component.For<UpdatePasswordViewModel>().LifestyleTransient());
            container.Register(Component.For<CertificateViewModel>());
            container.Register(Component.For<AboutViewModel>());

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
            container.Register(Component.For<ITicketTypeRepository>().ImplementedBy<TicketTypeRepository>());
            container.Register(Component.For<ICategoryRepository>().ImplementedBy<CategoryRepository>());
            container.Register(Component.For<IScheduleRepository>().ImplementedBy<ScheduleRepository>());
        }

        private ViewModelFactory CreateViewModelFactory(IWindsorContainer container)
        {
            return new ViewModelFactory(container);
        }

        private DispatcherViewModel CreateDispatcherViewModel(IWindsorContainer container)
        {
            DispatcherMenuCompositor compositor = new DispatcherMenuCompositor();
            return new DispatcherViewModel(compositor.ComposeMenu(container));
        }

        private SettingsViewModel CreateSettingsViewModel(IWindsorContainer container)
        {
            SettingsMenuCompositor compositor = new SettingsMenuCompositor();
            return new SettingsViewModel(compositor.ComposeMenu(container));
        }

        private ShellViewModel CreateShell(IWindsorContainer container)
        {
            MenuCompositor mc = new MenuCompositor(true, true, true, true);

            return new ShellViewModel(
                container.Resolve<NavigationStore>(),
                mc.ComposeMenu(container),
                container.Resolve<ScheduleService>());
        }
    }
}
