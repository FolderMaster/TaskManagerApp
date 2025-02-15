using MachineLearning.DistanceMetrics;

namespace MachineLearning.Tests.DistanceMetrics
{
    [TestFixture(Category = "Unit", TestOf = typeof(ManhattanDistanceMetric),
        Description = $"Тестирование класса {nameof(ManhattanDistanceMetric)}.")]
    class ManhattanDistanceMetricTests
    {
        private ManhattanDistanceMetric _distanceMetric;

        [SetUp]
        public void Setup()
        {
            _distanceMetric = new();
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(ManhattanDistanceMetric.CalculateDistance)}.")]
        public void CalculateDistance_ReturnCorrectValue()
        {
            var point1 = new double[] { 1, -1 };
            var point2 = new double[] { -2, 3 };
            var expected = 7;

            var result = _distanceMetric.CalculateDistance(point1, point2);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана дистанция!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(ManhattanDistanceMetric.CalculateDistance)} при одинаковых точках.")]
        public void CalculateDistance_SamePoints_Return0()
        {
            var point1 = new double[] { -10, 0, 10 };
            var point2 = new double[] { -10, 0, 10 };
            var expected = 0;

            var result = _distanceMetric.CalculateDistance(point1, point2);

            Assert.That(result, Is.EqualTo(expected), "Дистанция должна быть равна 0!");
        }
    }
}
