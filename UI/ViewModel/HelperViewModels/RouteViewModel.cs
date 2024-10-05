using Domain.Models;

namespace UI.ViewModel.HelperViewModels;

internal class RouteViewModel : ViewModelBase
{
    private int _id;
    private string _name;

    public RouteViewModel(Route route)
    {
        Id = route.Id;
        Name = route.Name;
    }

    public RouteViewModel()
    {
        Id = 0;
        Name = "";
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); }
    }
}