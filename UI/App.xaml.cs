using Castle.Windsor;
using System.Windows;
using UI.View;
using UI.ViewModel;

namespace UI
{
    public partial class App : Application
    {
        private readonly IWindsorContainer _container 
            = new WindsorContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            Configure();

            LoginViewModel viewModel = _container.Resolve<LoginViewModel>();
            viewModel.AuthenticationDone += ChangeLoginToMain;
            
            MainWindow = new LoginWindow() { DataContext = viewModel };
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ChangeLoginToMain()
        {
            ShellWindow window = new ShellWindow() { DataContext = new ShellViewModel() };
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            MainWindow.Close();
            MainWindow = window;
        }

        private void Configure()
        {
            _container.Install(new ContainerInstaller());
        }
    }
}
