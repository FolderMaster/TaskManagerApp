using Avalonia.Data.Converters;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace View.Converters
{
    public class EnumToEnumsConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var type = value.GetType();
            return Enum.GetValues(type) as IEnumerable<TaskStatus>;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new InvalidOperationException();
    }
}
