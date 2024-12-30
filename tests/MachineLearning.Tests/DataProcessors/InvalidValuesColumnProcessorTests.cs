using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Category = "Unit", TestOf = typeof(InvalidValuesColumnProcessor),
        Description = $"Тестирование класса {nameof(InvalidValuesColumnProcessor)}.")]
    public class InvalidValuesColumnProcessorTests
    {
        private InvalidValuesColumnProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(InvalidValuesColumnProcessor.Process)}.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double?[][] {
                [0, double.NaN, null, -7],
                [double.NaN, 3, double.NaN, 5],
                [null, 2, null, null]
            };
            var expected = new DataProcessorResult<IEnumerable<double>>
                ([
                    [0, 2.5, -7],
                    [0, 3, 5],
                    [0, 2, -1]
                ], [2]);

            var result = _dataProcessor.Process(data);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(expected.Result),
                    "Неправильно исправлены некорректные значения!");
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
