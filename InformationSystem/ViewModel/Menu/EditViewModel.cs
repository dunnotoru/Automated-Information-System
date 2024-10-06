using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel;

public abstract class EditViewModel : ViewModelBase
{
    protected readonly IDbContextFactory<DomainContext> ContextFactory;

    public event EventHandler? Save;
    public event EventHandler? Remove;
    public event EventHandler<Exception>? Error;

    public ICommand SaveCommand => new RelayCommand(ExecuteSave, CanSave);
    public ICommand RemoveCommand => new RelayCommand(ExecuteRemove);

    protected EditViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        ContextFactory = contextFactory;
    }

    protected abstract bool CanSave();
    protected abstract void ExecuteSave();
    protected abstract void ExecuteRemove();

    protected void OnSave()
    {
        Save?.Invoke(this, EventArgs.Empty);
    }

    protected void OnRemove()
    {
        Remove?.Invoke(this, EventArgs.Empty);
    }

    protected void OnError(Exception e)
    {
        Error?.Invoke(this, e);
    }

    public int Id { get; protected set; }
}