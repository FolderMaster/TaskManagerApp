﻿using MachineLearning.Interfaces;
using MachineLearning.DistanceMetrics;

namespace MachineLearning.ScoreMetrics
{
    public class SilhouetteScoreMetric : IClusteringScoreMetric
    {
        public IPointDistanceMetric PointDistanceMetric { get; set; } =
            new EuclideanDistanceMetric();

        public double CalculateScore(IEnumerable<int> actual,
            IEnumerable<IEnumerable<double>> data)
        {
            var count = actual.Count();
            var clustersPointDictionary = data.Select((p, i) =>
                new { Point = p, Cluster = actual.ElementAt(i) }).GroupBy(x => x.Cluster).
                ToDictionary(g => g.Key, g => g.Select(x => x.Point));
            var totalSilhouetteScore = 0d;

            for (var n = 0; n < count; ++n)
            {
                var cluster = actual.ElementAt(n);
                var point = data.ElementAt(n);
                var a = CalculateAverageIntraClusterDistance
                    (clustersPointDictionary, point, cluster);
                var b = AverageNearestClusterDistance(clustersPointDictionary, point, cluster);

                var silhouetteScore = (b - a) / Math.Max(a, b);
                totalSilhouetteScore += silhouetteScore;
            }

            return totalSilhouetteScore / count;
        }

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

        private double AverageNearestClusterDistance
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