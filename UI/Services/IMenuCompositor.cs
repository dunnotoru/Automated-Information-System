using System.Collections.Generic;
using UI.ViewModel;

namespace UI.Services
{
    internal interface IMenuCompositor
    {
        List<MenuItemViewModel> ComposeMenu();
    }
}
