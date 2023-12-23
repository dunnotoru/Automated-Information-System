using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using UI.ViewModel.Books.BookEditViewModels;

namespace UI.ViewModel
{
    internal class FreighterBookViewModel : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IFreighterRepository _freighterRepository;

        private ObservableCollection<FreighterEditViewModel> _items;
        private FreighterEditViewModel _selectedItem;

        public ICommand AddCommand { get; }

        public FreighterBookViewModel(IMessageBoxService messageBoxService, IFreighterRepository freighterRepository)
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(freighterRepository);

            _messageBoxService = messageBoxService;
            _freighterRepository = freighterRepository;

            Items = new ObservableCollection<FreighterEditViewModel>();
            foreach (Freighter item in _freighterRepository.GetAll())
            {
                FreighterEditViewModel vm = new FreighterEditViewModel(item.Id, item.Name, _messageBoxService, _freighterRepository);
                vm.RemoveEvent += OnRemove;
                Items.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void OnRemove(FreighterEditViewModel model)
        {
            model.RemoveEvent -= OnRemove;
            Items.Remove(model);
        }

        private void Add()
        {
            FreighterEditViewModel vm = new FreighterEditViewModel(_messageBoxService, _freighterRepository);
            vm.RemoveEvent += OnRemove;
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
