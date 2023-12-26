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
            set { _accountStore.CurrentAccount.Username = value; OnPropertyChangedByName(nameof(Username)); }
        }

        public string OldPassword
        {
            get => _oldPassword;
            set { _oldPassword = value; OnPropertyChangedByName(nameof(OldPassword)); }
        }

        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChangedByName(nameof(NewPassword)); }
        }

        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set { _confirmNewPassword = value; OnPropertyChangedByName(nameof(ConfirmNewPassword)); }
        }


        private readonly RegistrationService _registrationUseCase;
        private readonly NavigationService _navigationService;
        private readonly AccountStore _accountStore;
        private readonly IMessageBoxService _messageBoxService;

        public ICommand UpdatePasswordCommand
        {
            get => new RelayCommand(UpdatePassword);
        }

        public ICommand BackToShellCommand
        {
            get => new RelayCommand(() => _navigationService.Navigate<ScheduleDataViewModel>());
        }

        public UpdatePasswordViewModel(RegistrationService registrationUseCase,
            NavigationService navigationService, AccountStore accountStore, IMessageBoxService messageBoxService)
        {
            _registrationUseCase = registrationUseCase;
            _navigationService = navigationService;
            _accountStore = accountStore;
            _messageBoxService = messageBoxService;
        }

        private void UpdatePassword()
        {
            if(NewPassword != ConfirmNewPassword)
            {
                _messageBoxService.ShowMessage("Пароли не совпадают.");
                return;
            }
                
            if (_registrationUseCase.UpdatePassword(_accountStore.CurrentAccount.Username, OldPassword, NewPassword))
            {
                _messageBoxService.ShowMessage("Пароль успешно изменён.");
                _navigationService.Navigate<ScheduleDataViewModel>();
            }
            else
            {
                _messageBoxService.ShowMessage("Произошла ошибка.");
                _navigationService.Navigate<ScheduleDataViewModel>();
            }
        }
    }
}