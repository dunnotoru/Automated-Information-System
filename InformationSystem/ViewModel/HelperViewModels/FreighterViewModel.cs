using System;
using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class FreighterViewModel : ObservableObject
{
    private int _id;
    private string _name;

    public FreighterViewModel(Freighter freighter)
    {
        ArgumentNullException.ThrowIfNull(freighter);
        Id = freighter.Id;
        Name = freighter.Name;
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