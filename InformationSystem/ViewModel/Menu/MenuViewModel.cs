using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Factories;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public abstract class MenuViewModel<TEditViewModel, TEntity> : ObservableObject
    where TEditViewModel : EditViewModel
    where TEntity : EntityBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private readonly IViewModelFactory _vmFactory;

    private ObservableCollection<TEditViewModel> _items;
    private TEditViewModel? _selectedItem;

    public ICommand AddCommand { get; }

    protected MenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory)
    {
        _messageBoxService = messageBoxService;
        _contextFactory = contextFactory;
        _vmFactory = vmFactory;

        _items = new ObservableCollection<TEditViewModel>();
        using (DomainContext context = _contextFactory.CreateDbContext())
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

    private void Add()
    {
        TEditViewModel vm = (TEditViewModel)_vmFactory.CreateEditViewModel<TEntity>();
        
        vm.Saved += OnSaved;
        vm.ErrorOccured += OnErrorOccured;
        vm.Removed += OnRemoved;

        Items.Add(vm);
        SelectedItem = vm;
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

        using DomainContext context = _contextFactory.CreateDbContext();
        
        TEntity? entity = context.Set<TEntity>().Find(vm.Id);
        
        TEditViewModel updatedVm = (TEditViewModel)_vmFactory.CreateEditViewModel(entity);
            
        updatedVm.Saved += OnSaved;
        updatedVm.ErrorOccured += OnErrorOccured;
        updatedVm.Removed += OnRemoved;
            
        int index = Items.IndexOf(vm);
        Items.Insert(index, updatedVm);
        Items.Remove(vm);
            
        _messageBoxService.ShowMessage("data saved successfully.");
    }

    
    public bool IsRedactingEnabled => SelectedItem != null;

    public ObservableCollection<TEditViewModel> Items
    {
        get => _items;
        set { _items = value; OnPropertyChanged(); }
    }

    public TEditViewModel? SelectedItem
    {
        get => _selectedItem;
        set { _selectedItem = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsRedactingEnabled)); }
    }
}