using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;
using Domain.Services;

namespace UI.ViewModel.Settings.EditViewModels
{
    internal class AccountEditViewModel : ViewModelBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly RegistrationService _registrationService;
        private int _id;
        private string _username;
        private string _password;
        private string _passwordConfirmation;
        private bool _read;
        private bool _write;
        private bool _edit;
        private bool _delete;
        private bool _isNew;

        public event EventHandler Save;
        public event EventHandler Remove;
        public event EventHandler<Exception> Error;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public AccountEditViewModel(Account account, IAccountRepository accountRepository, RegistrationService registrationService) : this(accountRepository, registrationService)
        {
            ArgumentNullException.ThrowIfNull(account);

            Id = account.Id;
            Username = account.Username;
            Read = account.Read;
            Write = account.Write;
            Edit = account.Edit;
            Delete = account.Delete;
            IsNew = false;
        }

        public AccountEditViewModel(IAccountRepository accountRepository, RegistrationService registrationService)
        {
            ArgumentNullException.ThrowIfNull(accountRepository);
            ArgumentNullException.ThrowIfNull(registrationService);
            
            _accountRepository = accountRepository;
            _registrationService = registrationService;

            Id = 0;
            Username = "";
            Read = false;
            Write = false;
            Edit = false;
            Delete = false;
            IsNew = true;

            SaveCommand = new RelayCommand(ExecuteSave, CanSave);
            RemoveCommand = new RelayCommand(ExecuteRemove);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Username);
        }

        private void ExecuteSave()
        {
            if (Password != PasswordConfirmation)
            {
                Error?.Invoke(this, new Exception("Ошибка подтверждения пароля."));
                return;
            }

            Account account = new Account()
            {
                Id = _id,
                Username = _username,
                Read = _read,
                Write = _write,
                Edit = _edit,
                Delete = _delete,
            };

            try
            {
                if (Id == 0)
                {
                    Id = _registrationService.Register(Username,
                                                  Password,
                                                  Read,
                                                  Write,
                                                  Edit,
                                                  Delete);
                }
                else
                {
                    _accountRepository.Update(Id, account);
                }
                Save?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }
        private void ExecuteRemove()
        {
            if (Id == 0) return;

            try
            {
                _accountRepository.Remove(Id);
                Remove?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; OnPropertyChanged(); }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
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
