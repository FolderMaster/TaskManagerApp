using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения кластеризации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedScoreMetric{int, IEnumerable{double}}"/>.
    /// </remarks>
    public interface IClusteringScoreMetric :
        IUnsupervisedScoreMetric<int, IEnumerable<double>> { }
}
