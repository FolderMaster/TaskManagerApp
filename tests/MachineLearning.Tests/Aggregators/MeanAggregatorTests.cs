using MachineLearning.Aggregators;

namespace MachineLearning.Tests.Aggregators
{
    [TestFixture(Category = "Unit", TestOf = typeof(MeanAggregator),
        Description = $"Тестирование класса {nameof(MeanAggregator)}.")]
    public class MeanAggregatorTests
    {
        private MeanAggregator _aggregator;

        [SetUp]
        public void Setup()
        {
            _aggregator = new();
        }

        [Test(Description = $"Тестирование метода {nameof(MeanAggregator.AggregateToValue)}.")]
        public void AggregateToValue_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = 0.625;

            var result = _aggregator.AggregateToValue(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно агрегированы значения!");
        }
    }
}
