using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

internal class DriverViewModel : ViewModelBase
{
    private int _id;
    private string _fullName;
    private string _payrollNumber;

    public DriverViewModel(Driver driver)
    {
        Id = driver.Id;
        FullName = $"{driver.Surname} {driver.Name} {driver.Patronymic}";
        PayrollNumber = driver.PayrollNumber;
    }

    public DriverViewModel()
    {
        Id = 0;
        FullName = "";
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }

    public string FullName
    {
        get { return _fullName; }
        set { _fullName = value; NotifyPropertyChanged(); }
    }

    public string PayrollNumber
    {
        get { return _payrollNumber; }
        set { _payrollNumber = value; NotifyPropertyChanged(); }
    }
}