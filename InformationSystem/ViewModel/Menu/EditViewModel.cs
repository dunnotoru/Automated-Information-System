using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public abstract class EditViewModel : ViewModelBase
{
    protected readonly IDbContextFactory<DomainContext> ContextFactory;

    public event EventHandler? Saved;
    public event EventHandler? Removed;
    public event EventHandler<Exception>? ErrorOccured;

    public abstract ICommand SaveCommand { get; }
    public abstract ICommand RemoveCommand { get; }

    protected EditViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        ContextFactory = contextFactory;
    }
    
    protected abstract bool CanSave();

    protected void ExecuteSave<TEntity>(Func<TEntity> entityFactory)
        where TEntity : EntityBase
    {
        DomainContext context = ContextFactory.CreateDbContext();

        try
        {
            TEntity entity = entityFactory();
            context.Update<TEntity>(entity);
            context.SaveChanges();
            Id = entity.Id;
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
    
    protected void ExecuteRemove<TEntity>() where TEntity : EntityBase
    {
        DomainContext context = ContextFactory.CreateDbContext();
        
        try
        {
            context.Set<TEntity>()
                .Where(o => o.Id == Id)
                .ExecuteDelete();
            context.SaveChanges();
            
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

    public int Id { get; protected set; }
}