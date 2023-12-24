using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;

namespace UI.ViewModel.Books.EditViewModels
{
    internal class RepairTypeEditViewModel : ViewModelBase
    {
        private readonly IRepairTypeRepository _repairTypeRepository;
        private readonly IMessageBoxService _messageBoxService;
        private int _id;
        private string _name;

        public Action<RepairTypeEditViewModel> RemoveEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public RepairTypeEditViewModel(int id, string name, IMessageBoxService messageBoxService, IRepairTypeRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);
            _id = id;
            _name = name;
            _messageBoxService = messageBoxService;
            _repairTypeRepository = brandRepository;
        }
        public RepairTypeEditViewModel(IMessageBoxService messageBoxService, IRepairTypeRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);

            _id = 0;
            _name = "";
            _messageBoxService = messageBoxService;
            _repairTypeRepository = brandRepository;
        }
        private RepairTypeEditViewModel()
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
            RepairType type = new RepairType()
            {
                Id = _id,
                Name = _name,
            };

            try
            {
                if (Id == 0)
                {
                    _repairTypeRepository.Create(type);
                }
                else
                {
                    _repairTypeRepository.Update(Id, type);
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
                _repairTypeRepository.Remove(Id);
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
