using System;

namespace UI.ViewModel
{
    public class PassengerViewModel : ViewModelBase
    {
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _series;
        private string _number;
        private DateOnly _dateOfBirth;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                NotifyPropertyChanged(nameof(Surname));
            }
        }
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                NotifyPropertyChanged(nameof(Patronymic));
            }
        }
        public string Series
        {
            get => _series;
            set
            {
                _series = value;
                NotifyPropertyChanged(nameof(Series));
            }
        }
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                NotifyPropertyChanged(nameof(Number));
            }
        }
        public DateOnly DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                NotifyPropertyChanged(nameof(DateOfBirth));
            }
        }
    }
}
