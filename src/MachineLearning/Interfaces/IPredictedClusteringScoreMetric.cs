using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения кластеризации на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IPredictedUnsupervisedScoreMetric{int}"/>.
    /// </remarks>
    public interface IPredictedClusteringScoreMetric : IPredictedUnsupervisedScoreMetric<int> { }
}
