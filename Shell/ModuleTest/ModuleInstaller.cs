using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Contract;

namespace ModuleTest
{
    public class ModuleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<TestViewModel>())
                .Register(Component.For<IModule>().ImplementedBy<ModuleImplementation>());
        }
    }
}
