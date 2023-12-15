using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class StationMenuViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private readonly IMessageBoxService _messageBoxService;

        private ObservableCollection<StationEditViewModel> _stations;
        private StationEditViewModel _selectedStation;
        private State _currentState;

        public ObservableCollection<StationEditViewModel> Stations
        {
            get { return _stations; }
            set { _stations = value; OnPropertyChanged(); }
        }

        public StationEditViewModel SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedStation != null;
        
        public ICommand AddCommand { get; }

        public StationMenuViewModel(IStationRepository stationRepository, 
            IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            _stationRepository = stationRepository;
            _messageBoxService = messageBoxService;

            Stations = new ObservableCollection<StationEditViewModel>();
            IEnumerable<Station> stations = _stationRepository.GetAll();
            foreach (Station item in stations)
            {
                StationEditViewModel vm = new StationEditViewModel(item, _stationRepository);
                vm.RemoveEvent += OnRemove;
                vm.ErrorEvent += OnError;
                Stations.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            StationEditViewModel vm = new StationEditViewModel(_stationRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Stations.Add(vm);
            SelectedStation = vm;
        }

        private void OnRemove(StationEditViewModel viewModel)
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
