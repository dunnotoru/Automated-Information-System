using System;
using System.Collections.ObjectModel;
using System.Linq;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;

namespace InformationSystem.ViewModel.Sales;

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

    public PassengerViewModel()
    {
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
        set { _name = value; RaisePropertyChanged(); }
    }
    public string Surname
    {
        get => _surname;
        set { _surname = value; RaisePropertyChanged(); }
    }
    public string Patronymic
    {
        get => _patronymic;
        set { _patronymic = value; RaisePropertyChanged(); }
    }
    public string Series
    {
        get => _series;
        set { _series = value; RaisePropertyChanged(); }
    }
    public string Number
    {
        get => _number;
        set { _number = value; RaisePropertyChanged(); }
    }
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set { _dateOfBirth = value; RaisePropertyChanged(); }
    }

    public TicketTypeViewModel SelectedTicketType
    {
        get { return _selectedTicketType; }
        set { _selectedTicketType = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<TicketTypeViewModel> TicketTypes
    {
        get { return _ticketTypes; }
        set { _ticketTypes = value; RaisePropertyChanged(); }
    }
}