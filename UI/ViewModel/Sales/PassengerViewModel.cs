using System;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Sales;

internal class PassengerViewModel : ViewModelBase
{
    private string _name;
    private string _surname;
    private string _patronymic;
    private string _series;
    private string _number;
    private DateTime _dateOfBirth;
    private ObservableCollection<TicketTypeViewModel> _ticketTypes;
    private TicketTypeViewModel _selectedTicketType;

    public PassengerViewModel(ITicketTypeRepository ticketTypeRepository)
    {
        ArgumentNullException.ThrowIfNull(ticketTypeRepository);

        _ticketTypes = new ObservableCollection<TicketTypeViewModel>();
        foreach (var item in ticketTypeRepository.GetAll())
        {
            TicketTypeViewModel vm = new TicketTypeViewModel(item);
            _ticketTypes.Add(vm);
        }
        SelectedTicketType = _ticketTypes.FirstOrDefault();
    }

    public IdentityDocument GetDocument()
    {
        return new IdentityDocument()
        {
            Name = _name,
            Surname = _surname,
            Patronymic = _patronymic,
            Number = _number,
            Series = _series,
            BirthDate = _dateOfBirth,
        };
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }
    public string Surname
    {
        get => _surname;
        set { _surname = value; OnPropertyChanged(); }
    }
    public string Patronymic
    {
        get => _patronymic;
        set { _patronymic = value; OnPropertyChanged(); }
    }
    public string Series
    {
        get => _series;
        set { _series = value; OnPropertyChanged(); }
    }
    public string Number
    {
        get => _number;
        set { _number = value; OnPropertyChanged(); }
    }
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set { _dateOfBirth = value; OnPropertyChanged(); }
    }

    public TicketTypeViewModel SelectedTicketType
    {
        get { return _selectedTicketType; }
        set { _selectedTicketType = value; OnPropertyChanged(); }
    }

    public ObservableCollection<TicketTypeViewModel> TicketTypes
    {
        get { return _ticketTypes; }
        set { _ticketTypes = value; OnPropertyChanged(); }
    }
}