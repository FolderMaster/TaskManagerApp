using MachineLearning.Aggregators;

namespace MachineLearning.Tests.Aggregators
{
    [TestFixture(Category = "Unit", TestOf = typeof(ModeAggregator),
        Description = $"Тестирование класса {nameof(ModeAggregator)}.")]
    public class ModeAggregatorTests
    {
        private ModeAggregator _aggregator;

        [SetUp]
        public void Setup()
        {
            _aggregator = new();
        }

        [Test(Description = $"Тестирование метода {nameof(ModeAggregator.AggregateToValue)}.")]
        public void AggregateToValue_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = 0.5;

            var result = _aggregator.AggregateToValue(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно агрегированы значения!");
        }

        [Test(Description = $"Тестирование метода {nameof(ModeAggregator.AggregateToGroup)}.")]
        public void AggregateToGroup_ReturnCorrectValue()
        {
            var data = new double[] { -1, 7, 3.5, 0, -2.5, -3, 3.5, -2.5 };
            var expected = new double[] { -2.5, 3.5 };

            var result = _aggregator.AggregateToGroup(data);

            Assert.That(result, Is.EquivalentTo(expected), "Неправильно агрегированы значения!");
        }
    }
}
