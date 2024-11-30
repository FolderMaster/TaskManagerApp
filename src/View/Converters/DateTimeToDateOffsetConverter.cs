using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace View.Converters
{
    public class DateTimeToDateOffsetConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            if (value == default)
            {
                return default;
            }
            return new DateTimeOffset((DateTime)value);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => ((DateTimeOffset)value).DateTime;
    }
}
