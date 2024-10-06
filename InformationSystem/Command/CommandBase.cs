using System;
using System.Windows.Input;

namespace InformationSystem.Command;

public abstract class CommandBase : ICommand
{
    public virtual event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public abstract void Execute(object? parameter);

    protected void OnCanExecutedChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}