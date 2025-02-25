using Common.Tests;
using MachineLearning.DistanceMetrics;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.DistanceMetrics
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(ChebyshevDistanceMetric),
        Description = $"Тестирование класса {nameof(ChebyshevDistanceMetric)}.")]
    public class ChebyshevDistanceMetricTests
    {
        private ChebyshevDistanceMetric _distanceMetric;

        [SetUp]
        public void Setup()
        {
            _distanceMetric = new();
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(ChebyshevDistanceMetric.CalculateDistance)}.")]
        public void CalculateDistance_ReturnCorrectValue()
        {
            var point1 = new double[] { 1, -1 };
            var point2 = new double[] { -2, 3 };
            var expected = 4;

            var result = _distanceMetric.CalculateDistance(point1, point2);

            Assert.That(result, Is.EqualTo(expected), "Неправильно расчитана дистанция!");
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(ChebyshevDistanceMetric.CalculateDistance)} при одинаковых точках.")]
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
