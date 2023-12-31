﻿using Domain.Models;
using System;

namespace UI.ViewModel.HelperViewModels
{
    internal class FreighterViewModel : ViewModelBase
    {
        private int _id;
        private string _name;

        public FreighterViewModel(Freighter freighter)
        {
            ArgumentNullException.ThrowIfNull(freighter);
            Id = freighter.Id;
            Name = freighter.Name;
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
