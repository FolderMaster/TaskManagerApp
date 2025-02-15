using MachineLearning.Aggregators;

namespace MachineLearning.Tests.Aggregators
{
    [TestFixture(Category = "Unit", TestOf = typeof(QuantileAggregator),
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