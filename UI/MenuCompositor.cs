using Castle.Windsor;
using System;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI
{
    public class MenuCompositor
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
                    GetViewModel = () => container.Resolve<UpdatePasswordViewModel>(),
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
                    GetViewModel = () => container.Resolve<TicketSaleParentViewModel>(),
                },

                new MenuItemViewModel(r)
                {
                    Header = "Разное",
                },

                new MenuItemViewModel()
                {
                    Header = "Справка",
                    GetViewModel = () => container.Resolve<CertificateViewModel>(),
                },

                new MenuItemViewModel(s)
                {
                    Header = "Справочники",
                },

                new MenuItemViewModel()
                {
                    Header = "Документы",
                }
            };
            return menuList;
        }
    }
}
