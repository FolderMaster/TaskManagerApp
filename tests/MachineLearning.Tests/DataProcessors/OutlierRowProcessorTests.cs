using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(OutlierRowProcessor))]
    public class OutlierRowProcessorTests
    {
        private OutlierRowProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = "Тестирование Process.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double[][] {
                [0, -10],
                [10, -10000],
                [10000, 0],
                [10, -200],
                [-10, 20],
                [300, -10]
            };
            var expected = new double[][] {
                [0, -10],
                [10, -200],
                [-10, 20],
                [300, -10]
            };

            var result = _dataProcessor.Process(data);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно удалены строки с выбросами!");
        }
    }
}
