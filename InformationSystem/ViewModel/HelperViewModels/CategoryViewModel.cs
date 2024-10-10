using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class CategoryViewModel : ViewModelBase
{
    private readonly Category _category;
    private bool _isSelected = false;

    public CategoryViewModel(Category category)
    {
        _category = category;
    }
    public int Id => _category.Id;
    
    public string Name => _category.Name;

    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; RaisePropertyChanged(); }
    }
}