using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.HelperViewModels;

public class DriverLicenseViewModel : ViewModelBase
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private int _id;
    private string _licenseNumber;
    private DateTime _dateOfIssue;
    private DateTime _dateOfExpiration;
    private ObservableCollection<CategoryViewModel> _categories;

    public DriverLicenseViewModel(DriverLicense license, IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Id = license.Id;
        LicenseNumber = license.LicenseNumber;
        DateOfIssue = license.DateOfIssue;
        DateOfExpiration = license.DateOfExpiration;
        Categories = new ObservableCollection<CategoryViewModel>();
    }

    public DriverLicenseViewModel(IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Id = 0;
        LicenseNumber = "";
        DateOfIssue = DateTime.Now;
        DateOfExpiration = DateTime.Now;
        Categories = new ObservableCollection<CategoryViewModel>();

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
        set { _id = value; NotifyPropertyChanged(); }
    }

    public string LicenseNumber
    {
        get { return _licenseNumber; }
        set { _licenseNumber = value; NotifyPropertyChanged(); }
    }

    public DateTime DateOfIssue
    {
        get { return _dateOfIssue; }
        set { _dateOfIssue = value; NotifyPropertyChanged(); DateOfExpiration = DateOfIssue.AddYears(10); }
    }

    public DateTime DateOfExpiration
    {
        get { return _dateOfExpiration; }
        set { _dateOfExpiration = value; NotifyPropertyChanged(); }
    }

    public ObservableCollection<CategoryViewModel> Categories
    {
        get { return _categories; }
        set { _categories = value; NotifyPropertyChanged(); }
    }
}