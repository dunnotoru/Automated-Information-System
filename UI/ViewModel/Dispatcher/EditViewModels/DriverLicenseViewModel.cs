using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    class DriverLicenseViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private int _id;
        private string _licenseNumber;
        private DateTime _dateOfIssue;
        private DateTime _dateOfExpiration;
        private ObservableCollection<CategoryViewModel> _categories;

        public DriverLicenseViewModel(DriverLicense license, ICategoryRepository categoryRepository)
        {
            ArgumentNullException.ThrowIfNull(license);

            _categoryRepository = categoryRepository;
            Id = license.Id;
            LicenseNumber = license.LicenseNumber;
            DateOfIssue = license.DateOfIssue;
            DateOfExpiration = license.DateOfExpiration;
            Categories = new ObservableCollection<CategoryViewModel>();
            foreach (Category item in _categoryRepository.GetAll())
            {
                CategoryViewModel vm = new CategoryViewModel(item, _categoryRepository);
                if (license.Categories.Any(o => item.Id == o.Id))
                    vm.IsSelected = true;
                Categories.Add(vm);
            }
        }

        public DriverLicenseViewModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            Id = 0;
            LicenseNumber = "";
            DateOfIssue = DateTime.Now;
            DateOfExpiration = DateTime.Now;
            Categories = new ObservableCollection<CategoryViewModel>();
            foreach (Category item in _categoryRepository.GetAll())
            {
                CategoryViewModel vm = new CategoryViewModel(item, _categoryRepository);
                Categories.Add(vm);
            }
        }

        public DriverLicense GetLicense()
        {
            List<Category> categories = new List<Category>();
            foreach (CategoryViewModel item in Categories)
            {
                if (item.IsSelected == true)
                    categories.Add(item.GetCategory());
            }

            return new DriverLicense()
            {
                Id = Id,
                DateOfIssue = DateOfIssue,
                DateOfExpiration = DateOfExpiration,
                LicenseNumber = LicenseNumber,
                Categories = categories
            };
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { _licenseNumber = value; OnPropertyChanged(); }
        }

        public DateTime DateOfIssue
        {
            get { return _dateOfIssue; }
            set { _dateOfIssue = value; OnPropertyChanged(); }
        }

        public DateTime DateOfExpiration
        {
            get { return _dateOfExpiration; }
            set { _dateOfExpiration = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }
    }
}
