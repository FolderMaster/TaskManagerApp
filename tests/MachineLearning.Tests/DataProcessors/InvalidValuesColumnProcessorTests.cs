using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(InvalidValuesColumnProcessor))]
    public class InvalidValuesColumnProcessorTests
    {
        private InvalidValuesColumnProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = "Тестирование Process.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double?[][] {
                [0, double.NaN, -7],
                [double.NaN, 3, 5],
                [null, 2, null]
            };
            var expected = new double[][] {
                [0, 2.5, -7],
                [0, 3, 5],
                [0, 2, -1]
            };

            var result = _dataProcessor.Process(data);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно исправлены некорректные значения!");
        }
    }
}
