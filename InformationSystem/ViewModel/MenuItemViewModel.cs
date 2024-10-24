using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InformationSystem.Command;

namespace InformationSystem.ViewModel;

internal class MenuItemViewModel : ViewModelBase
{
    private string _name;
    private readonly Func<ViewModelBase>? _getViewModel;
        
    public event EventHandler<Func<ViewModelBase>>? ViewModelChanged;
        
    public bool IsReadRequired { get; }
    public bool IsWriteRequired { get; }
    public bool IsDeleteRequired { get; }
    public bool IsEditRequired { get; }

    public bool Visible => !IsReadRequired;

    public ObservableCollection<MenuItemViewModel> Items { get; set; }

    public MenuItemViewModel(string name, IEnumerable<MenuItemViewModel> subItems)
    {
        ArgumentNullException.ThrowIfNull(subItems);
        Items = new ObservableCollection<MenuItemViewModel>();
        _name = name;
        foreach (MenuItemViewModel item in subItems)
        {
            item.ViewModelChanged += OnViewModelChanged;
            Items.Add(item);
        }

        IsReadRequired = false;
        IsWriteRequired = false;
        IsEditRequired = false;
        IsDeleteRequired = false;
    }

    private void OnViewModelChanged(object? sender, Func<ViewModelBase> e)
    {
        ViewModelChanged?.Invoke(sender, e);
    }

    public MenuItemViewModel(string name, Func<ViewModelBase> getViewModel)
    {
        Items = new ObservableCollection<MenuItemViewModel>();
        _getViewModel = getViewModel;
        _name = name;

        IsReadRequired = false;
        IsWriteRequired = false;
        IsEditRequired = false;
        IsDeleteRequired = false;
    }

    public ICommand MenuItemCommand
    {
        get => new RelayCommand(Handler);
    }

    private void Handler()
    {
        if (_getViewModel != null)
            ViewModelChanged?.Invoke(this, _getViewModel);
    }

    public string Name
    {
        get => _name;
        set { _name = value; RaisePropertyChanged(); }
    }

}