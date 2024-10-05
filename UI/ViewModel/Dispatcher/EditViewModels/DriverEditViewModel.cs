using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Dispatcher.EditViewModels;

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

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

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

    private DriverEditViewModel()
    {
        SaveCommand = new RelayCommand(ExecuteSave, () => CanSave());
        RemoveCommand = new RelayCommand(ExecuteRemove);
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
               License.Categories.Count > 0 &&
               !string.IsNullOrWhiteSpace(License.LicenseNumber);
    }

    private void ExecuteSave()
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
                Id = _driverRepository.Create(driver);
            }
            else
            {
                _driverRepository.Update(Id, driver);
            }
            Save?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    private void ExecuteRemove()
    {
        if (Id == 0) return;
        try
        {
            _driverRepository.Remove(Id);
            Remove?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }

    public string PayrollNumber
    {
        get { return _payrollNumber; }
        set { _payrollNumber = value; NotifyPropertyChanged(); }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }

    public string Surname
    {
        get { return _surname; }
        set { _surname = value; NotifyPropertyChanged(); }
    }

    public string Patronymic
    {
        get { return _patronymic; }
        set { _patronymic = value; NotifyPropertyChanged(); }
    }

    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; NotifyPropertyChanged(); }
    }

    public string Gender
    {
        get { return _gender; }
        set { _gender = value; NotifyPropertyChanged(); }
    }

    public DriverLicenseViewModel License
    {
        get { return _license; }
        set { _license = value; NotifyPropertyChanged(); }
    }

    public string DriverClass
    {
        get { return _driverClass; }
        set { _driverClass = value; NotifyPropertyChanged(); }
    }

    public string ProfessionalStandardDetails
    {
        get { return _professionalStandard; }
        set { _professionalStandard = value; NotifyPropertyChanged(); }
    }

    public string EmploymentBookDetails
    {
        get { return _employmentBookDetails; }
        set { _employmentBookDetails = value; NotifyPropertyChanged(); }
    }
}