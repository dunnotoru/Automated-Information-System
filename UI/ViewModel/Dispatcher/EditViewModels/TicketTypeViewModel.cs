using Domain.Models;
using Domain.RepositoryInterfaces;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class TicketTypeViewModel : ViewModelBase
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private string _name;
        private int _modifier;

        public int Id { get; }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int Modifier
        {
            get { return _modifier; }
            set { _modifier = value; OnPropertyChanged(); }
        }

        public TicketTypeViewModel(TicketType ticketType, ITicketTypeRepository ticketTypeRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;

            Id = ticketType.Id;
            Name = ticketType.Name;
            Modifier = ticketType.PriceModifierInPercent;
        }

        public TicketType GetTicketType()
        {
            return _ticketTypeRepository.GetById(Id);
        }

    }
}
