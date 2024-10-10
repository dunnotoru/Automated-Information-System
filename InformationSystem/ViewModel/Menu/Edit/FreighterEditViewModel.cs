using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class FreighterEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public FreighterEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    public FreighterEditViewModel(Freighter freighter, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = freighter.Id;
        Name = freighter.Name;
    }
    
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }

    public override ICommand SaveCommand { get; }
    public override ICommand RemoveCommand { get; }
    protected override bool CanSave() => !string.IsNullOrEmpty(Name);
}