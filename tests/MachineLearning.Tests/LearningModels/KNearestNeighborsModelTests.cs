using Common.Tests;
using MachineLearning.LearningModels;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.LearningModels
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Instant)]
    [TestFixture(TestOf = typeof(KNearestNeighborsModel),
        Description = $"Тестирование класса {nameof(KNearestNeighborsModel)}.")]
    public class KNearestNeighborsModelTests
    {
        private KNearestNeighborsModel _learningModel;

        [SetUp]
        public void Setup()
        {
            _learningModel = new();
        }

        [Test(Description = $"Тестирование метода {nameof(KNearestNeighborsModel.Predict)}.")]
        public void Predict_ReturnCorrectArray()
        {
            var numbersOfNeighbors = 2;
            var trainData = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10]
            };
            var trainClass = new int[] { 0, 0, 1, 1 };
            var testData = new double[][]
            {
                [7, 6],
                [-1, 0]
            };
            var expected = new double[] { 1, 0 };

            _learningModel.NumberOfNeighbors = numbersOfNeighbors;
            _learningModel.Train(trainData, trainClass);
            var result = _learningModel.Predict(testData);

            Assert.That(result, Is.EqualTo(expected), "Неправильно предсказанные значения!");
        }
    }
}
