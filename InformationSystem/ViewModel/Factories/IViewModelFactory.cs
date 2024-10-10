using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.Menu;

namespace InformationSystem.ViewModel.Factories;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
    EditViewModel CreateEditViewModel<TEntity>() where TEntity : EntityBase;
    EditViewModel CreateEditViewModel<TEntity>(TEntity entity) where TEntity : EntityBase;
}