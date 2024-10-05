using Domain.Models;

namespace UI.ViewModel.HelperViewModels;

internal class TicketTypeViewModel : ViewModelBase
{
    private string _name;
    private int _modifier;

    public int Id { get; }

    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }

    public int Modifier
    {
        get { return _modifier; }
        set { _modifier = value; NotifyPropertyChanged(); }
    }

    public TicketTypeViewModel(TicketType ticketType)
    {
        Id = ticketType.Id;
        Name = ticketType.Name;
        Modifier = ticketType.PriceModifierInPercent;
    }
}