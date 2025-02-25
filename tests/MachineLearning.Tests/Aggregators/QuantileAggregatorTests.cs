using Common.Tests;
using MachineLearning.Aggregators;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.Aggregators
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Fast)]
    [TestFixture(TestOf = typeof(QuantileAggregator),
        Description = $"Тестирование класса {nameof(QuantileAggregator)}.")]
    public class QuantileAggregatorTests
    {
        private static double _probability = 0.3;

        private QuantileAggregator _aggregator;

        [SetUp]
        public void Setup()
        {
            _aggregator = new(_probability);
        }

        [Test(Description = $"Тестирование метода {nameof(QuantileAggregator.AggregateToValue)}.")]
        public void AggregateToValue_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = -2.35;
            var tolerance = 0.01;

            var result = _aggregator.AggregateToValue(data);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance), "Неправильно агрегированы значения!");
        }
    }
}