using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel;

internal class ScheduleDataViewModel : ObservableObject
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private ObservableCollection<ScheduleViewModel> _items;
    public ICommand UpdateDataCommand { get; }

    public ScheduleDataViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _items = new ObservableCollection<ScheduleViewModel>();
        Update();
        UpdateDataCommand = new RelayCommand(Update);
    }

    private void Update()
    {
        Items.Clear();
        using DomainContext context = _contextFactory.CreateDbContext();
        foreach (var item in context.Schedules)
        {
            ScheduleViewModel vm = new ScheduleViewModel(item);
            Items.Add(vm);
        }
    }

    public ObservableCollection<ScheduleViewModel> Items
    {
        get => _items;
        set { _items = value; OnPropertyChanged(); }
    }
}