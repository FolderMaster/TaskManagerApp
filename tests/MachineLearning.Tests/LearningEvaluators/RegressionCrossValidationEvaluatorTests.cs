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
    [TestFixture(TestOf = typeof(RegressionCrossValidationEvaluator),
        Description = $"Тестирование класса {nameof(RegressionCrossValidationEvaluator)}.")]
    public class RegressionCrossValidationEvaluatorTests
    {
        private RegressionCrossValidationEvaluatorPrototype _learningEvaluator;

        [SetUp]
        public void Setup()
        {
            _learningEvaluator = new();
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(RegressionCrossValidationEvaluatorPrototype.GetValidationFoldsSet)}.")]
        public void GetValidationFoldsSet_ReturnCorrectValues()
        {
            var numberOfFolds = 3;
            var data = new double[][]
            {
                [1, 3],
                [2, 4],
                [-1, 2],
                [3, -5],
                [2, 1],
                [-1, 10]
            };
            var values = new double[] { 13, 24, -8, 25, 21, 0 };
            var expected = new ValidationFold[]
            {
                new ValidationFold([2, 3, 4, 5], [0, 1]),
                new ValidationFold([0, 1, 4, 5], [2, 3]),
                new ValidationFold([0, 1, 2, 3], [4, 5])
            };

            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = _learningEvaluator.GetValidationFoldsSet(data, values);

            Assert.That(result, Is.EqualTo(expected).Using(new ValidationFoldComparer()),
                "Неправильно построены сегменты валидации!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(RegressionCrossValidationEvaluator.Evaluate)} " +
            "с корректными данными и классами.")]
        public async Task Evaluate_CorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new MultipleLinearRegressionModel();
            var scoreMetric = new RSquaredScoreMetric();
            var data = new double[][]
            {
                [1, 3],
                [2, 4],
                [-1, 2],
                [3, -5],
                [2, 1],
                [-1, 10]
            };
            var classes = new double[] { 13, 24, -8, 25, 21, 0 };
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
            $"{nameof(RegressionCrossValidationEvaluator.Evaluate)} " +
            "с некорректными данными и классами.")]
        public async Task Evaluate_IncorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new MultipleLinearRegressionModel();
            var scoreMetric = new RSquaredScoreMetric();
            var data = new double[][]
            {
                [1, 3],
                [2, 4],
                [-1, 2],
                [3, -5],
                [2, 1],
                [-1, 10]
            };
            var classes = new double[] { 0, 54, -1, 14, -24, 0 };
            var expected = ScoreMetricCategory.Horrible;

            _learningEvaluator.Model = learningModel;
            _learningEvaluator.ScoreMetric = scoreMetric;
            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = await _learningEvaluator.Evaluate(data, classes);

            Assert.That(result, Is.EqualTo(expected), "Неправильно поставлена категория оценки!");
        }
    }
}
