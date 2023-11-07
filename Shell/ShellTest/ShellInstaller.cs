using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contract;
using ShellTest.ViewModel;

namespace ShellTest
{
    public class ShellInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.Register(Component.For<ShellViewModel>());
            container.Register(Component.For<ModuleLoader>());
            container.Register(Component.For<IShell>().ImplementedBy<ShellImplementation>());
        }
    }
}
