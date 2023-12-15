using Domain.Services;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class UpdatePasswordViewModel : ViewModelBase
    {
        private string _oldPassword;
        private string _newPassword;
        private string _confirmNewPassword;

        public string Username
        {
            get => _accountStore.CurrentAccount.Username;
            set { _accountStore.CurrentAccount.Username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string OldPassword
        {
            get => _oldPassword; 
            set { _oldPassword = value; OnPropertyChanged(nameof(OldPassword)); }
        }

        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set { _confirmNewPassword = value; OnPropertyChanged(nameof(ConfirmNewPassword)); }
        }


        private readonly RegistrationService _registrationUseCase;
        private readonly NavigationService _navigationService;
        private readonly AccountStore _accountStore;

        public ICommand UpdatePasswordCommand
        {
            get => new RelayCommand(UpdatePassword);
        }

        public ICommand BackToShellCommand
        {
            get => new RelayCommand(() => _navigationService.Navigate<ShellViewModel>());
        }

        public UpdatePasswordViewModel(RegistrationService registrationUseCase,
            NavigationService navigationService, AccountStore accountStore)
        {
            _registrationUseCase = registrationUseCase;
            _navigationService = navigationService;
            _accountStore = accountStore;
        }

        private void UpdatePassword()
        {
            if(_registrationUseCase.UpdatePassword(_accountStore.CurrentAccount, OldPassword, NewPassword))
                _navigationService.Navigate<ShellViewModel>();
        }
    }
}