using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.Util;
using Castle.Windsor;
using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.UseCases.AccountUseCases;
using Domain.UseCases.CashierUseCases;
using Domain.UseCases.CasshierUseCases;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using UI.Services;
using UI.Stores;
using UI.ViewModel;

namespace UI
{
    internal class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<NavigationStore>());
            container.Register(Component.For<NavigationService>());

            container.Register(Component.For<AccountContext>().LifestyleTransient());
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>());
            container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>());
            container.Register(Component.For<AuthenticationUseCase>());
            container.Register(Component.For<LoginViewModel>());

            container.Register(Component.For<CashierContext>().LifestyleTransient());
            container.Register(Component.For<IStationRepository>().ImplementedBy<StationRepository>());
            container.Register(Component.For<IRouteRepository>().ImplementedBy<RouteRepository>());
            container.Register(Component.For<IRunRepository>().ImplementedBy<RunRepository>());
            container.Register(Component.For<FindRunsUseCase>());
            container.Register(Component.For<GetStationsUseCase>());
            container.Register(Component
                .For<RunSearchViewModel>()
                .UsingFactoryMethod<RunSearchViewModel>(() => CreateRunSearchViewModel(container))
                );

            container.Register(Component.For<TicketSaleViewModel>());
            container.Register(Component
                .For<ShellViewModel>()
                .UsingFactoryMethod(() => CreateShell(container)));
        }

        private NavigationService CreateTicketSellNavigationService(IWindsorContainer container)
        {
            return new NavigationService(
                container.Resolve<NavigationStore>(),
                () => new TicketSaleViewModel());
        }
        private RunSearchViewModel CreateRunSearchViewModel(IWindsorContainer container)
        {
            return new RunSearchViewModel(
                container.Resolve<GetStationsUseCase>(),
                container.Resolve<FindRunsUseCase>(),
                CreateTicketSellNavigationService(container)
                );
        }

        private List<MenuItemViewModel> CreateMenu(IWindsorContainer container)
        {
            List<MenuItemViewModel> r = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel()
                {
                    Header = "Настройки",
                    GetViewModel = () => throw new NotImplementedException()
                },

                new MenuItemViewModel()
                {
                    Header = "Сменить пароль",
                    GetViewModel = () => throw new NotImplementedException()
                }
            };

            List<MenuItemViewModel> s = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel()
                {
                    Header = "Содержание",
                    GetViewModel = () => throw new NotImplementedException()
                },

                new MenuItemViewModel()
                {
                    Header = "О программе",
                    GetViewModel = () => throw new NotImplementedException()
                }
            };

            List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel()
                {
                    Header = "Найти",
                    GetViewModel = () => container.Resolve<RunSearchViewModel>(),
                },

                new MenuItemViewModel(r)
                {
                    Header = "Разное",
                },

                new MenuItemViewModel()
                {
                    Header = "Справка",
                },

                new MenuItemViewModel(s)
                {
                    Header = "Справочники",
                    GetViewModel = () => throw new NotImplementedException()
                },

                new MenuItemViewModel()
                {
                    Header = "Документы",
                    GetViewModel = () => throw new NotImplementedException(),
                }
            };

            return menuList;
        }
        private ShellViewModel CreateShell(IWindsorContainer container)
        {
            return new ShellViewModel(
                container.Resolve<NavigationStore>(),
                CreateMenu(container));
        }
        
    }
}
