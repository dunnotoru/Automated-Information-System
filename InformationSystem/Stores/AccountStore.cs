using System;
using InformationSystem.Domain.Models;

namespace InformationSystem.Stores;

internal class AccountStore
{
    private Account _currentAccount;

    public Account CurrentAccount
    {
        get => _currentAccount;
        set
        {
            _currentAccount = value;
            OnAccountChanged();
        }
    }

    public void OnAccountChanged()
    {
        AccountChanged?.Invoke();
    }

    public event Action? AccountChanged;
}