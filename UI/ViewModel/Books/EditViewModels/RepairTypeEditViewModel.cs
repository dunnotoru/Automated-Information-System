using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;

namespace UI.ViewModel.Books.EditViewModels;

internal class RepairTypeEditViewModel : ViewModelBase
{
    private readonly IRepairTypeRepository _repairTypeRepository;
    private int _id;
    private string _name;

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    public RepairTypeEditViewModel(RepairType repairType, IRepairTypeRepository brandRepository) : this(brandRepository)
    {
        Id = repairType.Id;
        Name = repairType.Name;
    }
    public RepairTypeEditViewModel(IRepairTypeRepository brandRepository) 
    {
        ArgumentNullException.ThrowIfNull(brandRepository);
        _repairTypeRepository = brandRepository;

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
        RepairType type = new RepairType()
        {
            Id = _id,
            Name = _name,
        };

        try
        {
            if (Id == 0)
            {
                Id = _repairTypeRepository.Create(type);
            }
            else
            {
                _repairTypeRepository.Update(Id, type);
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
            _repairTypeRepository.Remove(Id);
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