using System;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public abstract class EditViewModel : ViewModelBase
{
    protected readonly IDbContextFactory<DomainContext> ContextFactory;

    public int Id { get; protected set; }
    
    public event EventHandler? Saved;
    public event EventHandler? Removed;
    public event EventHandler<Exception>? ErrorOccured;
    
    protected abstract int? Save(DomainContext context);
    protected abstract void Remove(DomainContext context);
    protected abstract bool CanSave();

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    protected EditViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        SaveCommand = new RelayCommand(SaveRoutine, CanSave);
        RemoveCommand = new RelayCommand(RemoveRoutine, CanSave);
        ContextFactory = contextFactory;
    }

    protected void SaveRoutine()
    {
        DomainContext context = ContextFactory.CreateDbContext();
        
        try
        {
            Id = Save(context) ?? 0;
            RaiseSaved();
        }
        catch (Exception e)
        {
            RaiseErrorOccured(e);
        }
        finally
        {
            context.Dispose();
        }
    }
    
    protected void RemoveRoutine()
    {
        DomainContext context = ContextFactory.CreateDbContext();
        
        try
        {
            Remove(context);
            RaiseRemoved();
        }
        catch (Exception ex)
        {
            RaiseErrorOccured(ex);
        }
        finally
        {
            context.Dispose();
        }
    }
    
    protected void RaiseSaved()
    {
        Saved?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseRemoved()
    {
        Removed?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseErrorOccured(Exception e)
    {
        ErrorOccured?.Invoke(this, e);
    }

}