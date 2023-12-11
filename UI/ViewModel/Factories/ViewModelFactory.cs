using Castle.Windsor;

namespace UI.ViewModel.Factories
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly IWindsorContainer _container;

        public ViewModelFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public ViewModelBase CreateViewModel<TViewModel>()
            where TViewModel : ViewModelBase
        {
            return _container.Resolve<TViewModel>();
        }
    }
}
