using MachineLearning.DataProcessors;

namespace MachineLearning.Tests.DataProcessors
{
    [TestFixture(Category = "Unit", TestOf = typeof(LowVariationColumnProcessor),
        Description = $"Тестирование класса {nameof(LowVariationColumnProcessor)}.")]
    public class LowVariationColumnProcessorTests
    {
        private LowVariationColumnProcessor _dataProcessor;

        [SetUp]
        public void Setup()
        {
            _dataProcessor = new();
        }

        [Test(Description = $"Тестирование метода {nameof(LowVariationColumnProcessor.Process)}.")]
        public void Process_ReturnCorrectData()
        {
            var data = new double[][] {
                [0, 0.4, -7],
                [0.1, 1, 5],
                [0, -1, 4],
                [0, 0, 2],
                [0, 0.5, -5]
            };
            var expected = new double[][] {
                [0.4, -7],
                [1, 5],
                [-1, 4],
                [0, 2],
                [0.5, -5]
            };

            var result = _dataProcessor.Process(data);

            Assert.That(result, Is.EqualTo(expected),
                "Неправильно удалены столбцы с низкой вариацией!");
        }
    }
}
