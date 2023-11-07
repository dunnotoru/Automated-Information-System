using Caliburn.Micro;
using Castle.Windsor.Installer;
using Castle.Windsor;
using System.Reflection;
using System;
using Contract;

namespace ShellTest
{
    public class ModuleLoader
    {
        private readonly IWindsorContainer _mainContainer;

        public ModuleLoader(IWindsorContainer mainContainer)
        {
            _mainContainer = mainContainer;
        }

        public IModule LoadModule(Assembly assembly)
        {
            try
            {
                var moduleInstaller = FromAssembly.Instance(assembly);

                var modulecontainer = new WindsorContainer();
                _mainContainer.AddChildContainer(modulecontainer);

                modulecontainer.Install(moduleInstaller);

                var module = modulecontainer.Resolve<IModule>();

                if (!AssemblySource.Instance.Contains(assembly))
                    AssemblySource.Instance.Add(assembly);

                return module;
            }
            catch (Exception ex)
            {
                //TODO: good exception handling 
                return null;
            }
        }
    }
}
