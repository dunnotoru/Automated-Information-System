using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.Services;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.Stores;
using UI.ViewModel.Settings.EditViewModels;

namespace UI.ViewModel.Settings;

internal class AccountMenuViewModel : ViewModelBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly RegistrationService _registrationService;
    private readonly IAccountRepository _accountRepository;
    private readonly AccountStore _accountStore;

    private ObservableCollection<AccountEditViewModel> _items;
    private AccountEditViewModel _selectedItem;

    public ICommand AddCommand { get; }

    public AccountMenuViewModel(IMessageBoxService messageBoxService, IAccountRepository accountRepository,
        RegistrationService registrationService, AccountStore accountStore)
    {
        ArgumentNullException.ThrowIfNull(messageBoxService);

        _messageBoxService = messageBoxService;
        _accountRepository = accountRepository;
        _registrationService = registrationService;
        _accountStore = accountStore;

        Items = new ObservableCollection<AccountEditViewModel>();
        foreach (Account item in _accountRepository.GetAll())
        {
            AccountEditViewModel vm = new AccountEditViewModel(item, _accountRepository, _registrationService, _accountStore);
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;
            Items.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        AccountEditViewModel vm = (AccountEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно удалены.");
    }

    private void OnSave(object? sender, EventArgs e)
    {
        AccountEditViewModel vm = (AccountEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        Account account = _accountRepository.GetById(vm.Id);
        AccountEditViewModel updatedVm = new AccountEditViewModel(account, _accountRepository, _registrationService, _accountStore);

        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;

        int index = Items.IndexOf(vm);
        Items.Insert(index, updatedVm);
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены.");
    }

    private void OnError(object? sender, Exception e)
    {
        AccountEditViewModel vm = (AccountEditViewModel)sender;

        Items.Remove(vm);

        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    private void Add()
    {
        AccountEditViewModel vm = new AccountEditViewModel(_accountRepository, _registrationService, _accountStore);
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;
        Items.Add(vm);
        SelectedItem = vm;
    }

    public bool IsRedactingEnabled => SelectedItem != null;

    public ObservableCollection<AccountEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; OnPropertyChanged(); }
    }

    public AccountEditViewModel SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value; OnPropertyChanged();
            OnPropertyChangedByName(nameof(IsRedactingEnabled));
        }
    }
}