using MachineLearning.Scalers;

namespace MachineLearning.Tests.Scalers
{
    [TestFixture(Category = "Unit", TestOf = typeof(MinMaxScaler),
        Description = $"“естирование класса {nameof(MinMaxScaler)}.")]
    public class MinMaxScalerTests
    {
        private MinMaxScaler _scaler;

        [SetUp]
        public void Setup()
        {
            _scaler = new();
        }

        [Test(Description = $"“естирование метода {nameof(MinMaxScaler.FitTransform)}.")]
        public void FitTransform_ReturnCorrectArray()
        {
            var array = new double[] { -10, 0, 10 };
            var expected = new double[] { 0, 0.5, 1 };

            var result = _scaler.FitTransform(array);

            Assert.That(result, Is.EqualTo(expected), "Ќеправильна€ нормализаци€ данных!");
        }

        [Test(Description = $"“естирование метода {nameof(MinMaxScaler.Transform)}.")]
        public void Transform_ReturnCorrectValue()
        {
            var array = new double[] { -10, 0, 10 };
            var value = 5;
            var expected = 0.75;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected), "Ќеправильна€ нормализаци€ значени€!");
        }

        [Test(Description = $"“естирование метода {nameof(MinMaxScaler.Transform)} " +
            $"при значении больше максимума.")]
        public void Transform_MoreThanMax_Return1()
        {
            var array = new double[] { -10, 0, 10 };
            var value = 20;
            var expected = 1;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected),
                "ѕри значении больше максимума должно возвращатьс€ 1!");
        }

        [Test(Description = $"“естирование метода {nameof(MinMaxScaler.Transform)} " +
            $"при значении меньше минимума.")]
        public void Transform_LessThanMin_Return0()
        {
            var array = new double[] { -10, 0, 10 };
            var value = -20;
            var expected = 0;

            _scaler.FitTransform(array);
            var result = _scaler.Transform(value);

            Assert.That(result, Is.EqualTo(expected),
                "ѕри значении меньше минимума должно возвращатьс€ 0!");
        }
    }
}