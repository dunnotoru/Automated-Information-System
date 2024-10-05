using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using UI.Services.Abstractions;
using UI.ViewModel;
using UI.ViewModel.Books;
using UI.ViewModel.Dispatcher;
using UI.ViewModel.Sales;
using UI.ViewModel.Settings;

namespace UI.Services;

internal class MenuCompositor : IMenuCompositor
{
    private readonly IServiceProvider _provider;
        
    public MenuCompositor(IServiceProvider provider)
    {
        _provider = provider;
    }

    public List<MenuItemViewModel> ComposeMenu()
    {
        List<MenuItemViewModel> list = new List<MenuItemViewModel>()
        {
            new MenuItemViewModel("Расписание", () => _provider.GetRequiredService<ScheduleDataViewModel>()),
            new MenuItemViewModel("Найти", () => _provider.GetRequiredService<RunSearchViewModel>()),
            new MenuItemViewModel("Диспетчер", ComposeDispatcher()),
            new MenuItemViewModel("Справочники", ComposeGuidebook()),
            new MenuItemViewModel("Настройки", ComposeSettings()),
            new MenuItemViewModel("Справка", () => _provider.GetRequiredService<CertificateViewModel>()),
        };

        return list;
    }

    private List<MenuItemViewModel> ComposeDispatcher()
    {
        List<MenuItemViewModel> list = new List<MenuItemViewModel>()
        {
            new MenuItemViewModel("Станции", () => _provider.GetRequiredService<StationMenuViewModel>()),
            new MenuItemViewModel("Маршруты", () => _provider.GetRequiredService<RouteMenuViewModel>()),
            new MenuItemViewModel("Рейсы", () => _provider.GetRequiredService<RunMenuViewModel>()),
            new MenuItemViewModel("Водители", () => _provider.GetRequiredService<DriverMenuViewModel>()),
            new MenuItemViewModel("Транспорт", () => _provider.GetRequiredService<VehicleMenuViewModel>()),
            new MenuItemViewModel("Список пассажиров", () => _provider.GetRequiredService<TicketMenuViewModel>()),
        };
        return list;
    }

    private List<MenuItemViewModel> ComposeGuidebook()
    {
        List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
        {
            new MenuItemViewModel("Марка", () => _provider.GetRequiredService<BrandMenuViewModel>()),
            new MenuItemViewModel("Модели", () => _provider.GetRequiredService<VehicleModelMenuViewModel>()),
            new MenuItemViewModel("Типы ремонта", () => _provider.GetRequiredService<RepairTypeMenuViewModel>()),
            new MenuItemViewModel("Категории", () => _provider.GetRequiredService<CategoryMenuViewModel>()),
            new MenuItemViewModel("Типы билетов", () => _provider.GetRequiredService<TicketTypeMenuViewModel>()),
            new MenuItemViewModel("Список фрахтовщиков", () => _provider.GetRequiredService<FreighterMenuViewModel>()),
        };
        return menuList;
    }

    private List<MenuItemViewModel> ComposeSettings()
    {
        List<MenuItemViewModel> menuList = new List<MenuItemViewModel>()
        {
            new MenuItemViewModel("Регистрация", () => _provider.GetRequiredService<AccountMenuViewModel>()),
            new MenuItemViewModel("Смена пароля", () => _provider.GetRequiredService<UpdatePasswordViewModel>()),
        };
        return menuList;
    }
}