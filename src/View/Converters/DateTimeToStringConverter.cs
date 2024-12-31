using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора даты в строку.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IValueConverter"/>.
    /// </remarks>
    public class DateTimeToStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            var format = parameter.ToString();
            return dateTime.ToString(format, culture);
        }

        /// <inheritdoc/>
        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
