using Domain.Services;
using System;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.Stores;

namespace UI.ViewModel;

internal class RegistrationViewModel : ViewModelBase
{
    private readonly RegistrationService _registrationService;
    private readonly AccountStore _accountStore;
    private readonly IMessageBoxService _messageBoxService;

    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassport = string.Empty;

    public event EventHandler? RegistrationDone;

    public ICommand RegisterCommand => new RelayCommand(Register);

    public RegistrationViewModel(RegistrationService registrationService, 
        AccountStore accountStore, IMessageBoxService messageBoxService)
    {
        _registrationService = registrationService;
        _accountStore = accountStore;
        _messageBoxService = messageBoxService;
    }

    private void Register()
    {
        try
        {
            _registrationService.Register(Username, Password, true, true, true, true);
            RegistrationDone?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            _messageBoxService.ShowMessage(e.InnerException?.Message ?? e.Message);
        }
    }

    private bool CanRegister()
    {
        return !string.IsNullOrWhiteSpace(_username) &&
               !string.IsNullOrWhiteSpace(_password) &&
               !string.IsNullOrWhiteSpace(_confirmPassport);
    }

    public string Username
    {
        get => _username;
        set { _username = value; NotifyPropertyChanged(); }
    }
    
    public string Password
    {
        get => _password;
        set { _password = value; NotifyPropertyChanged(); }
    }
    
    public string ConfirmPassport
    {
        get => _confirmPassport;
        set { _confirmPassport = value; NotifyPropertyChanged(); }
    }
}