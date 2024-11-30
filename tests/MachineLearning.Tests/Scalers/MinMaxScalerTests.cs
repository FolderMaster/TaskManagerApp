using MachineLearning.Scalers;

namespace MachineLearning.Tests.Scalers
{
    [TestFixture(Description = "Тестирование класса.", Category = "Unit",
        TestOf = typeof(MinMaxScaler))]
    public class MinMaxScalerTests
    {
        private MinMaxScaler _scaler;

        [SetUp]
        public void Setup()
        {
            _scaler = new();
        }

        [Test(Description = "Тестирование FitTransform.")]
        public void FitTransform_ReturnCorrectArray()
        {
            var array = new double[] { -10, 0, 10};
            var expected = new double[] { 0, 0.5, 1 };

            var result = _scaler.FitTransform(array);

            Assert.That(result, Is.EqualTo(expected), "Неправильная нормализация данных!");
        }

        [Test(Description = "Тестирование Transform.")]
        public void Transform_ReturnCorrectValue()
        {
            var array = new double[] { -10, 0, 10 };
            var value = 5;
            var expected = 0.75;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected), "Неправильная нормализация значения!");
        }

        [Test(Description = "Тестирование Transform при значении больше максимума.")]
        public void Transform_MoreThanMax_Return1()
        {
            var array = new double[] { -10, 0, 10 };
            var value = 20;
            var expected = 1;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected),
                "При значении больше максимума должно возвращаться 1!");
        }

        [Test(Description = "Тестирование Transform при значении меньше минимума.")]
        public void Transform_LessThanMin_Return0()
        {
            var array = new double[] { -10, 0, 10 };
            var value = -20;
            var expected = 0;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected),
                "При значении меньше минимума должно возвращаться 0!");
        }
    }
}