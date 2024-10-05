using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Books.EditViewModels;

namespace UI.ViewModel.Books;

internal class CategoryMenuViewModel : ViewModelBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBoxService _messageBoxService;

    private ObservableCollection<CategoryEditViewModel> _items;
    private CategoryEditViewModel _selectedItem;

    public ObservableCollection<CategoryEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; NotifyPropertyChanged(); }
    }

    public CategoryEditViewModel SelectedItem
    {
        get => _selectedItem;
        set { _selectedItem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }

    public bool IsRedactingEnabled => SelectedItem != null;

    public ICommand AddCommand { get; }

    public CategoryMenuViewModel(ICategoryRepository categoryRepository,
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
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;
            Items.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        CategoryEditViewModel vm = (CategoryEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;
        if (Items.Remove(vm))
        {
            _messageBoxService.ShowMessage("Данные успешно удалены.");
        }
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    private void OnSave(object? sender, EventArgs e)
    {
        CategoryEditViewModel vm = (CategoryEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        Category category = _categoryRepository.GetById(vm.Id);
        CategoryEditViewModel updatedVm = new CategoryEditViewModel(category, _categoryRepository);

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
        CategoryEditViewModel vm = new CategoryEditViewModel(_categoryRepository);
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;
        Items.Add(vm);
        SelectedItem = vm;
    }
}