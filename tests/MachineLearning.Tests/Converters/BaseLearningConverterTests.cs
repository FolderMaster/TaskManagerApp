using Common.Tests;
using MachineLearning.Converters;
using MachineLearning.DataProcessors;
using MachineLearning.Interfaces;
using MachineLearning.Scalers;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.Converters
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Fast)]
    [TestFixture(TestOf = typeof(BaseLearningConverter<double, IEnumerable<double?>,
        IEnumerable<double?>, double>),
        Description = $"Тестирование класса {nameof(BaseLearningConverter
            <double, IEnumerable<double?>, IEnumerable<double?>, double>)}.")]
    public class BaseLearningConverterTests
    {
        private LearningConverterPrototype _dataProcessor;

        [SetUp]
        public void Setup()
        {
            var processor = new InvalidValuesColumnProcessor();
            _dataProcessor = new(processor);
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(LearningConverterPrototype.ConvertData)}.")]
        public void ConvertData_ReturnCorrectData()
        {
            var scaler1 = new MinMaxScaler();
            scaler1.FitTransform([0, 1]);
            var scaler2 = new MinMaxScaler();
            scaler2.FitTransform([-2, -1]);
            var scaler3 = new MinMaxScaler();
            scaler3.FitTransform([0, 1]);
            var scaler4 = new MinMaxScaler();
            scaler4.FitTransform([4, 6]);
            var scalers = new IScaler[] { scaler1, scaler2, scaler3, scaler4 };
            var removedColumnsIndices = new int[] { 3, 4 };
            var data = new double?[] { null, double.NaN, 2, 3, 4, 5 };
            var expected = new double[] { 0, 1, 1, 0.5 };

            _dataProcessor.Scalers = scalers;
            _dataProcessor.RemovedColumnsIndices = removedColumnsIndices;
            var result = _dataProcessor.ConvertData(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно конвертированы данные!");
        }
    }
}
