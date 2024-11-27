using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class SmapeScoreMetric : IRegressionModelScoreMetric
    {
        public double CalculateScore(IEnumerable<double> expected, IEnumerable<double> predicted)
        {
            var result = 0d;
            var count = expected.Count();
            for (var n = 0; n < count; ++n)
            {
                var a = expected.ElementAt(n);
                var p = predicted.ElementAt(n);
                result += a == p ? 0 : 2 * Math.Abs(p - a) / (Math.Abs(p) + Math.Abs(a));
            }
            return result / count;
        }
    }
}
