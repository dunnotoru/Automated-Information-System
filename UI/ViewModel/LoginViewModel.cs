using Domain.Models;
using Domain.Services;
using System;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.Stores;

namespace UI.ViewModel;

internal class LoginViewModel : ViewModelBase
{
    private readonly AuthenticationService _authenticationService;
    private readonly AccountStore _accountStore;
    private readonly IMessageBoxService _messageBoxService;
        
    private string _username;
    private string _password;

    public event EventHandler AuthenticationDone;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public RelayCommand LoginCommand { get; }

    public void Authenticate()
    {
        try
        {
            Account acc = _authenticationService.Authenticate(Username, Password);
            _accountStore.CurrentAccount = acc;
            AuthenticationDone?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e) 
        {
            _messageBoxService.ShowMessage(e.Message);
        }
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    public LoginViewModel(AuthenticationService authenticationUseCase,
        AccountStore accountStore,
        IMessageBoxService messageBoxService)
    {
        _authenticationService = authenticationUseCase;
        _accountStore = accountStore;
        _messageBoxService = messageBoxService;

        Username = string.Empty;
        Password = string.Empty;

        LoginCommand = new RelayCommand(Authenticate, CanLogin);
    }
}