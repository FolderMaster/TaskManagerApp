using MachineLearning.Interfaces;

namespace MachineLearning.DistanceMetrics
{
    /// <summary>
    /// Класс метрики расстояние Чебышёва.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPointDistanceMetric"/>.
    /// </remarks>
    public class ChebyshevDistanceMetric : IPointDistanceMetric
    {
        /// <inheritdoc />
        public double CalculateDistance(IEnumerable<double> object1, IEnumerable<double> object2)
        {
            var max = 0d;
            var count = object1.Count();

            for (var i = 0; i < count; ++i)
            {
                var value = Math.Abs(object1.ElementAt(i) - object2.ElementAt(i));
                if (max < value)
                {
                    max = value;
                }
            }
            return max;
        }
    }
}
