using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public abstract class EditViewModel : ViewModelBase
{
    protected readonly IDbContextFactory<DomainContext> ContextFactory;

    public event EventHandler? Saved;
    public event EventHandler? Removed;
    public event EventHandler<Exception>? ErrorOccured;

    public ICommand SaveCommand => new RelayCommand(ExecuteSave, CanSave);
    public ICommand RemoveCommand => new RelayCommand(ExecuteRemove);

    protected EditViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        ContextFactory = contextFactory;
    }

    protected abstract bool CanSave();
    protected abstract void ExecuteSave();
    protected abstract void ExecuteRemove();

    protected void RaiseSaved()
    {
        Saved?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseRemoved()
    {
        Removed?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseOnErrorOccured(Exception e)
    {
        ErrorOccured?.Invoke(this, e);
    }

    public int Id { get; protected set; }
}