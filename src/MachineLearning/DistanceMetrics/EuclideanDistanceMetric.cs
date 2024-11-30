using MachineLearning.Interfaces;

namespace MachineLearning.DistanceMetrics
{
    public class EuclideanDistanceMetric : IPointDistanceMetric
    {
        public double CalculateDistance(IEnumerable<double> value1, IEnumerable<double> value2)
        {
            var sum = 0d;
            var count = value1.Count();

            for (int i = 0; i < value1.Count(); i++)
            {
                sum += Math.Pow(value1.ElementAt(i) - value2.ElementAt(i), 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
