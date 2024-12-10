using MachineLearning.Interfaces;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения регрессии методом кросс-валидации.
    /// Наследует <see cref="BaseSupervisedCrossValidationEvaluator{IEnumerable{double}, double}"/>.
    /// Реализует <see cref="IRegressionEvaluator"/>.
    /// </summary>
    public class RegressionCrossValidationEvaluator :
        BaseSupervisedCrossValidationEvaluator<IEnumerable<double>, double>, IRegressionEvaluator
    {
        /// <inheritdoc />
        protected override IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<IEnumerable<double>> data, IEnumerable<double> targets)
        {
            var indices = Enumerable.Range(0, targets.Count());
            var foldSize = targets.Count() / NumberOfFolds;
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
