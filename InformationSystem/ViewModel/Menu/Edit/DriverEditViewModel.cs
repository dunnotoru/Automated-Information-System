using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
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
    private DriverLicenseViewModel _license;
    private string _driverClass = string.Empty;
    private string _professionalStandard = string.Empty;
    private string _employmentBookDetails = string.Empty;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Driver
        {
            Id = this.Id,
            Name = _name,
            Surname = _surname,
            Patronymic = _patronymic,
            BirthDate = _birthDate,
            Gender = _gender,
            DriverLicenseId = _license.Id,
            DriverClass = _driverClass,
            ProfessionalStandardDetails = _professionalStandard,
            EmploymentBookDetails = _employmentBookDetails
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Freighter>);

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
        
        DomainContext context = contextFactory.CreateDbContext();
        _license = new DriverLicenseViewModel(
            context.Licenses.First(l => l.Id == driver.DriverLicenseId),
            contextFactory
            );
    }

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
        get => _surname;
        set => SetProperty(ref _surname, value);
    }

    public string Patronymic
    {
        get => _patronymic;
        set => SetProperty(ref _patronymic, value);
    }
    
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public string PayrollNumber
    {
        get => _payrollNumber;
        set => SetProperty(ref _payrollNumber, value);
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set => SetProperty(ref _birthDate, value);
    }

    public string Gender
    {
        get => _gender;
        set => SetProperty(ref _gender, value);
    }

    public DriverLicenseViewModel License
    {
        get => _license;
        set => SetProperty(ref _license, value);
    }

    public string DriverClass
    {
        get => _driverClass;
        set => SetProperty(ref _driverClass, value);
    }

    public string ProfessionalStandardDetails
    {
        get => _professionalStandard;
        set => SetProperty(ref _professionalStandard, value);
    }

    public string EmploymentBookDetails
    {
        get => _employmentBookDetails;
        set => SetProperty(ref _employmentBookDetails, value);
    }
}