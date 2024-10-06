using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Books.EditViewModels;

internal class BrandEditViewModel : ViewModelBase
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private int _id;
    private string _name = string.Empty;

    public event EventHandler? Save;
    public event EventHandler? Remove;
    public event EventHandler<Exception>? Error;

    public ICommand SaveCommand => new RelayCommand(ExecuteSave, CanSave);
    public ICommand RemoveCommand => new RelayCommand(ExecuteRemove);

    public BrandEditViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : this(contextFactory)
    {
        Id = brand.Id;
        Name = brand.Name;
    }

    public BrandEditViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;

        Id = 0;
        Name = string.Empty;
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    private void ExecuteSave()
    {
        Brand brand = new Brand()
        {
            Id = _id,
            Name = _name,
        };

        try
        {
            if (Id == 0)
            {
                Id = _brandRepository.Create(brand);
            }
            else
            {
                _brandRepository.Update(Id, brand);
            }
            Save?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }
    private void ExecuteRemove()
    {
        if (Id == 0) return;

        try
        {
            _brandRepository.Remove(Id);
            Remove?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }
}