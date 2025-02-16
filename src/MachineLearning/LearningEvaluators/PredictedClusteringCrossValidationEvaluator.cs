using MachineLearning.Interfaces;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения кластеризации методом кросс-валидации на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует
    /// <see cref="BaseDataUnsupervisedCrossValidationEvaluator{IEnumerable{double}, int}"/>.
    /// Реализует <see cref="IPredictedClusteringEvaluator"/>.
    /// </remarks>
    public class PredictedClusteringCrossValidationEvaluator :
        BasePredictedUnsupervisedCrossValidationEvaluator<IEnumerable<double>, int>,
        IPredictedClusteringEvaluator
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="DataClusteringCrossValidationEvaluator"/>
        /// по умолчанию.
        /// </summary>
        public PredictedClusteringCrossValidationEvaluator()
        {
            ScoreMetric = new AdjustedRandIndexScoreMetric();
        }

        /// <inheritdoc />
        protected override IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<IEnumerable<double>> data)
        {
            var count = data.Count();
            var indices = Enumerable.Range(0, count);
            var foldSize = count / NumberOfFolds;
            for (var i = 0; i < NumberOfFolds; ++i)
            {
                var testIndices = indices.Where
                    (index => index >= i * foldSize && index < (i + 1) * foldSize);
                var trainIndices = indices.Where
                    (index => index < i * foldSize || index >= (i + 1) * foldSize);

                yield return new ValidationFold(trainIndices, testIndices);
            }
        }

        /// <inheritdoc />
        protected override IEnumerable<IEnumerable<int>> GetSecondaryTrainIndices
            (IEnumerable<int> trainIndices)
        {
            var count = trainIndices.Count();
            var foldSize = count / NumberOfFolds;
            for (var i = 0; i < NumberOfFolds; ++i)
            {
                yield return trainIndices.Where
                    (index => index < i * foldSize || index >= (i + 1) * foldSize);
            }
        }
    }
}
