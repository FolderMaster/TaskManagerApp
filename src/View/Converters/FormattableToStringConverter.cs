using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора форматируемого объекта в строку.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IValueConverter"/>.
    /// </remarks>
    public class FormattableToStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var dateTime = (IFormattable)value;
            var format = parameter.ToString();
            return dateTime.ToString(format, culture);
        }

        /// <inheritdoc/>
        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
