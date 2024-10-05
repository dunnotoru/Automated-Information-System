using Domain.Models;
using System;

namespace UI.Stores;

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