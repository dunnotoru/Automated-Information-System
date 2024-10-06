using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel;

public class MenuViewModel<TEditViewModel, TEntity> : ViewModelBase
    where TEditViewModel : EditViewModel
    where TEntity : EntityBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IDbContextFactory<DomainContext> _factory;
    private readonly EditViewModelFactory _vmFactory;

    private ObservableCollection<TEditViewModel> _items;
    private TEditViewModel? _selectedItem;

    public ICommand AddCommand;

    public MenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> factory, EditViewModelFactory vmFactory)
    {
        _messageBoxService = messageBoxService;
        _factory = factory;
        _vmFactory = vmFactory;

        _items = new ObservableCollection<TEditViewModel>();
        
        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        if (sender is not TEditViewModel vm)
        {
            return;
        }
        
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        Items.Remove(vm);
        
        _messageBoxService.ShowMessage("data removed successfully");
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage(e.Message);
    }
    
    private void OnSave(object? sender, EventArgs e)
    {
        if (sender is not TEditViewModel vm)
        {
            return;
        }
        
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        using (DomainContext context = _factory.CreateDbContext())
        {
            TEntity entity = context.Set<TEntity>().First(o => o.Id == vm.Id);
            TEditViewModel updatedVm = (TEditViewModel)_vmFactory.CreateEditViewModel(entity);
            
            updatedVm.Save += OnSave;
            updatedVm.Error += OnError;
            updatedVm.Remove += OnRemove;
            
            int index = Items.IndexOf(vm);
            Items.Insert(index, updatedVm);
            Items.Remove(vm);
            
            _messageBoxService.ShowMessage("data saved successfully.");
        }
    }

    private void Add()
    {
        TEditViewModel vm = (TEditViewModel)_vmFactory.CreateEditViewModel<TEntity>();
        
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;

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
        set { _selectedItem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }
}