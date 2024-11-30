using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(F1ScoreMetric))]
    public class F1ScoreMetricTests
    {
        private F1ScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = "Тестирование CalculateDistance.")]
        public void CalculateDistance_ReturnCorrectValue()
        {
            var actual = new int[] { 2, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 2, 1, 0, 0 };
            var expected = 0.33;
            var accuracy = 0.01;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected).Within(accuracy),
                "Неправильно расчитана оценка!");
        }
    }
}
