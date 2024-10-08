using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class RepairTypeViewModel : ObservableObject
{
    private int _id;
    private string _name;

    public RepairTypeViewModel(RepairType repairType)
    {
        _id = repairType.Id;
        _name = repairType.Name;
    }

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}