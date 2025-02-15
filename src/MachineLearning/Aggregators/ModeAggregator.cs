using MachineLearning.Interfaces;

namespace MachineLearning.Aggregators
{
    /// <summary>
    /// Класс агрегатора данных в группу по моде.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IAggregator"/> и <see cref="IGroupAggregator"/>.
    /// </remarks>
    public class ModeAggregator : IAggregator, IGroupAggregator
    {
        /// <inheritdoc/>
        public IEnumerable<double> AggregateToGroup(IEnumerable<double> data)
        {
            var frequencies = data.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
            var maxFrequency = frequencies.Values.Max();
            return frequencies.Where(p => p.Value == maxFrequency).Select(p => p.Key);
        }

        /// <inheritdoc/>
        public double AggregateToValue(IEnumerable<double> data)
        {
            var modes = AggregateToGroup(data);
            return modes.Average();
        }
    }
}
