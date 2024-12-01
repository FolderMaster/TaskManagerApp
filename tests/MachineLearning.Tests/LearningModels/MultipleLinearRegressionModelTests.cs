using MachineLearning.LearningModels;

namespace MachineLearning.Tests.LearningModels
{
    [TestFixture(Category = "Unit", TestOf = typeof(MultipleLinearRegressionModel),
        Description = $"Тестирование класса {nameof(MultipleLinearRegressionModel)}.")]
    public class MultipleLinearRegressionModelTests
    {
        private MultipleLinearRegressionModel _learningModel;

        [SetUp]
        public void Setup()
        {
            _learningModel = new();
        }

        [Test(Description = "Тестирование метода " +
            $"{nameof(MultipleLinearRegressionModel.Predict)}.")]
        public void Predict_ReturnCorrectArray()
        {
            var trainData = new double[][]
            {
                [1, 3],
                [2, 4],
                [-1, 2],
                [3, -5]
            };
            var trainValues = new double[] { 13, 24, -8, 25 };
            var testData = new double[][]
            {
                [2, 1],
                [-1, 10]
            };
            var expected = new double[] { 21, 0 };
            var tolerance = 0.01;

            _learningModel.Train(trainData, trainValues);
            var result = _learningModel.Predict(testData);

            Assert.That(result, Is.EqualTo(expected).Within(tolerance),
                "Неправильно предсказанные значения!");
        }
    }
}
