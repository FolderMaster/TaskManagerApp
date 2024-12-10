using MachineLearning.Interfaces;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения кластеризации методом кросс-валидации.
    /// Наследует <see cref="BaseUnsupervisedCrossValidationEvaluator{IEnumerable{double}, int}"/>.
    /// Реализует <see cref="IClusteringEvaluator"/>.
    /// </summary>
    public class ClusteringCrossValidationEvaluator :
        BaseUnsupervisedCrossValidationEvaluator<IEnumerable<double>, int>, IClusteringEvaluator
    {
        /// <inheritdoc />
        protected override IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<IEnumerable<double>> data)
        {
            var indices = Enumerable.Range(0, data.Count());
            var foldSize = data.Count() / NumberOfFolds;
            for (var i = 0; i < NumberOfFolds; ++i)
            {
                var testIndices = indices.Skip(i * foldSize).Take(foldSize);
                var trainIndices = indices.Except(testIndices);

                yield return new ValidationFold
                {
                    TestIndices = trainIndices,
                    TrainIndices = trainIndices
                };
            }
        }
    }
}
