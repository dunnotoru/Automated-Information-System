using Castle.Windsor;
using System.Windows;
using UI.View;
using UI.ViewModel;
using System.IO;
using System.Configuration;

namespace UI
{
    public partial class App : Application
    {
        private readonly IWindsorContainer _container
            = new WindsorContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            Configure();
            CheckFiles();

            LoginViewModel viewModel = _container.Resolve<LoginViewModel>();
            viewModel.AuthenticationDone += ChangeLoginToMain;

            MainWindow = new LoginWindow() { DataContext = viewModel };
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();

            base.OnStartup(e);
        }


        private void ChangeLoginToMain()
        {
            ShellWindow window = new ShellWindow() { DataContext = _container.Resolve<ShellViewModel>() };

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            MainWindow.Close();
            MainWindow = window;
            MainWindow.WindowState = WindowState.Maximized;

        }
        private void CheckFiles()
        {
            string ticketFolderPath = ConfigurationManager.AppSettings.Get("TicketFolderPath") ?? Directory.GetCurrentDirectory();
            string receiptFolderPath = ConfigurationManager.AppSettings.Get("ReceiptFolderPath") ?? Directory.GetCurrentDirectory();
            string certificatePath = ConfigurationManager.AppSettings.Get("CertificatePath") ?? Directory.GetCurrentDirectory();

            if(!Directory.Exists(ticketFolderPath))
                Directory.CreateDirectory(ticketFolderPath);
            if(!Directory.Exists(receiptFolderPath))
                Directory.CreateDirectory(receiptFolderPath);
            if(!File.Exists(certificatePath))
                File.Create(certificatePath);
        }

        private void Configure()
        {
            _container.Install(new ContainerInstaller());
        }
    }
}
