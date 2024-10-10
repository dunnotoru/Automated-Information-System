using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.Sales;

internal class OrderViewModel : ViewModelBase
{
    private Station _departureStation;
    private Station _arrivalStation;
    private Run _selectedRun;

    public Station DepartureStation => _departureStation;
    public Station ArrivalStation => _arrivalStation;
    public Run SelectedRun => _selectedRun;

    public OrderViewModel(Station departureStation, Station arrivalStation, Run selectedRun)
    {
        _departureStation = departureStation;
        _arrivalStation = arrivalStation;
        _selectedRun = selectedRun;
    }
}