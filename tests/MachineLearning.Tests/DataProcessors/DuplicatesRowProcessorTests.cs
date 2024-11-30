using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(DuplicatesRowProcessor))]
    public class DuplicatesRowProcessorTests
    {
        private DuplicatesRowProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = "Тестирование Process.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double[][] {
                [10, 2],
                [0, 3],
                [10, 2]
            };
            var expected = new double[][] {
                [10, 2],
                [0, 3]
            };

            var result = _dataProcessor.Process(data);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно удалены дублированные строки!");
        }
    }
}
