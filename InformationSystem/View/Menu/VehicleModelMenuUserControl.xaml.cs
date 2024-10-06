using System.Windows.Controls;

namespace InformationSystem.View.Menu;

public partial class VehicleModelMenuUserControl : UserControl
{
    public VehicleModelMenuUserControl()
    {
        InitializeComponent();
    }

    private void NumberTextBoxPreviewInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        bool result = true;
        foreach (char item in e.Text)
        {
            result &= char.IsDigit(item);
        }
        if (result)
        {
            e.Handled = false;
        }
        else
        {
            e.Handled = true;
        }
    }
}