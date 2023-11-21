using Domain.UseCases.AccountUseCases;
using System;
using UI.Command;

namespace UI.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get => _loginCommand ?? new RelayCommand(Authenticate);
        }

        public void Authenticate()
        {
            bool result = true;// _authenticationUseCase.Authenticate(Username, Password);
            if (result)
                AuthenticationDone?.Invoke();
        }

        private readonly AuthenticationUseCase _authenticationUseCase;
        public event Action AuthenticationDone;

        public LoginViewModel(AuthenticationUseCase authenticationUseCase)
        {
            _authenticationUseCase = authenticationUseCase;
        }

    }
}
