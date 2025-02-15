using MachineLearning.Interfaces;

namespace MachineLearning.DistanceMetrics
{
    /// <summary>
    /// Класс метрики расстояние Манхэттена.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPointDistanceMetric"/>.
    /// </remarks>
    public class ManhattanDistanceMetric : IPointDistanceMetric
    {
        /// <inheritdoc />
        public double CalculateDistance(IEnumerable<double> object1, IEnumerable<double> object2)
        {
            var sum = 0d;
            var count = object1.Count();

            for (var i = 0; i < count; ++i)
            {
                sum += Math.Abs(object1.ElementAt(i) - object2.ElementAt(i));
            }
            return sum;
        }
    }
}
