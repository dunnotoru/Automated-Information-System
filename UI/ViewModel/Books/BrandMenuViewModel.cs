using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services.Abstractions;
using UI.ViewModel.Books.EditViewModels;

namespace UI.ViewModel.Books;

internal class BrandMenuViewModel : ViewModelBase
{
	private readonly IMessageBoxService _messageBoxService;
	private readonly IBrandRepository _brandRepository;

	private ObservableCollection<BrandEditViewModel> _items;
	private BrandEditViewModel _selectedItem;

	public ICommand AddCommand { get; }

	public BrandMenuViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository)
	{
		ArgumentNullException.ThrowIfNull(messageBoxService);
		ArgumentNullException.ThrowIfNull(brandRepository);

		_messageBoxService = messageBoxService;
		_brandRepository = brandRepository;
			
		Items = new ObservableCollection<BrandEditViewModel>();
		foreach (Brand item in _brandRepository.GetAll())
		{
			BrandEditViewModel vm = new BrandEditViewModel(item, _brandRepository);
			vm.Save += OnSave;
			vm.Error += OnError;
			vm.Remove += OnRemove;
			Items.Add(vm);
		}

		AddCommand = new RelayCommand(Add);
	}

	private void OnRemove(object? sender, EventArgs e)
	{
		BrandEditViewModel vm = (BrandEditViewModel)sender;
		vm.Save -= OnSave;
		vm.Error -= OnError;
		vm.Remove -= OnRemove;
		Items.Remove(vm);

		_messageBoxService.ShowMessage("Данные успешно удалены.");
	}

	private void OnSave(object? sender, EventArgs e)
	{
		BrandEditViewModel vm = (BrandEditViewModel)sender;
		vm.Save -= OnSave;
		vm.Error -= OnError;
		vm.Remove -= OnRemove;

		Brand brand = _brandRepository.GetById(vm.Id);
		BrandEditViewModel updatedVm = new BrandEditViewModel(brand, _brandRepository);

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
		_messageBoxService.ShowMessage($"Ошибка: {e.Message}");
	}

	private void Add()
	{
		BrandEditViewModel vm = new BrandEditViewModel(_brandRepository);
		vm.Save += OnSave;
		vm.Error += OnError;
		vm.Remove += OnRemove;
		Items.Add(vm);
		SelectedItem = vm;
	}

	public bool IsRedactingEnabled => SelectedItem != null;

	public ObservableCollection<BrandEditViewModel> Items
	{
		get { return _items; }
		set { _items = value; NotifyPropertyChanged(); }
	}
		
	public BrandEditViewModel SelectedItem
	{
		get { return _selectedItem; }
		set { _selectedItem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
	}
}