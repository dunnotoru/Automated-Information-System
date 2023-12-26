using Domain.Models;
using Domain.Services;
using System;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;


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
                AuthenticationDone?.Invoke();
            }
            catch (Exception e) 
            {
                _messageBoxService.ShowMessage(e.Message);
            }
        }

        private readonly AuthenticationService _authenticationService;
        private readonly AccountStore _accountStore;
        public event Action AuthenticationDone;
        private readonly IMessageBoxService _messageBoxService;

        public LoginViewModel(AuthenticationService authenticationUseCase,
            AccountStore accountStore,
            IMessageBoxService messageBoxService)
        {
            _authenticationService = authenticationUseCase;
            _accountStore = accountStore;
            _messageBoxService = messageBoxService;

            LoginCommand = new RelayCommand(Authenticate);
        }

    }
}
