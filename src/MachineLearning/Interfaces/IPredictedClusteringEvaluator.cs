using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения кластеризации на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IPredictedUnsupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IPredictedClusteringEvaluator :
        IPredictedUnsupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
