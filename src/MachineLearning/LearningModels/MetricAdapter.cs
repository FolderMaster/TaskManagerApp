using Accord.Math.Distances;

using MachineLearning.Interfaces;

namespace MachineLearning.LearningModels
{
    /// <summary>
    /// Адаптер для использования метрики расстояния <see cref="IPointDistanceMetric"/>
    /// с интерфейсом <see cref="IMetric{double[]}"/>.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMetric{double[]}"/>.
    /// </remarks>
    public class MetricAdapter : IMetric<double[]>
    {
        /// <summary>
        /// Возвращает и задаёт метрику дистанцию.
        /// </summary>
        public IPointDistanceMetric DistanceMetric { get; set; }

        /// <inheritdoc />
        public double Distance(double[] x, double[] y) => DistanceMetric.CalculateDistance(x, y);
    }
}
