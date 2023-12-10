using Domain.Services;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    public class UpdatePasswordViewModel : ViewModelBase
    {
        private string _oldPassword;
        private string _newPassword;
        private string _confirmNewPassword;

        public string Username
        {
            get => _accountStore.CurrentAccount.Username;
            set { _accountStore.CurrentAccount.Username = value; NotifyPropertyChanged(nameof(Username)); }
        }

        public string OldPassword
        {
            get => _oldPassword; 
            set { _oldPassword = value; NotifyPropertyChanged(nameof(OldPassword)); }
        }

        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; NotifyPropertyChanged(nameof(NewPassword)); }
        }

        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set { _confirmNewPassword = value; NotifyPropertyChanged(nameof(ConfirmNewPassword)); }
        }


        private readonly RegistrationService _registrationUseCase;
        private readonly NavigationService _toShell;
        private readonly AccountStore _accountStore;

        public ICommand UpdatePasswordCommand
        {
            get => new RelayCommand(UpdatePassword);
        }

        public NavigateCommand BackToShellCommand
        {
            get => new NavigateCommand(_toShell);
        }

        public UpdatePasswordViewModel(RegistrationService registrationUseCase, NavigationService toShell, AccountStore accountStore)
        {
            _registrationUseCase = registrationUseCase;
            _toShell = toShell;
            _accountStore = accountStore;
        }

        private void UpdatePassword()
        {
            if(_registrationUseCase.UpdatePassword(_accountStore.CurrentAccount, OldPassword, NewPassword))
                _toShell.Navigate();
        }
    }
}