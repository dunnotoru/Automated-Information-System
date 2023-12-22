using Castle.Windsor;
using System;
using System.Collections.Generic;
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

        public List<MainMenuItemViewModel> ComposeMenu(IWindsorContainer container)
        {
            List<MainMenuItemViewModel> menuList = new List<MainMenuItemViewModel>()
            {
                new MainMenuItemViewModel()
                {
                    Header = "Найти",
                    GetViewModel = () => container.Resolve<RunSearchViewModel>(),
                },

                new MainMenuItemViewModel()
                {
                    Header = "Диспетчер",
                    GetViewModel = () => container.Resolve<DispatcherViewModel>()
                },

                new MainMenuItemViewModel()
                {
                    Header = "Справочники",
                    GetViewModel = () => container.Resolve<GuideBookViewModel>()
                },

                new MainMenuItemViewModel()
                {
                    Header = "Справка",
                    GetViewModel = () => container.Resolve<CertificateViewModel>(),
                },

                new MainMenuItemViewModel()
                {
                    Header = "Настройки",
                    GetViewModel = () => container.Resolve<SettingsViewModel>()
                },

                new MainMenuItemViewModel()
                {
                    Header = "О программе",
                    GetViewModel = () => container.Resolve<AboutViewModel>()
                },
            };
            return menuList;
        }
    }
}
