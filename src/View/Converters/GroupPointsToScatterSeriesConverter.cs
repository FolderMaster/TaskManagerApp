using Avalonia.Data.Converters;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ViewModel.Technicals;

namespace View.Converters
{
    public class GroupPointsToScatterSeriesConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var groupPoints = (IEnumerable<GroupPoint>?)value ?? new List<GroupPoint>();
            return groupPoints.GroupBy(p => p.GroupIndex).Select(g =>
                new ScatterSeries<WeightedPoint>()
                {
                    Name = g.Key.ToString(),
                    Values = g.Select(g => new WeightedPoint(g.Value, g.Value2, 1)).ToList()
                }).Cast<ISeries>().ToArray();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
