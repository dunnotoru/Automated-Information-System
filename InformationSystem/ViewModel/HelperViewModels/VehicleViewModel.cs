﻿using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class VehicleViewModel : ViewModelBase
{
    private int _id;
    private string _licensePlateNumber;

    public VehicleViewModel(Vehicle vehicle)
    {
        Id = vehicle.Id;
        LicensePlateNumber = vehicle.LicensePlateNumber;
    }

    public VehicleViewModel()
    {
        Id = 0;
        LicensePlateNumber = "";
    }

    public string LicensePlateNumber
    {
        get { return _licensePlateNumber; }
        set { _licensePlateNumber = value; }
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }
}