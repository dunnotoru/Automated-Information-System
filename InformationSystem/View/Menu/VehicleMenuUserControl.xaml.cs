namespace InformationSystem.View.Menu;

public partial class VehicleMenuUserControl : System.Windows.Controls.UserControl
{
    public VehicleMenuUserControl()
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