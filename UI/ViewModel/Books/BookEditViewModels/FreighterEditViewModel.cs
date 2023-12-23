using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel.Books.BookEditViewModels
{
    internal class FreighterEditViewModel : ViewModelBase
    {
        private readonly IFreighterRepository _freighterRepository;
        private readonly IMessageBoxService _messageBoxService;
        private int _id;
        private string _name;

        public Action<FreighterEditViewModel> RemoveEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public FreighterEditViewModel(int id, string name, IMessageBoxService messageBoxService, IFreighterRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);
            _id = id;
            _name = name;
            _messageBoxService = messageBoxService;
            _freighterRepository = brandRepository;
        }
        public FreighterEditViewModel(IMessageBoxService messageBoxService, IFreighterRepository brandRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);

            _id = 0;
            _name = "";
            _messageBoxService = messageBoxService;
            _freighterRepository = brandRepository;
        }
        private FreighterEditViewModel()
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
            Freighter freighter = new Freighter()
            {
                Id = _id,
                Name = _name,
            };

            try
            {
                if (Id == 0)
                {
                    _freighterRepository.Create(freighter);
                }
                else
                {
                    _freighterRepository.Update(Id, freighter);
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
                _freighterRepository.Remove(Id);
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
