using Common.Tests;
using MachineLearning.LearningEvaluators;
using MachineLearning.LearningModels;
using MachineLearning.ScoreMetrics;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace MachineLearning.Tests.LearningEvaluators
{
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.Medium)]
    [Reproducibility(ReproducibilityType.Stable)]
    [TestFixture(TestOf = typeof(ClassificationCrossValidationEvaluator),
        Description = $"Тестирование класса {nameof(ClassificationCrossValidationEvaluator)}.")]
    public class ClassificationCrossValidationEvaluatorTests
    {
        private ClassificationCrossValidationEvaluator _learningEvaluator;

        [SetUp]
        public void Setup()
        {
            _learningEvaluator = new();
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(ClassificationCrossValidationEvaluator.GetValidationFolds)}.")]
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
            var result = _learningEvaluator.GetValidationFolds(data, classes);

            Assert.That(result, Is.EqualTo(expected).Using(new ValidationFoldComparer()),
                "Неправильно построены сегменты валидации!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [TestCase(Description = "Тестирование метода " +
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

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [TestCase(Description = "Тестирование метода " +
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
