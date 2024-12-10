using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения кластеризации.
    /// Наследует <see cref="IUnsupervisedScoreMetric{int, IEnumerable{double}}"/>.
    /// </summary>
    public interface IClusteringScoreMetric :
        IUnsupervisedScoreMetric<int, IEnumerable<double>> { }
}
