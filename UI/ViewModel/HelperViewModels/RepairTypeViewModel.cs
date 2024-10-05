using Domain.Models;

namespace UI.ViewModel.HelperViewModels;

internal class RepairTypeViewModel : ViewModelBase
{
    private int _id;
    private string _name;

    public RepairTypeViewModel(RepairType repairType)
    {
        Id = repairType.Id;
        Name = repairType.Name;
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