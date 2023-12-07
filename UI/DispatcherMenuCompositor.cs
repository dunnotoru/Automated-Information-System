﻿using Castle.Windsor;
using System.Collections.Generic;
using UI.View;
using UI.ViewModel;

namespace UI
{
    public class DispatcherMenuCompositor
    {
        public IEnumerable<DispatcherMenuItem> ComposeMenu(IWindsorContainer container)
        {
            List<DispatcherMenuItem> dispatcherMenuItems = new List<DispatcherMenuItem>()
            {
                new DispatcherMenuItem()
                {
                    Name = "Станции",
                    ViewModel = () => container.Resolve<StationManagerViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Маршруты",
                    ViewModel = () => container.Resolve<RouteManagerViewModel>(),
                },
                new DispatcherMenuItem()
                {
                    Name = "Рейсы",
                    ViewModel = () => container.Resolve<RunManagerViewModel>(),  
                }
            };

            return dispatcherMenuItems;
        }
    }
}
