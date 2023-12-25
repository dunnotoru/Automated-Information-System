using System;
using System.Windows.Controls;

namespace UI.View
{
    public partial class PassengerRegistrationView : UserControl
    {
        public PassengerRegistrationView()
        {
            InitializeComponent();
        }

        private void NumberTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool result = true;
            foreach (char item in e.Text)
            {
                result &= char.IsDigit(item);
            }
            if(result)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void NotNumberTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool result = false;
            foreach (char item in e.Text)
            {
                result |= char.IsDigit(item);
            }
            if (result)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void DatePicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            DateTime result;
            bool r = DateTime.TryParse(e.Text, out result);

            if (r == false)
            {
                e.Handled = true;
                return;
            }

            if(result >= DateTime.Now)
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }
    }
}
