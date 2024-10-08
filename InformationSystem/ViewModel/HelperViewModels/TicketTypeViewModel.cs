using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class TicketTypeViewModel : ObservableObject
{
    public int Id { get; }
    private string _name;
    private int _modifier;

    public TicketTypeViewModel(TicketType ticketType)
    {
        Id = ticketType.Id;
        _name = ticketType.Name;
        _modifier = ticketType.PriceModifierInPercent;
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public int Modifier
    {
        get => _modifier;
        set { _modifier = value; OnPropertyChanged(); }
    }

}