using System.Collections.Generic;
using InformationSystem.ViewModel;

namespace InformationSystem.Services.Abstractions;

internal interface IMenuCompositor
{
    List<MenuItemViewModel> ComposeMenu();
}