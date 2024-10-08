using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class FreighterEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Freighter
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Freighter>);

    public FreighterEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    public FreighterEditViewModel(Freighter freighter, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = freighter.Id;
        Name = freighter.Name;
    }
    
    protected override bool CanSave() => !string.IsNullOrEmpty(Name);
    
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

}