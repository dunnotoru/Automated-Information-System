using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using UI.ViewModel.Books.EditViewModels;

namespace UI.ViewModel
{
    internal class FreighterMenuViewModel : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IFreighterRepository _freighterRepository;

        private ObservableCollection<FreighterEditViewModel> _items;
        private FreighterEditViewModel _selectedItem;

        public ICommand AddCommand { get; }

        public FreighterMenuViewModel(IMessageBoxService messageBoxService, IFreighterRepository freighterRepository)
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(freighterRepository);

            _messageBoxService = messageBoxService;
            _freighterRepository = freighterRepository;

            Items = new ObservableCollection<FreighterEditViewModel>();
            foreach (Freighter item in _freighterRepository.GetAll())
            {
                FreighterEditViewModel vm = new FreighterEditViewModel(item, _freighterRepository);
                vm.Save += OnSave;
                vm.Error += OnError;
                vm.Remove += OnRemove;
                Items.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void OnRemove(object? sender, EventArgs e)
        {
            FreighterEditViewModel vm = (FreighterEditViewModel)sender;
            vm.Save -= OnSave;
            vm.Error -= OnError;
            vm.Remove -= OnRemove;
            Items.Remove(vm);
            _messageBoxService.ShowMessage("Данные успешно удалены");
        }

        private void OnError(object? sender, Exception e)
        {
            _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
        }

        private void OnSave(object? sender, EventArgs e)
        {
            FreighterEditViewModel vm = (FreighterEditViewModel)sender;
            vm.Save -= OnSave;
            vm.Error -= OnError;
            vm.Remove -= OnRemove;

            Freighter freighter = _freighterRepository.GetById(vm.Id);
            FreighterEditViewModel updatedVm = new FreighterEditViewModel(freighter, _freighterRepository);

            updatedVm.Save += OnSave;
            updatedVm.Error += OnError;
            updatedVm.Remove += OnRemove;

            int index = Items.IndexOf(vm);
            Items.Insert(index, updatedVm);
            Items.Remove(vm);

            _messageBoxService.ShowMessage("Данные успешно сохранены.");
        }

        private void Add()
        {
            FreighterEditViewModel vm = new FreighterEditViewModel(_freighterRepository);
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;

            Items.Add(vm);
            SelectedItem = vm;
        }

        public bool IsRedactingEnabled => SelectedItem != null;

        public ObservableCollection<FreighterEditViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public FreighterEditViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }
    }
}
