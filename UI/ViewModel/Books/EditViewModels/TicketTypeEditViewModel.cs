using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Books.EditViewModels;

internal class TicketTypeEditViewModel : ViewModelBase
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private string _name;
    private int _modifier;

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    public TicketTypeEditViewModel(TicketType ticketType, ITicketTypeRepository ticketTypeRepository) : this(ticketTypeRepository)
    {
        ArgumentNullException.ThrowIfNull(ticketType);

        Id = ticketType.Id;
        Name = ticketType.Name;
        Modifier = ticketType.PriceModifierInPercent;
    }

    public TicketTypeEditViewModel(ITicketTypeRepository ticketTypeRepository)
    {
        ArgumentNullException.ThrowIfNull(ticketTypeRepository);
        _ticketTypeRepository = ticketTypeRepository;

        Id = 0;
        Name = "";
        Modifier = 100;

        SaveCommand = new RelayCommand(ExecuteSave, CanSave);
        RemoveCommand = new RelayCommand(ExecuteRemove);
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    public int Id { get; set; }
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
    public int Modifier
    {
        get => _modifier;
        set { _modifier = value; NotifyPropertyChanged(); }
    }

    public void ExecuteSave()
    {
        TicketType ticketType = new TicketType()
        {
            Name = Name,
            PriceModifierInPercent = Modifier,
        };
        try
        {
            if (Id == 0)
            {
                Id = _ticketTypeRepository.Create(ticketType);
            }
            else
            {
                _ticketTypeRepository.Update(Id, ticketType);
            }
            Save?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    public void ExecuteRemove()
    {
        if (Id == 0) return;
        try
        {
            _ticketTypeRepository.Remove(Id);
            Remove?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }
}