using System.Windows.Controls;

namespace InformationSystem.View.Dispatcher;

public partial class DriverMenuUserControl : UserControl
{
    public DriverMenuUserControl()
    {
        InitializeComponent();
    }

    private void NumberPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        foreach (char c in e.Text)
        {
            if (!char.IsDigit(c))
            {
                e.Handled = true;
                break;
            }
        }
    }
}