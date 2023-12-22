using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Books.BookEditViewModels;

namespace UI.ViewModel
{
    internal class BrandBookViewModel : ViewModelBase
    {
		private readonly IMessageBoxService _messageBoxService;
		private readonly IBrandRepository _brandRepository;

		private ObservableCollection<BrandEditViewModel> _items;
		private BrandEditViewModel _selectedItem;

		public ICommand AddCommand { get; }

        public BrandBookViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository)
        {
			ArgumentNullException.ThrowIfNull(messageBoxService);
			ArgumentNullException.ThrowIfNull(brandRepository);

            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
			
			Items = new ObservableCollection<BrandEditViewModel>();
			foreach (Brand item in _brandRepository.GetAll())
			{
				BrandEditViewModel vm = new BrandEditViewModel(item.Id, item.Name, _messageBoxService, _brandRepository);
				vm.RemoveEvent += OnRemove;
				Items.Add(vm);
			}

            AddCommand = new RelayCommand(Add);
        }

        private void OnRemove(BrandEditViewModel model)
        {
			model.RemoveEvent -= OnRemove;
			Items.Remove(model);
        }

        private void Add()
        {
            BrandEditViewModel vm = new BrandEditViewModel(_messageBoxService, _brandRepository);
			vm.RemoveEvent += OnRemove;
			Items.Add(vm);
			SelectedItem = vm;
        }

		public bool IsRedactingEnabled => SelectedItem != null;

        public ObservableCollection<BrandEditViewModel> Items
		{
			get { return _items; }
			set { _items = value; OnPropertyChanged(); }
		}
		
		public BrandEditViewModel SelectedItem
		{
			get { return _selectedItem; }
			set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
		}
	}
}
