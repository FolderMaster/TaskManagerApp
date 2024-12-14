using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(AdjustedRandIndexScoreMetric),
        Description = $"Тестирование класса {nameof(AdjustedRandIndexScoreMetric)}.")]
    public class AdjustedRandIndexScoreMetricTests
    {
        private AdjustedRandIndexScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _scoreMetric = new();
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(AdjustedRandIndexScoreMetric.CalculateScore)}.")]
        public void CalculateScore_ReturnCorrectValue()
        {
            var actual = new int[] { 2, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 2, 1, 0, 0 };
            var expected = -0.25;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно расчитана оценка!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(AdjustedRandIndexScoreMetric.CalculateScore)} " +
            "при инверсированных предсказанных значениях.")]
        public void CalculateScore_InversedPredictedValues_Return1()
        {
            var actual = new int[] { 2, 0, 1, 1, 0 };
            var predicted = new int[] { 1, 2, 0, 0, 2 };
            var expected = 1;

            var result = _scoreMetric.CalculateScore(actual, predicted);

            Assert.That(result, Is.EqualTo(expected),
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
