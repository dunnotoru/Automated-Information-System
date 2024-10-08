using System;
using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.Sales;

public class RunViewModel : ObservableObject
{
    private int _id;
    private string _number;
    private string _routeName;
    private DateTime _departureDateTime;
    private DateTime _estimatedArrivalDateTime;
    private int _freePlaces;

    public RunViewModel(Run run, int freePlaces)
    {
        Id = run.Id;
        Number = run.Number;
        RouteName = run.Route.Name;
        DepartureDateTime = run.DepartureDateTime;
        EstimatedArrivalDateTime = run.EstimatedArrivalDateTime;
        FreePlaces = freePlaces;
    }

    public int FreePlaces
    {
        get { return _freePlaces; }
        set { _freePlaces = value; }
    }
    public int Id
    {
        get { return _id; }
        private set { _id = value; }
    }
    public string Number
    {
        get { return _number; }
        set { _number = value; OnPropertyChanged(); }
    }
    public string RouteName
    {
        get { return _routeName; }
        set { _routeName = value; OnPropertyChanged(); }
    }
    public DateTime DepartureDateTime
    {
        get { return _departureDateTime; }
        set { _departureDateTime = value; OnPropertyChanged(); }
    }
    public DateTime EstimatedArrivalDateTime
    {
        get { return _estimatedArrivalDateTime; }
        set { _estimatedArrivalDateTime = value; OnPropertyChanged(); }
    }
}