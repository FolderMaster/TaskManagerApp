using MachineLearning.Aggregators;

namespace MachineLearning.Tests.Aggregators
{
    [TestFixture(Category = "Unit", TestOf = typeof(MinAggregator),
        Description = $"Тестирование класса {nameof(MinAggregator)}.")]
    public class MinAggregatorTests
    {
        private MinAggregator _aggregator;

        [SetUp]
        public void Setup()
        {
            _aggregator = new();
        }

        [Test(Description = $"Тестирование метода {nameof(MinAggregator.AggregateToValue)}.")]
        public void AggregateToValue_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = -3;

            var result = _aggregator.AggregateToValue(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно агрегированы значения!");
        }
    }
}
