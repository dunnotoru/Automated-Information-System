using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    class DriverLicenseViewModel : ViewModelBase
    {
        private int _id;
        private string _licenseNumber;
        private DateTime _dateOfIssue;
        private DateTime _dateOfExpiration;
        private ObservableCollection<CategoryItemViewModel> _categories;

        public DriverLicenseViewModel(DriverLicense license)
        {
            ArgumentNullException.ThrowIfNull(license);

            Id = license.Id;
            LicenseNumber = license.LicenseNumber;
            DateOfIssue = license.DateOfIssue;
            DateOfExpiration = license.DateOfExpiration;
            Categories = new ObservableCollection<CategoryItemViewModel>();
            foreach (Category item in license.Categories)
                Categories.Add(new CategoryItemViewModel(item));
        }

        public DriverLicenseViewModel()
        {
            Id = 0;
            LicenseNumber = "";
            DateOfIssue = DateTime.Now;
            DateOfExpiration = DateTime.Now;
            Categories = new ObservableCollection<CategoryItemViewModel>();
        }

        public DriverLicense GetLicense()
        {
            List<Category> categories = new List<Category>();
            foreach (CategoryItemViewModel item in Categories)
                categories.Add(item.Category);
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

        public ObservableCollection<CategoryItemViewModel> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }

    }
}
