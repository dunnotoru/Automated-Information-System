using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;

namespace InformationSystem.ViewModel.Books.EditViewModels;

internal class FreighterEditViewModel : ViewModelBase
{
    private readonly IFreighterRepository _freighterRepository;
    private int _id;
    private string _name;

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    public FreighterEditViewModel(Freighter freighter, IFreighterRepository brandRepository) : this()
    {
        ArgumentNullException.ThrowIfNull(brandRepository);
        Id = freighter.Id;
        Name = freighter.Name;
        _freighterRepository = brandRepository;
    }
    public FreighterEditViewModel(IFreighterRepository brandRepository) : this()
    {
        ArgumentNullException.ThrowIfNull(brandRepository);

        Id = 0;
        Name = "";
        _freighterRepository = brandRepository;
    }
    private FreighterEditViewModel()
    {
        SaveCommand = new RelayCommand(ExecuteSave, CanSave);
        RemoveCommand = new RelayCommand(ExecuteRemove);
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    private void ExecuteSave()
    {
        Freighter freighter = new Freighter()
        {
            Id = _id,
            Name = _name,
        };

        try
        {
            if (Id == 0)
            {
                Id = _freighterRepository.Create(freighter);
            }
            else
            {
                _freighterRepository.Update(Id, freighter);
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
            _freighterRepository.Remove(Id);
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