using MachineLearning.Interfaces;

namespace MachineLearning.Aggregators
{
    /// <summary>
    /// Класс агрегатора данных по максимуму.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAggregator"/>.
    /// </remarks>
    public class MaxAggregator : IAggregator
    {
        /// <inheritdoc/>
        public double AggregateToValue(IEnumerable<double> data) => data.Max();
    }
}
