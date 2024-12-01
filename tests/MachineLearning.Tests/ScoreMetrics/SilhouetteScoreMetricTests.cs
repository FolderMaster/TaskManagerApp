using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(SilhouetteScoreMetric),
        Description = $"Тестирование класса {nameof(SilhouetteScoreMetric)}.")]
    public class SilhouetteScoreMetricTests
    {
        private SilhouetteScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description =
            $"Тестирование метода {nameof(SilhouetteScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var data = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10]
            };
            var predicted = new int[] { 0, 0, 1, 1 };
            var expected = 0.81;
            var tolerance = 0.01;

            var result = _scoreMetric.CalculateScore(predicted, data);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно расчитана оценка!");
        }
    }
}
