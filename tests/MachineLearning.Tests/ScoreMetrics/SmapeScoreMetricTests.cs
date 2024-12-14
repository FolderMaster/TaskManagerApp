using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(SmapeScoreMetric),
        Description = $"Тестирование класса {nameof(SmapeScoreMetric)}.")]
    public class SmapeScoreMetricTests
    {
        private SmapeScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = $"Тестирование метода {nameof(SmapeScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var actual = new double[] { 10, -20, 0, 3, 100 };
            var predicted = new double[] { 11, -19, 0, -2, 110 };
            var expected = 0.448;
            var tolerance = 0.001;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно расчитана оценка!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(AccuracyScoreMetric.GetScoreCategory)}.")]
        public void GetScoreCategory_ReturnCorrectValue()
        {
            var actual = new double[] { 0, 0.15, 0.25, 0.4, 0.6 };
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
