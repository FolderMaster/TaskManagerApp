using MachineLearning.LearningEvaluators;
using MachineLearning.LearningModels;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.LearningEvaluators
{
    [TestFixture(TestOf = typeof(ClassificationCrossValidationEvaluator),
        Description = $"Тестирование класса {nameof(ClassificationCrossValidationEvaluator)}.")]
    public class ClassificationCrossValidationEvaluatorTests
    {
        private ClassificationCrossValidationEvaluatorPrototype _learningEvaluator;

        [SetUp]
        public void Setup()
        {
            _learningEvaluator = new();
        }

        [TestCase(Category = "Unit", Description = "Тестирование метода " +
            $"{nameof(ClassificationCrossValidationEvaluatorPrototype.GetValidationFoldsSet)}.")]
        public void GetValidationFoldsSet_ReturnCorrectValues()
        {
            var numberOfFolds = 2;
            var data = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10]
            };
            var classes = new int[] { 0, 0, 1, 1 };
            var expected = new ValidationFold[]
            {
                new ValidationFold([1, 3], [0, 2]),
                new ValidationFold([0, 2], [1, 3])
            };

            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = _learningEvaluator.GetValidationFoldsSet(data, classes);

            Assert.That(result, Is.EqualTo(expected).Using(new ValidationFoldComparer()),
                "Неправильно построены сегменты валидации!");
        }

        [TestCase(Category = "Integration", Description = "Тестирование метода " +
            $"{nameof(ClassificationCrossValidationEvaluator.Evaluate)} " +
            "с корректными данными и классами.")]
        public async Task Evaluate_CorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new KNearestNeighborsModel()
            {
                NumberOfNeighbors = 2
            };
            var scoreMetric = new AccuracyScoreMetric();
            var data = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10],
                [7, 6],
                [-1, 0]
            };
            var classes = new int[] { 0, 0, 1, 1, 1, 0 };
            var expected = ScoreMetricCategory.Excellent;

            _learningEvaluator.Model = learningModel;
            _learningEvaluator.ScoreMetric = scoreMetric;
            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = await _learningEvaluator.Evaluate(data, classes);

            Assert.That(result, Is.EqualTo(expected), "Неправильно поставлена категория оценки!");
        }

        [TestCase(Category = "Integration", Description = "Тестирование метода " +
            $"{nameof(ClassificationCrossValidationEvaluator.Evaluate)} " +
            "с некорректными данными и классами.")]
        public async Task Evaluate_IncorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new KNearestNeighborsModel()
            {
                NumberOfNeighbors = 2
            };
            var scoreMetric = new AccuracyScoreMetric();
            var data = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10],
                [7, 6],
                [-1, 0]
            };
            var classes = new int[] { 0, 1, 2, 0, 1, 2 };
            var expected = ScoreMetricCategory.Horrible;

            _learningEvaluator.Model = learningModel;
            _learningEvaluator.ScoreMetric = scoreMetric;
            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = await _learningEvaluator.Evaluate(data, classes);

            Assert.That(result, Is.EqualTo(expected), "Неправильно поставлена категория оценки!");
        }
    }
}
