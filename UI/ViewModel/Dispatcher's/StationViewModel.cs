using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class StationViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private Station _selectedItem;

        public ObservableCollection<Station> Items { get; set; }
        public Station SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); }
        }

        public ICommand AddStationCommand
        {
            get => new RelayCommand(AddStation);
        }
        public ICommand DeleteStationCommand
        {
            get => new RelayCommand(DeleteStation);
        }
        public ICommand SaveChangesCommand
        {
            get => new RelayCommand(SaveChanges);
        }

        public StationViewModel(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
            Items = new ObservableCollection<Station>(_stationRepository.GetAll());
        }

        private void AddStation()
        {

        }

        private void DeleteStation()
        {

        }

        private void SaveChanges()
        {

        }
    }
}
