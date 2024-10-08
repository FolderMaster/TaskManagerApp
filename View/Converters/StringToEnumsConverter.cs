using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace View.Converters
{
    public class StringToEnumsConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var type = Type.GetType(parameter.ToString());
            return Enum.GetValues(type);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new InvalidOperationException();
    }
}
