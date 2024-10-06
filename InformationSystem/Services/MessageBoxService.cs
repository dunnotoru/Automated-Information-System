using System.Windows;
using InformationSystem.Services.Abstractions;

namespace InformationSystem.Services;

internal class MessageBoxService : IMessageBoxService
{
    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}