using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI
{
    internal class DispatcherMenuCompositor
    {
        public IEnumerable<DispatcherMenuItem> ComposeMenu(IWindsorContainer container)
        {
            List<DispatcherMenuItem> dispatcherMenuItems = new List<DispatcherMenuItem>()
            {
                new DispatcherMenuItem()
                {
                    Name = "Станции",
                    ViewModel = () => container.Resolve<StationMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Маршруты",
                    ViewModel = () => container.Resolve<RouteMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Рейсы",
                    ViewModel = () => container.Resolve<RunManagerViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Водители",
                    ViewModel = () => container.Resolve<DriverMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Транспорт",
                    ViewModel = () => container.Resolve<VehicleManagerViewModel>(),
                },
            };

            return dispatcherMenuItems;
        }
    }
}
