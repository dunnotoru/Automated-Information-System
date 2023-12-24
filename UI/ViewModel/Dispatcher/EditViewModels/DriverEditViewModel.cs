using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    class DriverEditViewModel : ViewModelBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ICategoryRepository _categoryRepository;

        private int _id;
        private string _payrollNumber;
        private string _name;
        private string _surname;
        private string _patronymic;
        private DateTime _birthDate;
        private string _gender;
        private DriverLicenseViewModel _license;
        private string _driverClass;
        private string _professionalStandard;
        private string _employmentBookDetails;

        public Action<DriverEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public DriverEditViewModel(Driver driver, IDriverRepository driverRepository, 
            ICategoryRepository categoryRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(driver);
            ArgumentNullException.ThrowIfNull(driverRepository);

            _driverRepository = driverRepository;
            _categoryRepository = categoryRepository;

            Id = driver.Id;
            PayrollNumber = driver.PayrollNumber ?? "";
            Name = driver.Name ?? "";
            Surname = driver.Surname ?? "";
            Patronymic = driver.Patronymic ?? "";
            PayrollNumber = driver.PayrollNumber ?? "";
            BirthDate = driver.BirthDate;
            Gender = driver.Gender ?? "";
            DriverClass = driver.DriverClass ?? "";
            ProfessionalStandardDetails = driver.ProfessionalStandardDetails ?? "";
            EmploymentBookDetails = driver.EmploymentBookDetails ?? "";
            License = new DriverLicenseViewModel(driver.DriverLicense, _categoryRepository);
        }

        public DriverEditViewModel(IDriverRepository driverRepository, ICategoryRepository categoryRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(driverRepository);

            _driverRepository = driverRepository;
            _categoryRepository = categoryRepository;

            Id = 0;
            PayrollNumber = "";
            Name = "";
            Surname = "";
            Patronymic = "";
            PayrollNumber = "";
            BirthDate = DateTime.Now;
            Gender = "";
            DriverClass = "";
            ProfessionalStandardDetails = "";
            EmploymentBookDetails = "";
            License = new DriverLicenseViewModel(_categoryRepository);
        }

        public DriverEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, () => CanSave());
            RemoveCommand = new RelayCommand(Remove);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(PayrollNumber) &&
                !string.IsNullOrWhiteSpace(Name) &&
                !string.IsNullOrWhiteSpace(Surname) &&
                !string.IsNullOrWhiteSpace(Patronymic) &&
                !string.IsNullOrWhiteSpace(Gender) &&
                !string.IsNullOrWhiteSpace(DriverClass) &&
                !string.IsNullOrWhiteSpace(ProfessionalStandardDetails) &&
                !string.IsNullOrWhiteSpace(EmploymentBookDetails) &&
                License != null &&
                License.DateOfIssue.Year - BirthDate.Year > 16 &&
                License.DateOfExpiration > License.DateOfIssue &&
                License.Categories != null &&
                !string.IsNullOrWhiteSpace(License.LicenseNumber);
        }

        private void Save()
        {
            Driver driver = new Driver()
            {
                PayrollNumber = PayrollNumber,
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic,
                Gender = Gender,
                BirthDate = BirthDate,
                DriverClass = DriverClass,
                EmploymentBookDetails = EmploymentBookDetails,
                ProfessionalStandardDetails = ProfessionalStandardDetails,
                DriverLicense = License.GetLicense(),
            };

            try
            {
                if (Id == 0)
                {
                    _driverRepository.Create(driver);
                }
                else
                {
                    _driverRepository.Update(Id, driver);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        private void Remove()
        {
            if (Id == 0) return;
            try
            {
                _driverRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string PayrollNumber
        {
            get { return _payrollNumber; }
            set { _payrollNumber = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(); }
        }

        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; OnPropertyChanged(); }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }

        public DriverLicenseViewModel License
        {
            get { return _license; }
            set { _license = value; OnPropertyChanged(); }
        }

        public string DriverClass
        {
            get { return _driverClass; }
            set { _driverClass = value; OnPropertyChanged(); }
        }

        public string ProfessionalStandardDetails
        {
            get { return _professionalStandard; }
            set { _professionalStandard = value; OnPropertyChanged(); }
        }

        public string EmploymentBookDetails
        {
            get { return _employmentBookDetails; }
            set { _employmentBookDetails = value; OnPropertyChanged(); }
        }
    }
}
