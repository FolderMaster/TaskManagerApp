using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace View.Converters
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            if (value == default)
            {
                return default;
            }
            var dateTime = (DateTime)value;
            return new TimeSpan(dateTime.Hour, dateTime.Minute,
                dateTime.Second, dateTime.Millisecond, dateTime.Microsecond);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var timeSpan = (TimeSpan)value;
            var dateTime = (DateTime)parameter;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeSpan.Hours,
                timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds, timeSpan.Microseconds);
        }
    }
}
