using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора тегов в строку и наоборот.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IValueConverter"/>.
    /// </remarks>
    public class TagsToStringConverter : IValueConverter
    {
        /// <summary>
        /// Регулярное выражение тегов.
        /// </summary>
        private static Regex _tagsRegex = new Regex(@"\b\w+\b");

        /// <inheritdoc/>
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var tags = (IEnumerable<string>?)value;
            return tags != null ? string.Join(" ", tags) : "";
        }

        /// <inheritdoc/>
        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var text = (string?)value;
            return text != null ? _tagsRegex.Matches(text.ToString()).
                Select(m => m.Value) : Enumerable.Empty<string>();
        }
    }
}
