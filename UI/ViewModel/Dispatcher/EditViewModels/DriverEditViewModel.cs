using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    class DriverEditViewModel : ViewModelBase
    {
        private readonly IDriverRepository _driverRepository;

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

        public DriverEditViewModel(Driver driver, IDriverRepository driverRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(driver);
            ArgumentNullException.ThrowIfNull(driverRepository);

            _driverRepository = driverRepository;

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
            License = new DriverLicenseViewModel(driver.License);
        }

        public DriverEditViewModel(IDriverRepository driverRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(driverRepository);

            _driverRepository = driverRepository;

            Id = 0;
            PayrollNumber = "";
            Name = "";
            Surname = "";
            Patronymic = "";
            PayrollNumber = "";
            BirthDate = DateTime.MinValue;
            Gender = "";
            DriverClass = "";
            ProfessionalStandardDetails = "";
            EmploymentBookDetails = "";
            License = new DriverLicenseViewModel();
        }

        public DriverEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            RemoveCommand = new RelayCommand(Remove);
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
                License = License.GetLicense(),
            };

            try
            {
                if (Id == 0)
                {
                    _driverRepository.Add(driver);
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
