using MachineLearning.Interfaces;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения регрессии методом кросс-валидации.
    /// </summary>
    /// <remarks>
    /// Наследует
    /// <see cref="BaseSupervisedCrossValidationEvaluator{IEnumerable{double}, double}"/>.
    /// Реализует <see cref="IRegressionEvaluator"/>.
    /// </remarks>
    public class RegressionCrossValidationEvaluator :
        BaseSupervisedCrossValidationEvaluator<IEnumerable<double>, double>, IRegressionEvaluator
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="RegressionCrossValidationEvaluator"/> по умолчанию.
        /// </summary>
        public RegressionCrossValidationEvaluator()
        {
            ScoreMetric = new SmapeScoreMetric();
        }

        /// <inheritdoc />
        protected internal override IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<IEnumerable<double>> data, IEnumerable<double> targets)
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
