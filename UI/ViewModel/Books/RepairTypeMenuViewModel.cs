using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using System.Collections.ObjectModel;
using UI.ViewModel.Books.BookEditViewModels;

namespace UI.ViewModel
{
    internal class RepairTypeMenuViewModel : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IRepairTypeRepository _repairTypeRepository;

        private ObservableCollection<RepairTypeEditViewModel> _items;
        private RepairTypeEditViewModel _selectedItem;

        public ICommand AddCommand { get; }

        public RepairTypeMenuViewModel(IMessageBoxService messageBoxService, IRepairTypeRepository repairType)
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(repairType);

            _messageBoxService = messageBoxService;
            _repairTypeRepository = repairType;

            Items = new ObservableCollection<RepairTypeEditViewModel>();
            foreach (RepairType item in _repairTypeRepository.GetAll())
            {
                RepairTypeEditViewModel vm = new RepairTypeEditViewModel(item.Id, item.Name, _messageBoxService, _repairTypeRepository);
                vm.RemoveEvent += OnRemove;
                Items.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void OnRemove(RepairTypeEditViewModel model)
        {
            model.RemoveEvent -= OnRemove;
            Items.Remove(model);
        }

        private void Add()
        {
            RepairTypeEditViewModel vm = new RepairTypeEditViewModel(_messageBoxService, _repairTypeRepository);
            vm.RemoveEvent += OnRemove;
            Items.Add(vm);
            SelectedItem = vm;
        }

        public bool IsRedactingEnabled => SelectedItem != null;

        public ObservableCollection<RepairTypeEditViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public RepairTypeEditViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }
    }
}
