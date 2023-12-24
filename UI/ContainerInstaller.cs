using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.Services;
using System.Configuration;
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
            
            container.Register(Component
                .For<ITicketPrintService>()
                .ImplementedBy<TicketPrintService>()
                .DependsOn(Dependency
                    .OnValue("path", ConfigurationManager.AppSettings.Get("TicketFolderPath"))));

            container.Register(Component
                .For<IReceiptPrintService>()
                .ImplementedBy<ReceiptPrintService>()
                .DependsOn(Dependency
                    .OnValue("path", ConfigurationManager.AppSettings.Get("ReceiptFolderPath"))));

            container.Register(Component.For<ITicketPriceCalculator>().ImplementedBy<TicketPriceCalculator>());

            #region dispatcher menu viewmodels
            container.Register(Component.For<StationMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RouteMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<RunMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<DriverMenuViewModel>().LifestyleTransient());
            container.Register(Component.For<VehicleMenuViewModel>().LifestyleTransient());
            #endregion

            #region guidebook viewmodels
            container.Register(Component.For<CategoryBookViewModel>().LifestyleTransient());
            container.Register(Component.For<BrandBookViewModel>().LifestyleTransient());
            container.Register(Component.For<TicketTypeBookViewModel>().LifestyleTransient());
            container.Register(Component.For<RepairTypeBookViewModel>().LifestyleTransient());
            container.Register(Component.For<VehicleModelBookViewModel>().LifestyleTransient());
            container.Register(Component.For<FreighterBookViewModel>().LifestyleTransient());
            #endregion

            container.Register(Component.For<RegistrationViewModel>().LifestyleTransient());

            container.Register(Component
                .For<DispatcherViewModel>()
                .UsingFactoryMethod(() => CreateDispatcherViewModel(container))
                .LifestyleTransient());

            container.Register(Component
                .For<GuideBookViewModel>()
                .UsingFactoryMethod(() => CreateGuidebookViewModel(container))
                .LifestyleTransient());


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
            container.Register(Component.For<IScheduleRepository>().ImplementedBy<ScheduleRepository>());
            
            container.Register(Component.For<ICategoryRepository>().ImplementedBy<CategoryRepository>());
            container.Register(Component.For<ITicketTypeRepository>().ImplementedBy<TicketTypeRepository>());
            container.Register(Component.For<IBrandRepository>().ImplementedBy<BrandRepository>());
            container.Register(Component.For<IVehicleModelRepository>().ImplementedBy<VehicleModelRepository>());
            container.Register(Component.For<IRepairTypeRepository>().ImplementedBy<RepairTypeRepository>());
            container.Register(Component.For<IFreighterRepository>().ImplementedBy<FreighterRepository>());
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

        private GuideBookViewModel CreateGuidebookViewModel(IWindsorContainer container)
        {
            GuidebookMenuCompositor compositor = new GuidebookMenuCompositor();
            return new GuideBookViewModel(compositor.ComposeMenu(container));

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
