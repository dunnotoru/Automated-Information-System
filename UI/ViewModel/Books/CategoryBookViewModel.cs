using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class CategoryBookViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMessageBoxService _messageBoxService;

        private ObservableCollection<CategoryEditViewModel> _items;
        private CategoryEditViewModel _selectedItem;

        public ObservableCollection<CategoryEditViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public CategoryEditViewModel SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedItem != null;

        public ICommand AddCommand { get; }

        public CategoryBookViewModel(ICategoryRepository categoryRepository,
            IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(categoryRepository);
            _categoryRepository = categoryRepository;
            _messageBoxService = messageBoxService;

            Items = new ObservableCollection<CategoryEditViewModel>();
            IEnumerable<Category> categories = _categoryRepository.GetAll();
            foreach (Category item in categories)
            {
                CategoryEditViewModel vm = new CategoryEditViewModel(item, _categoryRepository);
                vm.RemoveEvent += OnRemove;
                vm.ErrorEvent += OnError;
                Items.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            CategoryEditViewModel vm = new CategoryEditViewModel(_categoryRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Items.Add(vm);
            SelectedItem = vm;
        }

        private void OnRemove(CategoryEditViewModel viewModel)
        {
            viewModel.RemoveEvent -= OnRemove;
            viewModel.ErrorEvent -= OnError;
            if (Items.Remove(viewModel))
            {
                _messageBoxService.ShowMessage("Станция удалена");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
