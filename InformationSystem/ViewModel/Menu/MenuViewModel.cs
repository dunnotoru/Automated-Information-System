using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Factories;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public abstract class MenuViewModel<TEditViewModel, TEntity> : ViewModelBase
    where TEditViewModel : EditViewModel
    where TEntity : EntityBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IDbContextFactory<DomainContext> _factory;
    private readonly EditViewModelFactory _vmFactory;

    private ObservableCollection<TEditViewModel> _items;
    private TEditViewModel? _selectedItem;

    public ICommand AddCommand { get; }

    public MenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> factory, EditViewModelFactory vmFactory)
    {
        _messageBoxService = messageBoxService;
        _factory = factory;
        _vmFactory = vmFactory;

        _items = new ObservableCollection<TEditViewModel>();
        using (DomainContext context = _factory.CreateDbContext())
        {
            foreach (TEntity entity in context.Set<TEntity>())
            {
                TEditViewModel vm = (TEditViewModel)_vmFactory.CreateEditViewModel(entity);
                vm.Saved += OnSaved;
                vm.ErrorOccured += OnErrorOccured;
                vm.Removed += OnRemoved;
                Items.Add(vm);
            }
        }
        
        AddCommand = new RelayCommand(Add);
    }

    private void OnRemoved(object? sender, EventArgs e)
    {
        if (sender is not TEditViewModel vm)
        {
            return;
        }
        
        vm.Saved -= OnSaved;
        vm.ErrorOccured -= OnErrorOccured;
        vm.Removed -= OnRemoved;

        Items.Remove(vm);
        
        _messageBoxService.ShowMessage("data removed successfully");
    }

    private void OnErrorOccured(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage(e.Message);
    }
    
    private void OnSaved(object? sender, EventArgs e)
    {
        if (sender is not TEditViewModel vm)
        {
            return;
        }
        
        vm.Saved -= OnSaved;
        vm.ErrorOccured -= OnErrorOccured;
        vm.Removed -= OnRemoved;

        using (DomainContext context = _factory.CreateDbContext())
        {
            TEntity entity = context.Set<TEntity>().First(o => o.Id == vm.Id);
            TEditViewModel updatedVm = (TEditViewModel)_vmFactory.CreateEditViewModel(entity);
            
            updatedVm.Saved += OnSaved;
            updatedVm.ErrorOccured += OnErrorOccured;
            updatedVm.Removed += OnRemoved;
            
            int index = Items.IndexOf(vm);
            Items.Insert(index, updatedVm);
            Items.Remove(vm);
            
            _messageBoxService.ShowMessage("data saved successfully.");
        }
    }

    private void Add()
    {
        TEditViewModel vm = (TEditViewModel)_vmFactory.CreateEditViewModel<TEntity>();
        
        vm.Saved += OnSaved;
        vm.ErrorOccured += OnErrorOccured;
        vm.Removed += OnRemoved;

        Items.Add(vm);
        SelectedItem = vm;
    }
    
    public bool IsRedactingEnabled => SelectedItem != null;

    public ObservableCollection<TEditViewModel> Items
    {
        get => _items;
        set { _items = value; NotifyPropertyChanged(); }
    }

    public TEditViewModel? SelectedItem
    {
        get => _selectedItem;
        set { _selectedItem = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(IsRedactingEnabled)); }
    }
}