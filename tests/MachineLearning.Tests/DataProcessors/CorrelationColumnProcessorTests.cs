using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(CorrelationColumnProcessor))]
    public class CorrelationColumnProcessorTests
    {
        private CorrelationColumnProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = "Тестирование Process.")]
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
