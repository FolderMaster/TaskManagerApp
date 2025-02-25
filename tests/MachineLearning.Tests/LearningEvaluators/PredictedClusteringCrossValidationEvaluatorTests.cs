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
    [TestFixture(TestOf = typeof(PredictedClusteringCrossValidationEvaluator),
        Description = "Тестирование класса " +
        $"{nameof(PredictedClusteringCrossValidationEvaluator)}.")]
    public class PredictedClusteringCrossValidationEvaluatorTests
    {
        private PredictedClusteringCrossValidationEvaluatorPrototype _learningEvaluator;

        [SetUp]
        public void Setup()
        {
            _learningEvaluator = new();
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(PredictedClusteringCrossValidationEvaluatorPrototype.GetValidationFoldsSet)}.")]
        public void GetValidationFoldsSet_ReturnCorrectValues()
        {
            var numberOfFolds = 3;
            var data = new double[][]
            {
                [1, 2],
                [2, 3],
                [8, 8],
                [9, 10],
                [7, 6],
                [-1, 0]
            };
            var expected = new ValidationFold[]
            {
                new ValidationFold([2, 3, 4, 5], [0, 1]),
                new ValidationFold([0, 1, 4, 5], [2, 3]),
                new ValidationFold([0, 1, 2, 3], [4, 5])
            };

            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = _learningEvaluator.GetValidationFoldsSet(data);

            Assert.That(result, Is.EqualTo(expected).Using(new ValidationFoldComparer()),
                "Неправильно построены сегменты валидации!");
        }

        [Level(TestLevel.Unit)]
        [Time(TestTime.Instant)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(PredictedClusteringCrossValidationEvaluatorPrototype.GetSecondaryTrainIndicesSet)}.")]
        public void GetSecondaryTrainIndicesSet_ReturnCorrectValues()
        {
            var numberOfFolds = 3;
            var indices = new int[] { 0, 1, 2, 3 };
            var expected = new int[][]
            {
                [ 1, 2, 3 ],
                [ 0, 2, 3 ],
                [ 0, 1, 3 ],
            };

            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = _learningEvaluator.GetSecondaryTrainIndicesSet(indices);

            Assert.That(result, Is.EqualTo(expected).Using(new ValidationFoldComparer()),
                "Неправильно построены вторичные индексы тренировки!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(PredictedClusteringCrossValidationEvaluator.Evaluate)} " +
            "с корректными данными и классами.")]
        public async Task Evaluate_CorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new KMeanLearningModel()
            {
                NumberOfClusters = 2
            };
            var scoreMetric = new AdjustedRandIndexScoreMetric();
            var data = new double[][]
            {
                [1, 3],
                [2, 4],
                [-1, 2],
                [3, -5],
                [2, 1],
                [-1, 10]
            };
            var expected = ScoreMetricCategory.Excellent;

            _learningEvaluator.Model = learningModel;
            _learningEvaluator.ScoreMetric = scoreMetric;
            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = await _learningEvaluator.Evaluate(data);

            Assert.That(result, Is.EqualTo(expected), "Неправильно поставлена категория оценки!");
        }

        [Level(TestLevel.Integration)]
        [Time(TestTime.Fast)]
        [TestCase(Description = "Тестирование метода " +
            $"{nameof(PredictedClusteringCrossValidationEvaluator.Evaluate)} " +
            "с некорректными данными и классами.")]
        public async Task Evaluate_IncorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 2;
            var learningModel = new KMeanLearningModel()
            {
                NumberOfClusters = 2
            };
            var scoreMetric = new AdjustedRandIndexScoreMetric();
            var data = new double[][]
            {
                [0, 0],
                [-1, 0],
                [0, 0],
                [4, -4],
                [1, 0],
                [2, 0],
                [0, 0],
                [0, 1],
                [0, 2],
                [-4, 7],
                [3, 0],
                [4, 0],
                [5, 0],
                [0, 1],
                [-5, 6],
                [0, -1],
                [0, 3],
                [0, 4],
                [0, 5],
                [10, 7]
            };
            var expected = ScoreMetricCategory.Horrible;

            _learningEvaluator.Model = learningModel;
            _learningEvaluator.ScoreMetric = scoreMetric;
            _learningEvaluator.NumberOfFolds = numberOfFolds;
            var result = await _learningEvaluator.Evaluate(data);

            Assert.That(result, Is.LessThanOrEqualTo(expected),
                "Неправильно поставлена категория оценки!");
        }
    }
}
