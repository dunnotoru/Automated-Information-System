using System;
using System.Collections.ObjectModel;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class DriverEditViewModel : EditViewModel
{
    private readonly Driver _driver;
    private readonly DriverLicense _license;
    private ObservableCollection<CategoryViewModel> _categories;

    protected override int? Save(DomainContext context)
    {
        throw new NotImplementedException();
    }

    protected override void Remove(DomainContext context)
    {
        throw new NotImplementedException();
    }
    
    public DriverEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _license = new DriverLicense();
        _driver = new Driver
        {
            DriverLicense = _license
        };

        using DomainContext context = contextFactory.CreateDbContext();
        _categories = new ObservableCollection<CategoryViewModel>(
            context.Categories.Select(c => new CategoryViewModel(c)));
    }
    
    public DriverEditViewModel(Driver driver, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _driver = driver;
        _license = _driver.DriverLicense;
        Id = _driver.Id;
        
        using DomainContext context = contextFactory.CreateDbContext();
        _categories = new ObservableCollection<CategoryViewModel>(
            context.Categories.Select(c => new CategoryViewModel(c)));
    }

    protected override bool CanSave() => //TODO: add validator
        !string.IsNullOrWhiteSpace(PayrollNumber) &&
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Surname) &&
        !string.IsNullOrWhiteSpace(Patronymic) &&
        !string.IsNullOrWhiteSpace(Gender) &&
        !string.IsNullOrWhiteSpace(DriverClass) &&
        !string.IsNullOrWhiteSpace(ProfessionalStandardDetails) &&
        !string.IsNullOrWhiteSpace(EmploymentBookDetails) &&
        _license.DateOfIssue.Year - BirthDate.Year > 16 &&
        _license.DateOfExpiration > _license.DateOfIssue &&
        _license.Categories.Count > 0 &&
        !string.IsNullOrWhiteSpace(_license.LicenseNumber);

    public string Surname
    {
        get => _driver.Surname;
        set { _driver.Surname = value; RaisePropertyChanged(); }
    }

    public string Patronymic
    {
        get => _driver.Patronymic;
        set { _driver.Patronymic = value; RaisePropertyChanged(); }
    }
    
    public string Name
    {
        get => _driver.Name;
        set { _driver.Name = value; RaisePropertyChanged(); }
    }
    
    public string PayrollNumber
    {
        get => _driver.PayrollNumber;
        set { _driver.PayrollNumber = value; RaisePropertyChanged(); }
    }

    public DateTime BirthDate
    {
        get => _driver.BirthDate;
        set { _driver.BirthDate = value; RaisePropertyChanged(); }
    }

    public string Gender
    {
        get => _driver.Gender;
        set { _driver.Gender = value; RaisePropertyChanged(); }
    }

    public string DriverClass
    {
        get => _driver.DriverClass;
        set { _driver.DriverClass = value; RaisePropertyChanged(); }
    }

    public string ProfessionalStandardDetails
    {
        get => _driver.ProfessionalStandardDetails;
        set { _driver.ProfessionalStandardDetails = value; RaisePropertyChanged(); }
    }

    public string EmploymentBookDetails
    {
        get => _driver.EmploymentBookDetails;
        set { _driver.EmploymentBookDetails = value; RaisePropertyChanged(); }
    }
    
    public string LicenseNumber
    {
        get => _license.LicenseNumber;
        set { _license.LicenseNumber = value; RaisePropertyChanged(); }
    }

    public DateTime DateOfIssue
    {
        get => _license.DateOfIssue;
        set { _license.DateOfIssue = value; RaisePropertyChanged(); DateOfExpiration = DateOfIssue.AddYears(10); }
    }

    public DateTime DateOfExpiration
    {
        get => _license.DateOfExpiration;
        set { _license.DateOfExpiration = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<CategoryViewModel> Categories
    {
        get => _categories;
        set { _categories = value; RaisePropertyChanged(); }
    }
}