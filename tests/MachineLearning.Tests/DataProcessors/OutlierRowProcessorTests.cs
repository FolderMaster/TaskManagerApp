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
    [TestFixture(TestOf = typeof(OutlierRowProcessor),
        Description = $"Тестирование класса {nameof(OutlierRowProcessor)}.")]
    public class OutlierRowProcessorTests
    {
        private OutlierRowProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = $"Тестирование метода {nameof(OutlierRowProcessor.Process)}.")]
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
            var expected = new DataProcessorResult<IEnumerable<double>>
                ([
                    [0, -10],
                    [10, -200],
                    [-10, 20],
                    [300, -10]
                ], removedRowsIndices: [1, 2]);

            var result = _dataProcessor.Process(data);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(expected.Result),
                    "Неправильно удалены строки с выбросами!");
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
