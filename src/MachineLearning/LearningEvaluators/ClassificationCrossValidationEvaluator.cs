using MachineLearning.Interfaces;
using MachineLearning.ScoreMetrics;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Класс оценки модели обучения классификации методом кросс-валидации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseSupervisedCrossValidationEvaluator{IEnumerable{double}, int}"/>.
    /// Реализует <see cref="IRegressionEvaluator"/>.
    /// </remarks>
    public class ClassificationCrossValidationEvaluator :
        BaseSupervisedCrossValidationEvaluator<IEnumerable<double>, int>, IClassificationEvaluator
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="ClassificationCrossValidationEvaluator"/>
        /// по умолчанию.
        /// </summary>
        public ClassificationCrossValidationEvaluator()
        {
            ScoreMetric = new F1ScoreMetric();
        }

        /// <inheritdoc />
        protected internal override IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<IEnumerable<double>> data, IEnumerable<int> targets)
        {
            var groupedIndices = targets.Select((t, i) => (Target: t, Index: i)).
                GroupBy(p => p.Target).ToDictionary(g => g.Key, g => g.Select(p => p.Index));

            var folds = new List<int>[NumberOfFolds];
            for (var i = 0; i < NumberOfFolds; ++i)
            {
                folds[i] = new List<int>();
            }

            foreach (var group in groupedIndices.Values)
            {
                var foldIndex = 0;
                foreach (var index in group)
                {
                    folds[foldIndex % NumberOfFolds].Add(index);
                    ++foldIndex;
                }
            }

            for (var i = 0; i < NumberOfFolds; ++i)
            {
                var testIndices = folds[i];
                var trainIndices = folds.SelectMany
                    ((f, index) => index == i ? Enumerable.Empty<int>() : f);

                yield return new ValidationFold(trainIndices, testIndices);
            }
        }
    }
}
