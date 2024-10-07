﻿using System;
using System.Windows.Input;

namespace InformationSystem.Command;

internal class RelayCommand : CommandBase
{
    public override event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    private readonly Action? _methodToExecute;
    private readonly Func<bool>? _canExecuteEvaluator;
    public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
    {
        _methodToExecute = methodToExecute;
        _canExecuteEvaluator = canExecuteEvaluator;
    }
    public RelayCommand(Action methodToExecute)
        : this(methodToExecute, null)
    {
    }
    public override bool CanExecute(object? parameter)
    {
        if (_canExecuteEvaluator == null)
        {
            return true;
        }
        else
        {
            bool result = _canExecuteEvaluator.Invoke();
            return result;
        }
    }
    public override void Execute(object? parameter)
    {
        _methodToExecute?.Invoke();
    }
}