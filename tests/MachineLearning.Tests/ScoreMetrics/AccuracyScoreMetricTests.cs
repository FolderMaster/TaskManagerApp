using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(AccuracyScoreMetric),
        Description = $"Тестирование класса {nameof(AccuracyScoreMetric)}.")]
    public class AccuracyScoreMetricTests
    {
        private AccuracyScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = $"Тестирование метода {nameof(AccuracyScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var actual = new int[] { 2, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 2, 1, 0, 0 };
            var expected = 0.4;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана оценка!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(AccuracyScoreMetric.GetScoreCategory)}.")]
        public void GetScoreCategory_ReturnCorrectValue()
        {
            var actual = new double[] { 1, 0.9, 0.8, 0.6, 0.4 };
            var expected = new ScoreMetricCategory[]
            {
                ScoreMetricCategory.Excellent,
                ScoreMetricCategory.Good,
                ScoreMetricCategory.Satisfactory,
                ScoreMetricCategory.Bad,
                ScoreMetricCategory.Horrible
            };

            var result = new List<ScoreMetricCategory>();
            foreach (var item in actual)
            {
                var category = _scoreMetric.GetScoreCategory(item);
                result.Add(category);
            }

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитаны категории оценок!");
        }
    }
}
