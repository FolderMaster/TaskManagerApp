using MachineLearning.Interfaces;
using MachineLearning.DistanceMetrics;
using MachineLearning.Aggregators;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики оценки Силуэта для модели обучения кластеризации на данных.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IDataClusteringScoreMetric"/>.
    /// </remarks>
    public class SilhouetteScoreMetric : IDataClusteringScoreMetric
    {
        /// <summary>
        /// Возвращает и задаёт метрику дистанцию.
        /// </summary>
        public IPointDistanceMetric PointDistanceMetric { get; set; } =
            new EuclideanDistanceMetric();

        /// <summary>
        /// Возвращает и задаёт агрегатор.
        /// </summary>
        public IAggregator Aggregator { get; set; } = new MeanAggregator();

        /// <inheritdoc />
        public double CalculateScore(IEnumerable<int> predicted,
            IEnumerable<IEnumerable<double>> data)
        {
            var count = predicted.Count();
            var clustersPointDictionary = data.Select((p, i) =>
                new { Point = p, Cluster = predicted.ElementAt(i) }).GroupBy(x => x.Cluster).
                ToDictionary(g => g.Key, g => g.Select(x => x.Point));
            var silhouetteScores = new List<double>();

            for (var n = 0; n < count; ++n)
            {
                var cluster = predicted.ElementAt(n);
                var point = data.ElementAt(n);
                var a = CalculateAverageIntraClusterDistance
                    (clustersPointDictionary, point, cluster);
                var b = CalculateAverageNearestClusterDistance
                    (clustersPointDictionary, point, cluster);

                var silhouetteScore = (b - a) / Math.Max(a, b);
                silhouetteScores.Add(silhouetteScore);
            }

            return Aggregator.AggregateToValue(silhouetteScores);
        }

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.75 => ScoreMetricCategory.Excellent,
            < 0.75 and >= 0.6 => ScoreMetricCategory.Good,
            < 0.6 and >= 0.4 => ScoreMetricCategory.Satisfactory,
            < 0.4 and >= 0.25 => ScoreMetricCategory.Bad,
            _ => ScoreMetricCategory.Horrible
        };

        /// <summary>
        /// Вычисляет среднее расстояние между точкой и всеми другими точками в том же кластере.
        /// </summary>
        /// <param name="clustersPointDictionary">Словарь точек кластеров.</param>
        /// <param name="currentPoint">Текущая точка.</param>
        /// <param name="currentCluster">Текущий кластер.</param>
        /// <returns>Возвращает среднее расстояние между точкой и всеми другими точками в том же
        /// кластере.</returns>
        private double CalculateAverageIntraClusterDistance
            (Dictionary<int, IEnumerable<IEnumerable<double>>> clustersPointDictionary,
            IEnumerable<double> currentPoint, int currentCluster)
        {
            var clusterPoints = clustersPointDictionary[currentCluster].
                Where(p => p != currentPoint);

            if (clusterPoints.Count() == 0)
            {
                return 0;
            }
            return clusterPoints.Average(p => PointDistanceMetric.
                CalculateDistance(currentPoint, p));
        }

        /// <summary>
        /// Вычисляет среднее расстояние между точкой и точками из ближайшего другого кластера.
        /// </summary>
        /// <param name="clustersPointDictionary">Словарь точек кластеров.</param>
        /// <param name="currentPoint">Текущая точка.</param>
        /// <param name="currentCluster">Текущий кластер.</param>
        /// <returns>Возвращает минимальное среднее расстояние между точкой и точками из другого
        /// кластера.</returns>
        private double CalculateAverageNearestClusterDistance
            (Dictionary<int, IEnumerable<IEnumerable<double>>> clustersPointDictionary,
            IEnumerable<double> currentPoint, int currentCluster)
        {
            var otherClusters = clustersPointDictionary.Keys.Where(c => c != currentCluster);
            var minDistance = double.MaxValue;

            foreach (var cluster in otherClusters)
            {
                var clusterPoints = clustersPointDictionary[cluster];
                var averageDistance = clusterPoints.Average(p => PointDistanceMetric.
                    CalculateDistance(currentPoint, p));
                minDistance = Math.Min(minDistance, averageDistance);
            }
            return minDistance;
        }
    }
}
