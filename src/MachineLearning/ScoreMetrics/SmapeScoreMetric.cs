using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class SmapeScoreMetric : IRegressionScoreMetric
    {
        public double CalculateScore(IEnumerable<double> actual, IEnumerable<double> predicted)
        {
            var result = 0d;
            var count = actual.Count();

            for (var n = 0; n < count; ++n)
            {
                var actualValue = actual.ElementAt(n);
                var predictedValue = predicted.ElementAt(n);
                var value = actualValue == predictedValue ? 0 : 2 *
                    Math.Abs(actualValue - predictedValue) /
                    (Math.Abs(predictedValue) + Math.Abs(actualValue));
                result += value;
            }
            return result / count;
        }
    }
}
