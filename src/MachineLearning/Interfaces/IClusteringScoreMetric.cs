using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения кластеризации.
    /// </summary>
    public interface IClusteringScoreMetric :
        IUnsupervisedScoreMetric<int, double> { }
}
