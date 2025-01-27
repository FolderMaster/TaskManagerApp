﻿using MachineLearning.Interfaces;

namespace MachineLearning.DistanceMetrics
{
    /// <summary>
    /// Класс метрики Евклидового расстояния.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPointDistanceMetric"/>.
    /// </remarks>
    public class EuclideanDistanceMetric : IPointDistanceMetric
    {
        /// <inheritdoc />
        public double CalculateDistance(IEnumerable<double> object1, IEnumerable<double> object2)
        {
            var sum = 0d;
            var count = object1.Count();

            for (int i = 0; i < object1.Count(); i++)
            {
                sum += Math.Pow(object1.ElementAt(i) - object2.ElementAt(i), 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
