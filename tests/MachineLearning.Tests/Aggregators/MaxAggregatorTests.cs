using MachineLearning.Aggregators;

namespace MachineLearning.Tests.Aggregators
{
    [TestFixture(Category = "Unit", TestOf = typeof(MaxAggregator),
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
