using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    internal class MultiEnableConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            foreach (var item in values)
            {
                result &= !(bool)item;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
