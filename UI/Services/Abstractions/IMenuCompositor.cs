using System.Collections.Generic;
using UI.ViewModel;

namespace UI.Services.Abstractions;

internal interface IMenuCompositor
{
    List<MenuItemViewModel> ComposeMenu();
}