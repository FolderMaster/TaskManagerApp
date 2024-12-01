using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Category = "Unit", TestOf = typeof(DuplicatesRowProcessor),
        Description = $"Тестирование класса {nameof(DuplicatesRowProcessor)}.")]
    public class DuplicatesRowProcessorTests
    {
        private DuplicatesRowProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = $"Тестирование метода {nameof(DuplicatesRowProcessor.Process)}.")]
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
