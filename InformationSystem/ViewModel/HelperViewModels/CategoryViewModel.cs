using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;

namespace InformationSystem.ViewModel.HelperViewModels;

public class CategoryViewModel : ViewModelBase
{
    private readonly ICategoryRepository _categoryRepository;
    private string _name;
    private bool _isSelected;


    public CategoryViewModel(Category category, ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        Id = category.Id;
        Name = category.Name;
    }
    public int Id { get; }
    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; NotifyPropertyChanged(); }
    }

    public Category GetCategory()
    {
        return _categoryRepository.GetById(Id);
    }
}