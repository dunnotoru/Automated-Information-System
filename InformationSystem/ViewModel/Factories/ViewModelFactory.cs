using System;
using Microsoft.Extensions.DependencyInjection;

namespace InformationSystem.ViewModel.Factories;

internal class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _provider;

    public ViewModelFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public ViewModelBase CreateViewModel<TViewModel>()
        where TViewModel : ViewModelBase
    {
        return (ViewModelBase)_provider.GetRequiredService<TViewModel>();
    }
}