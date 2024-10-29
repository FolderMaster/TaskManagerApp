using Avalonia.Data.Converters;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ViewModel;

namespace View.Converters
{
    public class StatisticElementsToPieSeriesConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var pieElements = (IEnumerable<StatisticElement>?)value ??
                new List<StatisticElement>();
            return pieElements.Select(e => new PieSeries<double>()
            {
                Name = e.Name,
                Values = [e.Value]
            }).ToArray().Cast<ISeries>();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new InvalidOperationException();
    }
}
