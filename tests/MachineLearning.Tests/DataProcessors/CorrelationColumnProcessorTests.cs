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
            var expected = new DataProcessorResult<IEnumerable<double>>
                ([
                    [-10],
                    [0],
                    [10]
                ], removedColumnsIndices: [1]);

            var result = _dataProcessor.Process(data);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(expected.Result),
                    "Неправильно удалены скоррелированные столбцы!");
                Assert.That(result.RemovedColumnsIndices,
                    Is.EqualTo(expected.RemovedColumnsIndices),
                    "Неправильно указаны удалённые столбцы!");
                Assert.That(result.RemovedRowsIndices,
                    Is.EqualTo(expected.RemovedRowsIndices),
                    "Неправильно указаны удалённые строки!");
            });
        }
    }
}
