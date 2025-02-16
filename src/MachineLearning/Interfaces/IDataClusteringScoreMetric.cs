using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения кластеризации на данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IDataUnsupervisedScoreMetric{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IDataClusteringScoreMetric :
        IDataUnsupervisedScoreMetric<int, IEnumerable<double>> { }
}
