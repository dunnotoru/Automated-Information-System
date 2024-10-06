namespace InformationSystem.View.Menu;

public partial class DriverMenuUserControl : System.Windows.Controls.UserControl
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