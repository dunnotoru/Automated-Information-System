﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    internal class MenuItemViewModel : ViewModelBase
    {
        private string _name;
        private Func<ViewModelBase> _getViewModel;
        
        public event EventHandler<Func<ViewModelBase>> ViewModelChanged;
        
        public bool isReadRequired { get; }
        public bool isWriteRequired { get; }
        public bool isDeleteRequired { get; }
        public bool isEditRequired { get; }

        public bool Visible { get => !isReadRequired; }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }

        public MenuItemViewModel(string name, IEnumerable<MenuItemViewModel> subItems)
        {
            ArgumentNullException.ThrowIfNull(subItems);
            Items = new ObservableCollection<MenuItemViewModel>(subItems);

            Name = name;
            isReadRequired = false;
            isWriteRequired = false;
            isEditRequired = false;
            isDeleteRequired = false;
        }

        public MenuItemViewModel(string name, Func<ViewModelBase> getViewModel)
        {
            Items = new ObservableCollection<MenuItemViewModel>();
            _getViewModel = getViewModel;
            Name = name;

            isReadRequired = false;
            isWriteRequired = false;
            isEditRequired = false;
            isDeleteRequired = false;
        }

        public ICommand MenuItemCommand
        {
            get => new RelayCommand(() => ViewModelChanged?.Invoke(this, _getViewModel));
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
    }
}
