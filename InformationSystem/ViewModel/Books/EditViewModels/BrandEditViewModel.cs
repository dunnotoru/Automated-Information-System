using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;

namespace InformationSystem.ViewModel.Books.EditViewModels;

internal class BrandEditViewModel : ViewModelBase
{
    private readonly IBrandRepository _brandRepository;
    private int _id;
    private string _name;

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    public BrandEditViewModel(Brand brand, IBrandRepository brandRepository) : this(brandRepository)
    {
        ArgumentNullException.ThrowIfNull(brand);
        Id = brand.Id;
        Name = brand.Name;
        _brandRepository = brandRepository;
    }

    public BrandEditViewModel(IBrandRepository brandRepository)
    {
        ArgumentNullException.ThrowIfNull(brandRepository);
        _brandRepository = brandRepository;

        Id = 0;
        Name = "";

        SaveCommand = new RelayCommand(ExecuteSave, CanSave);
        RemoveCommand = new RelayCommand(ExecuteRemove);
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