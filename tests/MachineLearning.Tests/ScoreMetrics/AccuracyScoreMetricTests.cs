using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(AccuracyScoreMetric))]
    public class AccuracyScoreMetricTests
    {
        private AccuracyScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = "Тестирование CalculateDistance.")]
        public void CalculateDistance_ReturnCorrectValue()
        {
            var actual = new int[] { 1, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 0, 1, 0, 0 };
            var expected = 0.8;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана оценка!");
        }
    }
}
