using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI
{
    internal class DispatcherMenuCompositor
    {
        public IEnumerable<ListMenuItemViewModel> ComposeMenu(IWindsorContainer container)
        {
            List<ListMenuItemViewModel> dispatcherMenuItems = new List<ListMenuItemViewModel>()
            {
                new ListMenuItemViewModel()
                {
                    Name = "Станции",
                    ViewModel = () => container.Resolve<StationMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Маршруты",
                    ViewModel = () => container.Resolve<RouteMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Рейсы",
                    ViewModel = () => container.Resolve<RunMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Водители",
                    ViewModel = () => container.Resolve<DriverMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Категории",
                    ViewModel = () => container.Resolve<CategoryMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Транспорт",
                    ViewModel = () => container.Resolve<VehicleMenuViewModel>(),
                },
                new ListMenuItemViewModel()
                {
                    Name = "Типы билетов",
                    ViewModel = () => container.Resolve<TicketTypeMenuViewModel>(),
                },
            };

            return dispatcherMenuItems;
        }
    }
}
