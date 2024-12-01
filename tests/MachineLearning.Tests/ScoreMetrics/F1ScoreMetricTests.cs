using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(F1ScoreMetric),
        Description = $"Тестирование класса {nameof(F1ScoreMetric)}.")]
    public class F1ScoreMetricTests
    {
        private F1ScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = $"Тестирование метода {nameof(F1ScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var actual = new int[] { 2, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 2, 1, 0, 0 };
            var expected = 1 / 3d;
            var tolerance = 0.01;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно расчитана оценка!");
        }
    }
}
