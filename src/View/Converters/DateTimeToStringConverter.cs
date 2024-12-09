using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace View.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            var format = parameter.ToString();
            return dateTime.ToString(format, culture);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
