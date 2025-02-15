using MachineLearning.Interfaces;

namespace MachineLearning.Aggregators
{
    /// <summary>
    /// Класс агрегатора данных по квантилю.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAggregator"/>.
    /// </remarks>
    public class QuantileAggregator : IAggregator
    {
        /// <summary>
        /// Доля квантиля.
        /// </summary>
        private readonly double _quantileValue;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="QuantileAggregator"/>.
        /// </summary>
        /// <param name="quantileValue">Доля квантиля.</param>
        public QuantileAggregator(double quantileValue)
        {
            _quantileValue = quantileValue;
        }

        /// <inheritdoc/>
        public double AggregateToValue(IEnumerable<double> data)
        {
            var sortedData = data.Order();
            var index = _quantileValue * (sortedData.Count() - 1);
            var lowerIndex = (int)Math.Floor(index);
            var upperIndex = (int)Math.Ceiling(index);
            var lowerValue = sortedData.ElementAt(lowerIndex);
            if (lowerIndex == upperIndex)
            {
                return lowerValue;
            }
            var upperValue = sortedData.ElementAt(upperIndex);
            if (upperValue == lowerValue)
            {
                return lowerValue;
            }
            var fraction = index - lowerIndex;
            return sortedData.ElementAt(lowerIndex) + fraction * (upperValue - lowerValue);
        }
    }
}
