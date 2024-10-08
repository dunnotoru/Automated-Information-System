using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class DriverEditViewModel : EditViewModel
{
    private string _payrollNumber = string.Empty;
    private string _name = string.Empty;
    private string _surname = string.Empty;
    private string _patronymic = string.Empty;
    private DateTime _birthDate = DateTime.Now;
    private string _gender = string.Empty;
    private DriverLicenseViewModel? _license = null;
    private string _driverClass = string.Empty;
    private string _professionalStandard = string.Empty;
    private string _employmentBookDetails = string.Empty;

    public DriverEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public DriverEditViewModel(Driver driver, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = driver.Id;
        _payrollNumber = driver.PayrollNumber;
        _name = driver.Name;
        _surname = driver.Surname;
        _patronymic = driver.Patronymic;
        _payrollNumber = driver.PayrollNumber;
        _birthDate = driver.BirthDate;
        _gender = driver.Gender;
        _driverClass = driver.DriverClass;
        _professionalStandard = driver.ProfessionalStandardDetails;
        _employmentBookDetails = driver.EmploymentBookDetails;
        _license = null;
    }


    public override ICommand SaveCommand { get; }
    public override ICommand RemoveCommand { get; }

    protected override bool CanSave() =>
        !string.IsNullOrWhiteSpace(PayrollNumber) &&
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

    public DriverLicenseViewModel? License
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