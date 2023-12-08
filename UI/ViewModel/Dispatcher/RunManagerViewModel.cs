﻿using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class RunManagerViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;

        private int _id;
        private string _name;
        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;  
        private Driver _driver;
        private Vehicle _vehicle;

        private Run _selectedRun;
        private Route _selectedRoute;
        private State _currentState;

        public ObservableCollection<Run> Runs { get; set; }
        public ObservableCollection<Route> Routes { get; set; }

        public Run SelectedRun
        {
            get => _selectedRun;
            set { _selectedRun = value; NotifyPropertyChangedByCallerName(); }
        }
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set { _selectedRoute = value; NotifyPropertyChangedByCallerName(); }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                NotifyPropertyChanged(nameof(IsRedactingEnabled));
            }
        }

        public bool IsRedactingEnabled => CurrentState != State.None;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public RunManagerViewModel(IRunRepository runRepository, IRouteRepository routeRepository)
        {
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(routeRepository);

            _runRepository = runRepository;
            _routeRepository = routeRepository;

            Runs = new ObservableCollection<Run>(_runRepository.GetAll());
            Routes = new ObservableCollection<Route>(_routeRepository.GetAll());

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedRoute != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedRoute != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedRun = new Run();
        }

        private void Delete()
        {
           Runs.Remove(SelectedRun);
            _runRepository.Remove(SelectedRun);
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            if (CurrentState == State.Add)
            {
                _runRepository.Add(SelectedRun);
            }
            else if (CurrentState == State.Edit)
            {
                _runRepository.Update(SelectedRun);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}