﻿using System;

namespace UI.ViewModel
{
    internal class PassengerViewModel : ViewModelBase
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
            set { _name = value; OnPropertyChanged(); }
        }
        public string Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(); }
        }
        public string Patronymic
        {
            get => _patronymic;
            set { _patronymic = value; OnPropertyChanged(); }
        }
        public string Series
        {
            get => _series;
            set { _series = value; OnPropertyChanged(); }
        }
        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); }
        }
        public DateOnly DateOfBirth
        {
            get => _dateOfBirth;
            set { _dateOfBirth = value; OnPropertyChanged(); }
        }
    }
}
