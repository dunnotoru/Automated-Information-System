using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;

namespace UI.ViewModel.Books.EditViewModels
{
    internal class CategoryEditViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private string _name;

        public Action<CategoryEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public CategoryEditViewModel(Category category, ICategoryRepository categoryRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(category);
            ArgumentNullException.ThrowIfNull(categoryRepository);

            Id = category.Id;
            Name = category.Name;

            _categoryRepository = categoryRepository;
        }

        public CategoryEditViewModel(ICategoryRepository categoryRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(categoryRepository);

            Id = 0;
            Name = "";
            _categoryRepository = categoryRepository;
        }

        private CategoryEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, () => CanSave());
            RemoveCommand = new RelayCommand(Remove);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public void Save()
        {
            Category createdStation = new Category()
            {
                Name = Name,
            };
            try
            {
                if (Id == 0)
                {
                    _categoryRepository.Create(createdStation);
                }
                else
                {
                    _categoryRepository.Update(Id, createdStation);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        public void Remove()
        {
            if (Id == 0) return;
            try
            {
                _categoryRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }
    }
}
