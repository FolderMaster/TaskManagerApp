using MachineLearning.Converters;
using MachineLearning.DataProcessors;
using MachineLearning.Interfaces;

namespace MachineLearning.Tests.Converters
{
    [TestFixture(Category = "Integration", TestOf = typeof(BaseSupervisedLearningConverter
        <double, IEnumerable<double?>, IEnumerable<double?>, double>),
        Description = $"Тестирование класса {nameof(BaseSupervisedLearningConverter
            <double, IEnumerable<double?>, IEnumerable<double?>, double>)}.")]
    public class BaseSupervisedLearningConverterTests
    {
        private SupervisedLearningConverterPrototype _dataProcessor;

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

        [Test(Description = "Тестирование метода " +
            $"{nameof(SupervisedLearningConverterPrototype.NormalizeRemovedIndices)}.")]
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

        [Test(Description = "Тестирование метода " +
            $"{nameof(SupervisedLearningConverterPrototype.FitConvertData)}.")]
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
            var expected = new LearningModelData<IEnumerable<double>, double>([
                [0, 1, 0],
                [0.5, 0.33, 0],
                [0.5, 0.33, 1],
                [1, 0, 0]
            ], [0, 1, 2, 3]);

            var result = _dataProcessor.FitConvertData(data);

            Assert.Multiple(() =>
            {
                Assert.That(result.Data, Is.EqualTo(expected.Data).Within(tolerance),
                    "Неправильно сконвертированы данные!");
                Assert.That(result.Targets, Is.EqualTo(expected.Targets),
                    "Неправильно сконвертированы целевые значения!");
            });
        }
    }
}
