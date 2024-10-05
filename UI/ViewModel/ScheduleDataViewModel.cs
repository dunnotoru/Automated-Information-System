using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel;

internal class ScheduleDataViewModel : ViewModelBase
{
    private readonly IScheduleRepository _scheduleRepository;

    private ObservableCollection<ScheduleViewModel> _items;

    public ICommand UpdateDataCommand { get; }

    public ScheduleDataViewModel(IScheduleRepository scheduleRepository)
    {
        Items = new ObservableCollection<ScheduleViewModel>();
        _scheduleRepository = scheduleRepository;
        Update();

        UpdateDataCommand = new RelayCommand(Update);
    }

    private void Update()
    {
        Items.Clear();
        foreach (var item in _scheduleRepository.GetAll())
        {
            ScheduleViewModel vm = new ScheduleViewModel(item);
            Items.Add(vm);
        }
    }

    public ObservableCollection<ScheduleViewModel> Items
    {
        get { return _items; }
        set { _items = value; NotifyPropertyChanged(); }
    }
}