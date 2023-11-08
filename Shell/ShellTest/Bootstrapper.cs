using Caliburn.Micro;
using Castle.Windsor;
using Shell.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Shell
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "ViewModel",
                DefaultSubNamespaceForViews = "View"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);

            var loader = _container.Resolve<ModuleLoader>();

            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pattern = "*.dll";

            string[] files = Directory.GetFiles(exeDir, pattern);
            files
                .Select(Assembly.LoadFrom)
                .Select(loader.LoadModule)
                .Where(module => module != null).ToList()
                .ForEach(module => module.Initialize());

            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Install(new ShellInstaller());
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)

                ? _container.Kernel.HasComponent(service)
                    ? _container.Resolve(service)
                    : base.GetInstance(service, key)

                : _container.Kernel.HasComponent(key)
                    ? _container.Resolve(key, service)
                    : base.GetInstance(service, key);
        }
    }
}
