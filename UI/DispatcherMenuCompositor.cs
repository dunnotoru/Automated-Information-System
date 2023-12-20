﻿using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;
using UI.ViewModel.Dispatcher;

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
                    ViewModel = () => container.Resolve<RunMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Водители",
                    ViewModel = () => container.Resolve<DriverMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Категории",
                    ViewModel = () => container.Resolve<CategoryMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Транспорт",
                    ViewModel = () => container.Resolve<VehicleMenuViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Типы билетов",
                    ViewModel = () => container.Resolve<TicketTypeMenuViewModel>(),
                },
            };

            return dispatcherMenuItems;
        }
    }
}
