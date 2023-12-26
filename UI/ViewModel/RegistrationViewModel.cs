using Domain.Models;
using Domain.Services;
using System;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class RegistrationViewModel : ViewModelBase
    {
        private readonly RegistrationService _registrationService;
        private readonly AccountStore _accountStore;
        private readonly IMessageBoxService _messageBoxService;

        private string _username;
        private string _password;
        private string _confirmPassport;


        public event EventHandler Registration;

        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        public RegistrationViewModel(RegistrationService registrationService, 
            AccountStore accountStore, IMessageBoxService messageBoxService)
        {
            _registrationService = registrationService;
            _accountStore = accountStore;
            _messageBoxService = messageBoxService;
        }

        private void Register()
        {
            _registrationService.Register(Username, Password, true, true, true, true);
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
            set { _username = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }
        public string ConfirmPassport
        {
            get => _confirmPassport;
            set { _confirmPassport = value; OnPropertyChanged(); }
        }
    }
}