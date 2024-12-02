using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения классификации.
    /// </summary>
    public interface IClassificationScoreMetric : ISupervisedScoreMetric<int> { }
}
