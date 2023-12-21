using Domain.Services;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    internal class RegistrationViewModel : ViewModelBase
    {
		private readonly IMessageBoxService _messageBoxService;
		private readonly RegistrationService _registrationService;

		private string _username;
		private string _password;
		private string _passwordConfirmation;
		private bool _read;
		private bool _write;
		private bool _edit;
		private bool _delete;

		public ICommand RegisterCommand { get; }
		public ICommand DenyCommand { get; }

        public RegistrationViewModel(RegistrationService registrationService, IMessageBoxService messageBoxService)
        {
            _registrationService = registrationService;
            _messageBoxService = messageBoxService;
            RegisterCommand = new RelayCommand(Register);
            DenyCommand = new RelayCommand(Deny);
        }

        private void Register()
		{
			bool result = _registrationService.Register(Username,Password,
				Read, Write, Edit, Delete);
			if (result == true)
				_messageBoxService.ShowMessage("Регистрация прошла успешно");
			else 
				_messageBoxService.ShowMessage("Произошла неизвестная ошибка");
		}

		private void Deny()
		{
			Username = "";
			Password = "";
			PasswordConfirmation = "";
			Read = false;
			Write = false; 
			Edit = false; 
			Delete = false;
		}

        public string Username
		{
			get { return _username; }
			set { _username = value; OnPropertyChanged(); }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; OnPropertyChanged(); }
		}

		public string PasswordConfirmation
		{
			get { return _passwordConfirmation; }
			set { _passwordConfirmation = value; OnPropertyChanged(); }
		}

		public bool Read
		{
			get { return _read; }
			set { _read = value; OnPropertyChanged(); }
		}

		public bool Write
		{
			get { return _write; }
			set { _write = value; OnPropertyChanged(); }
		}

		public bool Edit
		{
			get { return _edit; }
			set { _edit = value; OnPropertyChanged(); }
		}

		public bool Delete
		{
			get { return _delete; }
			set { _delete = value; OnPropertyChanged(); }
		}
	}
}
