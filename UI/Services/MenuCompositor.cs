using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI.Services
{
    internal class MenuCompositor : IMenuCompositor
    {
        private readonly IWindsorContainer _container;
        
        public MenuCompositor(IWindsorContainer container)
        {
            _container = container;
        }

        public List<MenuItemViewModel> ComposeMenu()
        {
            List<MenuItemViewModel> list = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel("Найти", () => _container.Resolve<RunSearchViewModel>()),

                new MenuItemViewModel("Диспетчер", ComposeDispatcher()),

                new MenuItemViewModel("Справочники", ComposeGuidebook()),

                new MenuItemViewModel("Справка", () => _container.Resolve<CertificateViewModel>()),

                new MenuItemViewModel("Настройки", ComposeSettings()),

                new MenuItemViewModel("О программе", () => _container.Resolve<AboutViewModel>())
            };

            return list;
        }

        private List<MenuItemViewModel> ComposeDispatcher()
        {
            List<MenuItemViewModel> list = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel("Станции", () => _container.Resolve<StationMenuViewModel>()),
                new MenuItemViewModel("Маршруты", () => _container.Resolve<RouteMenuViewModel>()),
                new MenuItemViewModel("Рейсы", () => _container.Resolve<RunMenuViewModel>()),
                new MenuItemViewModel("Водители", () => _container.Resolve<DriverMenuViewModel>()),
                new MenuItemViewModel("Транспорт", () => _container.Resolve<VehicleMenuViewModel>()),
            };
            return list;
        }

        private List<MenuItemViewModel> ComposeGuidebook()
        {
            List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel("Марка", () => _container.Resolve<BrandMenuViewModel>()),
                new MenuItemViewModel("Модели", () => _container.Resolve<VehicleModelMenuViewModel>()),
                new MenuItemViewModel("Типы ремонта", () => _container.Resolve<RepairTypeMenuViewModel>()),
                new MenuItemViewModel("Категории", () => _container.Resolve<CategoryMenuViewModel>()),
                new MenuItemViewModel("Типы билетов", () => _container.Resolve<TicketTypeMenuViewModel>()),
                new MenuItemViewModel("Список фрахтовщиков", () => _container.Resolve<FreighterMenuViewModel>()),
            };
            return menuList;
        }

        private List<MenuItemViewModel> ComposeSettings()
        {
            List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel("Регистрация", () => _container.Resolve<RegistrationViewModel>()),
                new MenuItemViewModel("Смена пароля", () => _container.Resolve<UpdatePasswordViewModel>()),
            };
            return menuList;
        }
    }
}
