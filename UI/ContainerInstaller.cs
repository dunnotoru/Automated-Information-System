using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces;
using Domain.UseCases.AccountUseCases;
using UI.Model;
using UI.ViewModel;

namespace UI
{
    internal class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AccountContext>().LifestyleTransient());
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>());
            container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>());
            container.Register(Component.For<AuthenticationUseCase>());
            container.Register(Component.For<LoginViewModel>());
        }
    }
}
