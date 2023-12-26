using System.Windows.Controls;

namespace UI.View
{
    public partial class VehicleMenuUserControl : UserControl
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
}