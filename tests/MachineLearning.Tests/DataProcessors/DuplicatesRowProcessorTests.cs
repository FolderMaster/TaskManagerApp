using Common.Tests;
using MachineLearning.DataProcessors;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.DataProcessors
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(DuplicatesRowProcessor),
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
            var expected = new DataProcessorResult<IEnumerable<double>>
                ([
                    [10, 2],
                    [0, 3]
                ], removedRowsIndices: [2]);

            var result = _dataProcessor.Process(data);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(expected.Result),
                    "Неправильно удалены дублированные строки!");
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
