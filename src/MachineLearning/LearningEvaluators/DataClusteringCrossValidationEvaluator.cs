using MachineLearning.Interfaces;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения кластеризации методом кросс-валидации на данных.
    /// </summary>
    /// <remarks>
    /// Наследует
    /// <see cref="BaseDataUnsupervisedCrossValidationEvaluator{IEnumerable{double}, int}"/>.
    /// Реализует <see cref="IDataClusteringEvaluator"/>.
    /// </remarks>
    public class DataClusteringCrossValidationEvaluator :
        BaseDataUnsupervisedCrossValidationEvaluator<IEnumerable<double>, int>,
        IDataClusteringEvaluator
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="DataClusteringCrossValidationEvaluator"/>
        /// по умолчанию.
        /// </summary>
        public DataClusteringCrossValidationEvaluator()
        {
            ScoreMetric = new SilhouetteScoreMetric();
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
    }
}
