using System;
using System.Globalization;
using System.Windows.Data;

namespace InformationSystem.Converters;

internal class DateConverter : IValueConverter
{
    private DateTime timePickerDate;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        timePickerDate = ((DateTime)(value));

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return timePickerDate;

        DateTime datePickerDate = ((DateTime)(value));

        if (datePickerDate.Hour != timePickerDate.Hour
            || datePickerDate.Minute != timePickerDate.Minute
            || datePickerDate.Second != timePickerDate.Second)
        {
            DateTime result = new DateTime(datePickerDate.Year,
                datePickerDate.Month,
                datePickerDate.Day,
                timePickerDate.Hour,
                timePickerDate.Minute,
                timePickerDate.Second);

            return result;
        }
        return datePickerDate;
    }
}