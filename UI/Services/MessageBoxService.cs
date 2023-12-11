using System.Windows;

namespace UI.Services
{
    internal class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
