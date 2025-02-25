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
    [TestFixture(TestOf = typeof(MaxAggregator),
        Description = $"Тестирование класса {nameof(MaxAggregator)}.")]
    public class MaxAggregatorTests
    {
        private MaxAggregator _aggregator;

        [SetUp]
        public void Setup()
        {
            _aggregator = new();
        }

        [Test(Description = $"Тестирование метода {nameof(MaxAggregator.AggregateToValue)}.")]
        public void AggregateToValue_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = 7;

            var result = _aggregator.AggregateToValue(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно агрегированы значения!");
        }
    }
}
