using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel.Dispatcher;

internal class StationMenuViewModel : ViewModelBase
{
    private readonly IStationRepository _stationRepository;
    private readonly IMessageBoxService _messageBoxService;

    private ObservableCollection<StationEditViewModel> _stations;
    private StationEditViewModel _selectedStation;

    public ObservableCollection<StationEditViewModel> Stations
    {
        get { return _stations; }
        set { _stations = value; NotifyPropertyChanged(); }
    }

    public StationEditViewModel SelectedStation
    {
        get => _selectedStation;
        set { _selectedStation = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
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
            vm.Remove += OnRemove;
            vm.Save += OnSave;
            vm.Error += OnError;
            Stations.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void Add()
    {
        StationEditViewModel vm = new StationEditViewModel(_stationRepository);
        vm.Remove += OnRemove;
        vm.Save += OnSave;
        vm.Error += OnError;
        Stations.Add(vm);
        SelectedStation = vm;
    }

    private void OnRemove(object? sender, EventArgs eventArgs)
    {
        StationEditViewModel vm = (StationEditViewModel)sender;
        vm.Remove -= OnRemove;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        if (Stations.Remove(vm))
        {
            _messageBoxService.ShowMessage("Данные успешно удалены");
        }
    }

    private void OnSave(object? sender, EventArgs eventArgs)
    {
        StationEditViewModel vm = (StationEditViewModel)sender;
        vm.Remove -= OnRemove;
        vm.Save -= OnSave;
        vm.Error -= OnError;
            
        Station station = _stationRepository.GetById(vm.Id);
        StationEditViewModel updatedVm = new StationEditViewModel(station,_stationRepository);
            
        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;
            
        int index = Stations.IndexOf(vm);
        Stations.Insert(index, updatedVm);
        Stations.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены");
    }

    private void OnError(object? sender, Exception exception)
    {
        _messageBoxService.ShowMessage($"Ошибка: {exception.Message}");
    }
}