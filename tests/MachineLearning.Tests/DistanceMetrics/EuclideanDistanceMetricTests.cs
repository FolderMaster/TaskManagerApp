using MachineLearning.DistanceMetrics;

namespace MachineLearning.Tests.DistanceMetrics
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(EuclideanDistanceMetric))]
    public class EuclideanDistanceMetricTests
    {
        private EuclideanDistanceMetric _distanceMetric;

        [SetUp]
        public void Setup()
        {
            _distanceMetric = new();
        }

        [Test(Description = "Тестирование CalculateDistance.")]
        public void CalculateDistance_ReturnCorrectValue()
        {
            var point1 = new double[] { 1, -1 };
            var point2 = new double[] { -2, 3 };
            var expected = 5;

            var result = _distanceMetric.CalculateDistance(point1, point2);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана дистанция!");
        }

        [Test(Description = "Тестирование CalculateDistance при одинаковых точках.")]
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
