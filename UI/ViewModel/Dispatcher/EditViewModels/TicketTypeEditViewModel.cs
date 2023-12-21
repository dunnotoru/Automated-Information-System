using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class TicketTypeEditViewModel : ViewModelBase
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private string _name;
        private int _modifier;

        public Action<TicketTypeEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public TicketTypeEditViewModel(TicketType ticketType, ITicketTypeRepository ticketTypeRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(ticketType);
            ArgumentNullException.ThrowIfNull(ticketTypeRepository);

            Id = ticketType.Id;
            Name = ticketType.Name;
            Modifier = ticketType.PriceModifierInPercent;

            _ticketTypeRepository = ticketTypeRepository;
        }

        public TicketTypeEditViewModel(ITicketTypeRepository stationRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(stationRepository);

            Id = 0;
            Name = "";
            Modifier = 100;
            _ticketTypeRepository = stationRepository;
        }

        private TicketTypeEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, () => CanSave());
            RemoveCommand = new RelayCommand(Remove);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public int Modifier
        {
            get => _modifier;
            set { _modifier = value; OnPropertyChanged(); }
        }

        public void Save()
        {
            TicketType createdStation = new TicketType()
            {
                Name = Name,
                PriceModifierInPercent = Modifier,
            };
            try
            {
                if (Id == 0)
                {
                    _ticketTypeRepository.Add(createdStation);
                }
                else
                {
                    _ticketTypeRepository.Update(Id, createdStation);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        public void Remove()
        {
            if (Id == 0) return;
            try
            {
                _ticketTypeRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }
    }
}
