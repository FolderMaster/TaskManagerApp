﻿using MachineLearning.LearningEvaluators;
using MachineLearning.LearningModels;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.Tests.LearningEvaluators
{
    [TestFixture(TestOf = typeof(DataClusteringCrossValidationEvaluator),
        Description = $"Тестирование класса {nameof(DataClusteringCrossValidationEvaluator)}.")]
    public class DataClusteringCrossValidationEvaluatorTests
    {
        private DataClusteringCrossValidationEvaluatorPrototype _learningEvaluator;

        [SetUp]
        public void Setup()
        {
            _learningEvaluator = new();
        }

        [TestCase(Category = "Unit", Description = "Тестирование метода " +
            $"{nameof(DataClusteringCrossValidationEvaluatorPrototype.GetValidationFoldsSet)}.")]
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

        [TestCase(Category = "Integration", Description = "Тестирование метода " +
            $"{nameof(DataClusteringCrossValidationEvaluator.Evaluate)} " +
            "с корректными данными и классами.")]
        public async Task Evaluate_CorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 3;
            var learningModel = new KMeanLearningModel()
            {
                NumberOfClusters = 2
            };
            var scoreMetric = new SilhouetteScoreMetric();
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

        [TestCase(Category = "Integration", Description = "Тестирование метода " +
            $"{nameof(DataClusteringCrossValidationEvaluator.Evaluate)} " +
            "с некорректными данными и классами.")]
        public async Task Evaluate_IncorrectDataAndClasses_ReturnCorrectScoreCategory()
        {
            var numberOfFolds = 2;
            var learningModel = new KMeanLearningModel()
            {
                NumberOfClusters = 2
            };
            var scoreMetric = new SilhouetteScoreMetric();
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
