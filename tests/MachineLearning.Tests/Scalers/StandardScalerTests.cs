using Common.Tests;
using MachineLearning.Scalers;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.Scalers
{
    [Level(TestLevel.Unit)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(StandardScaler),
        Description = $"Тестирование класса {nameof(StandardScaler)}.")]
    public class StandardScalerTests
    {
        private StandardScaler _scaler;

        [SetUp]
        public void Setup()
        {
            _scaler = new();
        }

        [Test(Description = $"Тестирование метода {nameof(StandardScaler.FitTransform)}.")]
        public void FitTransform_ReturnCorrectArray()
        {
            var array = new double[] { -10, 0, 10 };
            var expected = new double[] { -1.22, 0, 1.22 };
            var tolerance = 0.01;

            var result = _scaler.FitTransform(array);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance), "Неправильная нормализация данных!");
        }

        [Test(Description = $"Тестирование метода {nameof(StandardScaler.Transform)}.")]
        public void Transform_ReturnCorrectValue()
        {
            var array = new double[] { -10, 0, 10 };
            var value = 5;
            var expected = 0.61;
            var tolerance = 0.01;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance), "Неправильная нормализация значения!");
        }
    }
}
