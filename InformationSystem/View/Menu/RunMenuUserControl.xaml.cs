using System.Windows.Input;

namespace InformationSystem.View.Menu;

public partial class RunMenuUserControl : System.Windows.Controls.UserControl
{
    public RunMenuUserControl()
    {
        InitializeComponent();
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        foreach (char c in e.Text)
        {
            if (!char.IsDigit(c) && c != ':')
            {
                e.Handled = true;
                break;
            }
        }
    }

    private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
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