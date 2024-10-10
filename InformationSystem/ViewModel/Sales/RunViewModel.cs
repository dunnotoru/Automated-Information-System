using System;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.Sales;

public class RunViewModel : ViewModelBase
{
    private int _id;
    private string _number;
    private string _routeName;
    private int _freePlaces;

    public RunViewModel(Run run, int freePlaces)
    {
        Id = run.Id;
        Number = run.Number;
        DepartureDateTime = run.DepartureDateTime;
        EstimatedArrivalDateTime = run.EstimatedArrivalDateTime;
        FreePlaces = freePlaces;
    }

    public int FreePlaces
    {
        get => _freePlaces;
        set => _freePlaces = value;
    }
    public int Id
    {
        get => _id;
        private set => _id = value;
    }
    public string Number
    {
        get => _number;
        set { _number = value; RaisePropertyChanged(); }
    }
    public string RouteName => "route";

    public DateTime DepartureDateTime { get; }

    public DateTime EstimatedArrivalDateTime { get; }
}