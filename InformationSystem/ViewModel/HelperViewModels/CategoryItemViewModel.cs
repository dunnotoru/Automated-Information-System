namespace InformationSystem.ViewModel.HelperViewModels;

public class CategoryItemViewModel : ViewModelBase
{
    private bool _isSelected;
    public CategoryViewModel Category { get; }

    public CategoryItemViewModel(CategoryViewModel category)
    {
        Category = category;
    }

    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; NotifyPropertyChanged(); }
    }

}