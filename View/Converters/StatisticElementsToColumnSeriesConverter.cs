﻿using Avalonia.Data.Converters;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ViewModel;

namespace View.Converters
{
    public class StatisticElementsToColumnSeriesConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var pieElements = (IEnumerable<StatisticElement>?)value ??
                new List<StatisticElement>();
            return pieElements.Select(e => new ColumnSeries<double>()
            {
                Name = e.Name,
                Values = [e.Value]
            }).ToArray().Cast<ISeries>();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new InvalidOperationException();
    }
}