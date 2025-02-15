using MachineLearning.Interfaces;

namespace MachineLearning.Aggregators
{
    /// <summary>
    /// Класс агрегатора данных по минимуму.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAggregator"/>.
    /// </remarks>
    public class MinAggregator : IAggregator
    {
        /// <inheritdoc/>
        public double AggregateToValue(IEnumerable<double> data) => data.Min();
    }
}
