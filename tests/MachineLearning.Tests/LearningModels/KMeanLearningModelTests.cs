using MachineLearning.LearningModels;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.LearningModels
{
    [TestFixture(Category = "Unit", TestOf = typeof(KMeanLearningModel),
        Description = $"Тестирование класса {nameof(KMeanLearningModel)}.")]
    public class KMeanLearningModelTests
    {
        private KMeanLearningModel _learningModel;

        private AdjustedRandIndexScoreMetric _scoreMetric;

        [SetUp]
        public void Setup()
        {
            _learningModel = new();
            _scoreMetric = new();
        }

        [Test(Description = $"Тестирование метода {nameof(KMeanLearningModel.Predict)}.")]
        public void Predict_ReturnCorrectArray()
        {
            var numbersOfClusters = 2;
            var trainData = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10]
            };
            var testData = new double[][]
            {
                [7, 6],
                [-1, 0]
            };
            var actualClusters = new int[] { 1, 0 };
            var expected = 1;

            _learningModel.NumbersOfClusters = numbersOfClusters;
            _learningModel.Train(trainData);
            var predictedClusters = _learningModel.Predict(testData);
            var result = _scoreMetric.CalculateScore(actualClusters, predictedClusters);

            Assert.That(result, Is.EqualTo(expected), "Неправильно предсказанные значения!");
        }
    }
}
