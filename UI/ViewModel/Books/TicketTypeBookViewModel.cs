using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class TicketTypeBookViewModel : ViewModelBase
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IMessageBoxService _messageBoxService;

        private ObservableCollection<TicketTypeEditViewModel> _stations;
        private TicketTypeEditViewModel _selectedStation;

        public ObservableCollection<TicketTypeEditViewModel> Stations
        {
            get { return _stations; }
            set { _stations = value; OnPropertyChanged(); }
        }

        public TicketTypeEditViewModel SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedStation != null;

        public ICommand AddCommand { get; }

        public TicketTypeBookViewModel(ITicketTypeRepository ticketTypeRepository,
            IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(ticketTypeRepository);
            _ticketTypeRepository = ticketTypeRepository;
            _messageBoxService = messageBoxService;

            Stations = new ObservableCollection<TicketTypeEditViewModel>();
            IEnumerable<TicketType> stations = _ticketTypeRepository.GetAll();
            foreach (TicketType item in stations)
            {
                TicketTypeEditViewModel vm = new TicketTypeEditViewModel(item, _ticketTypeRepository);
                vm.RemoveEvent += OnRemove;
                vm.ErrorEvent += OnError;
                Stations.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            TicketTypeEditViewModel vm = new TicketTypeEditViewModel(_ticketTypeRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Stations.Add(vm);
            SelectedStation = vm;
        }

        private void OnRemove(TicketTypeEditViewModel viewModel)
        {
            viewModel.RemoveEvent -= OnRemove;
            viewModel.ErrorEvent -= OnError;
            if (Stations.Remove(viewModel))
            {
                _messageBoxService.ShowMessage("Станция удалена");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
