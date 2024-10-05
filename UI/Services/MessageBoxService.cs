using System.Windows;
using UI.Services.Abstractions;

namespace UI.Services;

internal class MessageBoxService : IMessageBoxService
{
    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}