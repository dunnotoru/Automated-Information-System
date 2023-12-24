using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UI.ViewModel;

namespace UI
{
    internal class MenuCompositor
    {
        private bool _read;
        private bool _write;
        private bool _edit;
        private bool _delete;

        public MenuCompositor(bool read, bool write,
            bool edit, bool delete)
        {
            _read = read;
            _write = write;
            _edit = edit;
            _delete = delete;
        }

        public List<MenuItemViewModel> ComposeMenu(IWindsorContainer container)
        {
            List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel("Найти", () => container.Resolve<RunSearchViewModel>()),

                new MenuItemViewModel("Диспетчер", () => container.Resolve<DispatcherViewModel>()),

                new MenuItemViewModel("Справочники", () => container.Resolve<GuideBookViewModel>()),

                new MenuItemViewModel("Справка", () => container.Resolve<CertificateViewModel>()),

                new MenuItemViewModel("Настройки", () => container.Resolve<SettingsViewModel>()),

                new MenuItemViewModel("О программе", () => container.Resolve<AboutViewModel>())
            };
            return menuList;
        }
    }
}
