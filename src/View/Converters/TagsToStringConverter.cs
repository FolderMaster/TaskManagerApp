using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace View.Converters
{
    public class TagsToStringConverter : IValueConverter
    {
        private static Regex _tagsRegex = new Regex(@"\b\w+\b");
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var tags = (IEnumerable<string>?)value;
            return tags != null ? string.Join(" ", tags) : "";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var text = (string?)value;
            return text != null ? _tagsRegex.Matches(text.ToString()).
                Select(m => m.Value) : Enumerable.Empty<string>();
        }
    }
}
