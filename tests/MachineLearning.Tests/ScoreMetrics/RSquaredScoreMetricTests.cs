﻿using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.ScoreMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(RSquaredScoreMetric),
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
    }
}