using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;

namespace InformationSystem.ViewModel.Menu.Edit;

public class StationForRouteSelectionViewModel : ViewModelBase
{
    private ObservableCollection<StationViewModel> _allStations;
    private ObservableCollection<StationViewModel> _usedStations;
    private StationViewModel? _selectedStationFromAll = null;
    private StationViewModel? _selectedStationFromUsed = null; 
    
    public ICommand UseStationCommand { get; }
    public ICommand FreeStationCommand { get; }
    public ICommand MoveUpCommand { get; }
    public ICommand MoveDownCommand { get; }

    public StationForRouteSelectionViewModel(ObservableCollection<StationViewModel> allStations,
        ObservableCollection<StationViewModel> usedStations)
    {
        _allStations = allStations;
        _usedStations = usedStations;

        UseStationCommand = new RelayCommand(UseStation);
        FreeStationCommand = new RelayCommand(FreeStation);
        MoveUpCommand = new RelayCommand(MoveUp);
        MoveDownCommand = new RelayCommand(MoveDown);
    }

    private void UseStation()
    {
        if (SelectedStationFromAll is null)
        {
            return;
        }
        
        UsedStations.Add(SelectedStationFromAll);
        AllStations.Remove(SelectedStationFromAll);
    }

    private void FreeStation()
    {
        if (SelectedStationFromUsed is null)
        {
            return;
        }
        
        AllStations.Add(SelectedStationFromUsed);
        UsedStations.Remove(SelectedStationFromUsed);
    }

    private void MoveUp()
    {
        if (SelectedStationFromUsed is null)
        {
            return;
        }

        int index = UsedStations.IndexOf(SelectedStationFromUsed);
        UsedStations.Move(index, Math.Clamp(index - 1, 0, UsedStations.Count - 1));
    }

    private void MoveDown()
    {
        if (SelectedStationFromUsed is null)
        {
            return;
        }

        int index = UsedStations.IndexOf(SelectedStationFromUsed);
        UsedStations.Move(index, Math.Clamp(index + 1, 0, UsedStations.Count - 1));
    }

    public ObservableCollection<StationViewModel> AllStations
    {
        get => _allStations;
        set { _allStations = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<StationViewModel> UsedStations
    {
        get => _usedStations;
        set { _usedStations = value; RaisePropertyChanged(); }
    }

    public StationViewModel? SelectedStationFromAll
    {
        get => _selectedStationFromAll;
        set { _selectedStationFromAll = value; RaisePropertyChanged(); }
    }

    public StationViewModel? SelectedStationFromUsed
    {
        get => _selectedStationFromUsed;
        set { _selectedStationFromUsed = value; RaisePropertyChanged(); }
    }
}