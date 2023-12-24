using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel.Books.EditViewModels
{
    internal class BrandEditViewModel : ViewModelBase
    {
        private readonly IBrandRepository _brandRepository;
		private readonly IMessageBoxService _messageBoxService;
		private int _id;
		private string _name;

		public Action<BrandEditViewModel> RemoveEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public BrandEditViewModel(int id, string name, IMessageBoxService messageBoxService, IBrandRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);
            _id = id;
            _name = name;
            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
        }
        public BrandEditViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);

            _id = 0;
            _name = "";
            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
        }
        private BrandEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, CanSave);
            RemoveCommand = new RelayCommand(Remove);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        private void Save()
        {
            Brand brand = new Brand()
            {
                Id = _id,
                Name = _name,
            };

            try
            {
                if (Id == 0)
                {
                    _brandRepository.Create(brand);
                }
                else
                {
                    _brandRepository.Update(Id, brand);
                }
                _messageBoxService.ShowMessage("Данные успешно сохранены.");
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }

        }
        private void Remove()
        {
            if (Id == 0) return;

            try
            {
                _brandRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
                _messageBoxService.ShowMessage("Данные успешно удалены.");
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }
        }

        public int Id
		{
			get { return _id; }
			set { _id = value; OnPropertyChanged(); }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}
	}
}
