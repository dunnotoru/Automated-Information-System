using Castle.Windsor;
using System.Collections.Generic;
using UI.ViewModel;

namespace UI
{
    public class SettingsMenuCompositor
    {
        public IEnumerable<ListMenuItemViewModel> ComposeMenu(IWindsorContainer container)
        {
            List<ListMenuItemViewModel> dispatcherMenuItems = new List<ListMenuItemViewModel>()
            {
                new ListMenuItemViewModel()
                {
                    Name = "Регистрация",
                    ViewModel = () => container.Resolve<RegistrationViewModel>()
                },
                new ListMenuItemViewModel()
                {
                    Name = "Смена пароля",
                    ViewModel = () => container.Resolve<UpdatePasswordViewModel>()
                }
            };

            return dispatcherMenuItems;
        }
    }
}
