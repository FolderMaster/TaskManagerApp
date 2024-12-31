using Avalonia.Data.Converters;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ViewModel.Technicals;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора элементов статистики в столбцы столбчатой диаграммы.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IValueConverter"/>.
    /// </remarks>
    public class StatisticElementsToColumnSeriesConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            var pieElements = (IEnumerable<StatisticElement>?)value ??
                new List<StatisticElement>();
            return pieElements.Select(e => new ColumnSeries<double>()
            {
                Name = e.Name,
                Values = [e.Value]
            }).Cast<ISeries>().ToArray();
        }

        /// <inheritdoc/>
        public object ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture) => throw new InvalidOperationException();
    }
}
