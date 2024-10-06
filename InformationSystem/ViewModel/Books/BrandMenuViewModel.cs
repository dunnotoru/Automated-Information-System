using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Books.EditViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Books;

internal class BrandMenuViewModel : ViewModelBase
{
	private readonly IMessageBoxService _messageBoxService;
	private readonly IDbContextFactory<DomainContext> _contextFactory;

	private ObservableCollection<BrandEditViewModel> _items;
	private BrandEditViewModel _selectedItem;

	public ICommand AddCommand { get; }

	public BrandMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory)
	{
		ArgumentNullException.ThrowIfNull(messageBoxService);

		_messageBoxService = messageBoxService;
		_contextFactory = contextFactory;

		Items = new ObservableCollection<BrandEditViewModel>();
		
		using (DomainContext context = _contextFactory.CreateDbContext())
		{
			foreach (Brand item in context.Brands)
			{
				BrandEditViewModel vm = new BrandEditViewModel(item, _contextFactory);
				vm.Save += OnSave;
				vm.Error += OnError;
				vm.Remove += OnRemove;
				Items.Add(vm);
			}
		}

		AddCommand = new RelayCommand(Add);
	}

	private void OnRemove(object? sender, EventArgs e)
	{
		BrandEditViewModel vm = (BrandEditViewModel)sender!;
		vm.Save -= OnSave;
		vm.Error -= OnError;
		vm.Remove -= OnRemove;
		Items.Remove(vm);

		_messageBoxService.ShowMessage("Данные успешно удалены.");
	}

	private void OnSave(object? sender, EventArgs e)
	{
		BrandEditViewModel vm = (BrandEditViewModel)sender!;
		vm.Save -= OnSave;
		vm.Error -= OnError;
		vm.Remove -= OnRemove;

		Brand brand = _brandRepository.GetById(vm.Id);
		BrandEditViewModel updatedVm = new BrandEditViewModel(brand, _brandRepository);
		
		using (DomainContext context = _contextFactory.CreateDbContext())
		{
			Brand brand = context.Brands.First(o => o.Id == id);
		}

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
		set
		{
			_selectedItem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled));
		}
	}
}