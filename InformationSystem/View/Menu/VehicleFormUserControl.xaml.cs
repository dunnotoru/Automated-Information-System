namespace InformationSystem.View.Menu;

public partial class VehicleFormUserControl : System.Windows.Controls.UserControl
{
    public VehicleFormUserControl()
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