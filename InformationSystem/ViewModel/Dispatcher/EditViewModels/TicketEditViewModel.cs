using System;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.Dispatcher.EditViewModels;

class TicketEditViewModel : ViewModelBase
{
    private int _id;
    private string _number;
    private DateTime _bookDate;
    private string _name;
    private string _surname;
    private string _patronymic;
    private string _runNumber;
    private string _ticketTypeName;
    private int _price;
    private string _series;

    public event EventHandler<Exception> Error;

    public TicketEditViewModel(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket);
        Id = ticket.Id;
        RunNumber = ticket.Run.Number;
        BookDate = ticket.BookDate;
        Series = ticket.IdentityDocument.Series;
        Number = ticket.IdentityDocument.Number;
        Name = ticket.IdentityDocument.Name;
        Surname = ticket.IdentityDocument.Surname;
        Patronymic = ticket.IdentityDocument.Patronymic;
        TicketTypeName = ticket.TicketType.Name;
        Price = ticket.Price;
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }
    public string Series
    {
        get { return _series; }
        set { _series = value; NotifyPropertyChanged(); }
    }
    public string Number
    {
        get { return _number; }
        set { _number = value; NotifyPropertyChanged(); }
    }
    public DateTime BookDate
    {
        get { return _bookDate; }
        set { _bookDate = value; NotifyPropertyChanged(); }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }
    public string Surname
    {
        get { return _surname; }
        set { _surname = value; NotifyPropertyChanged(); }
    }
    public string Patronymic
    {
        get { return _patronymic; }
        set { _patronymic = value; NotifyPropertyChanged(); }
    }
    public string RunNumber
    {
        get { return _runNumber; }
        set { _runNumber = value; NotifyPropertyChanged(); }
    }
    public string TicketTypeName
    {
        get { return _ticketTypeName; }
        set { _ticketTypeName = value; NotifyPropertyChanged(); }
    }
    public int Price
    {
        get { return _price; }
        set { _price = value; NotifyPropertyChanged(); }
    }
}