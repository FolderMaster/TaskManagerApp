using Common.Tests;
using MachineLearning.Converters;
using MachineLearning.DataProcessors;
using MachineLearning.Interfaces;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.Converters
{
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [TestFixture(TestOf = typeof(BaseUnsupervisedLearningConverter
        <double, IEnumerable<double?>, IEnumerable<double?>, double>),
        Description = $"Тестирование класса {nameof(BaseUnsupervisedLearningConverter
            <double, IEnumerable<double?>, IEnumerable<double?>, double>)}.")]
    public class BaseUnsupervisedLearningConverterTests
    {
        private FakeUnsupervisedLearningConverter _dataProcessor;

        [SetUp]
        public void Setup()
        {
            var primaryProcessor = new InvalidValuesColumnProcessor();
            var processors = new IPointDataProcessor[]
            {
                new DuplicatesRowProcessor(),
                new OutlierRowProcessor(),
                new CorrelationColumnProcessor(),
                new LowVariationColumnProcessor()
            };
            _dataProcessor = new(primaryProcessor, processors);
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(FakeSupervisedLearningConverter.NormalizeRemovedIndices)}.")]
        public void NormalizeRemovedIndices_ReturnCorrectData()
        {
            var removedIndicesGroups = new int[][] {
                [1, 3],
                [0, 2]
            };
            var expected = new int[] { 0, 1, 3, 4 };

            var result = _dataProcessor.NormalizeRemovedIndices(removedIndicesGroups);

            Assert.That(result, Is.EqualTo(expected), "Неправильно нормализованы индексы!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [Test(Description = "Тестирование метода " +
            $"{nameof(FakeSupervisedLearningConverter.FitConvertData)}.")]
        public void FitConvertData_ReturnCorrectData()
        {
            var data = new double?[][] {
                [-10, -10, double.NaN, 7, 0, 0, 1],
                [0, 0, double.NaN, null, 1e-200, 0, 2],
                [0, 0, null, 1, 0, 1, 3],
                [10, 10, null, -2, 0, 0, 4],
                [10, 10, null, -2, 0, 0, 5]
            };
            var tolerance = 0.01;
            var expected = new double[][]
            {
                [0, 1, 0],
                [0.5, 0.33, 0],
                [0.5, 0.33, 1],
                [1, 0, 0]
            };

            var result = _dataProcessor.FitConvertData(data);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно сконвертированы данные!");
        }
    }
}
