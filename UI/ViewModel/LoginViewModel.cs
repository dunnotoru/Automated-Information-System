using Domain.Models;
using Domain.Services;
using System;
using UI.Command;
using UI.Stores;

namespace UI.ViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public RelayCommand LoginCommand
        {
            get => new RelayCommand(Authenticate);
        }

        public void Authenticate()
        {
            bool result = true; // _authenticationService.Authenticate(Username, Password);
            _accountStore.CurrentAccount = new Account() { Username = "root", PasswordHash = "root_HASH" };
            if (result)
                AuthenticationDone?.Invoke();
        }

        private readonly AuthenticationService _authenticationService;
        private readonly AccountStore _accountStore;
        public event Action AuthenticationDone;

        public LoginViewModel(AuthenticationService authenticationUseCase,
            AccountStore accountStore)
        {
            _authenticationService = authenticationUseCase;
            _accountStore = accountStore;
        }

    }
}
