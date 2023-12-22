using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI
{
    internal class GuidebookMenuCompositor
    {
        public IEnumerable<ListMenuItemViewModel> ComposeMenu(IWindsorContainer container)
        {
            List<ListMenuItemViewModel> items = new List<ListMenuItemViewModel>()
            {
                new ListMenuItemViewModel()
                {
                    Name = "Марки",
                    ViewModel = () => container.Resolve<BrandBookViewModel>()
                },
                new ListMenuItemViewModel()
                {
                    Name = "Модели",
                    ViewModel = () => container.Resolve<VehicleModelBookViewModel>()
                },
                new ListMenuItemViewModel()
                {
                    Name = "Типы ремонта",
                    ViewModel = () => container.Resolve<RepairTypeBookViewModel>()
                },
                new ListMenuItemViewModel()
                {
                    Name = "Категории",
                    ViewModel = () => container.Resolve<CategoryBookViewModel>()
                },
                new ListMenuItemViewModel()
                {
                    Name = "Типы билетов",
                    ViewModel = () => container.Resolve<TicketTypeBookViewModel>()
                },
            };

            return items;
        }
    }
}
