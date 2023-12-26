using Domain.Models;
using System;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
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
            set { _id = value; OnPropertyChanged(); }
        }
        public string Series
        {
            get { return _series; }
            set { _series = value; OnPropertyChanged(); }
        }
        public string Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
        }
        public DateTime BookDate
        {
            get { return _bookDate; }
            set { _bookDate = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(); }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; OnPropertyChanged(); }
        }
        public string RunNumber
        {
            get { return _runNumber; }
            set { _runNumber = value; OnPropertyChanged(); }
        }
        public string TicketTypeName
        {
            get { return _ticketTypeName; }
            set { _ticketTypeName = value; OnPropertyChanged(); }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }
    }
}
