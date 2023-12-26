using Castle.Windsor;
using System.Windows;
using UI.View;
using UI.ViewModel;
using System.IO;
using System.Configuration;
using Domain.RepositoryInterfaces;
using System.Collections.Generic;
using Domain.Models;
using System.Linq;
using System;

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

            IAccountRepository repo = _container.Resolve<IAccountRepository>();
            List<Account> accounts = repo.GetAll().ToList();
            if (accounts.Count == 0)
                LoadRegistration();
            else 
                LoadLogin();

            

            base.OnStartup(e);
        }

        private void LoadLogin()
        {
            LoginViewModel vm = _container.Resolve<LoginViewModel>();
            vm.AuthenticationDone += ChangeLoginToMain;

            MainWindow = new LoginWindow() { DataContext = vm };
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
        }

        private void LoadRegistration()
        {
            RegistrationViewModel vm = _container.Resolve<RegistrationViewModel>();
            vm.Registration += ChangeLoginToMain;
            MainWindow = new RegistrationWindow() { DataContext = vm };
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
        }

        private void ChangeLoginToMain(object sender, EventArgs e)
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
