using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Category = "Unit", TestOf = typeof(CorrelationColumnProcessor),
        Description = $"Тестирование класса {nameof(CorrelationColumnProcessor)}.")]
    public class CorrelationColumnProcessorTests
    {
        private CorrelationColumnProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = $"Тестирование метода {nameof(CorrelationColumnProcessor.Process)}.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double[][] {
                [-10, -10],
                [0, 0],
                [10, 10]
            };
            var expected = new double[][] {
                [-10],
                [0],
                [10]
            };

            var result = _dataProcessor.Process(data);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно удалены скоррелированные столбцы!");
        }
    }
}
