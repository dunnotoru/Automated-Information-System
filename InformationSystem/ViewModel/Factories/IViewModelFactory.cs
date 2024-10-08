using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.Menu;

namespace InformationSystem.ViewModel.Factories;

public interface IViewModelFactory
{
    ObservableObject CreateViewModel<TViewModel>() where TViewModel : ObservableObject;
    EditViewModel CreateEditViewModel<TEntity>() where TEntity : EntityBase;
    EditViewModel CreateEditViewModel<TEntity>(TEntity entity) where TEntity : EntityBase;
}