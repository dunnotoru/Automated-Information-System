using Domain.Models;
using System;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    class TicketSaleParentViewModel : ViewModelBase
    {
        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private Station _departureStation;
        private Station _arrivalStation;
        private Run _selectedRun;

        public DateTime DepartureDateTime
        {
            get => _departureDateTime;
            set
            {
                _departureDateTime = value;
                NotifyPropertyChanged(nameof(DepartureDateTime));
            }
        }
        public DateTime ArrivalDateTime
        {
            get => _arrivalDateTime;
            set
            {
                _arrivalDateTime = value;
                NotifyPropertyChanged(nameof(ArrivalDateTime));
            }
        }
        public Station DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                NotifyPropertyChanged(nameof(ArrivalStation));
            }
        }
        public Run SelectedRun
        {
            get => _selectedRun;
            set
            {
                _selectedRun = value;
                NotifyPropertyChanged(nameof(SelectedRun));
            }
        }


        private readonly NavigationStore _navigationStore;
        
        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set
            {
                _navigationStore.CurrentViewModel = value;
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public TicketSaleParentViewModel(NavigationStore navigationStore,
            NavigationService runSearchViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            runSearchViewModelNavigationService.Navigate();
        }

        public void OnCurrentViewModelChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
