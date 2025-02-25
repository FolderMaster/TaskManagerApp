using Common.Tests;
using MachineLearning.ScoreMetrics;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.ScoreMetrics
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(RSquaredScoreMetric),
        Description = $"Тестирование класса {nameof(RSquaredScoreMetric)}.")]
    public class RSquaredScoreMetricTests
    {
        private RSquaredScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = $"Тестирование метода {nameof(RSquaredScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var actual = new double[] { 10, -20, 0, 3, 100 };
            var predicted = new double[] { 11, -19, 0, -2, 110 };
            var expected = 0.98;
            var tolerance = 0.01;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно расчитана оценка!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(AccuracyScoreMetric.GetScoreCategory)}.")]
        public void GetScoreCategory_ReturnCorrectValue()
        {
            var actual = new double[] { 1, 0.85, 0.7, 0.5, 0.2 };
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
