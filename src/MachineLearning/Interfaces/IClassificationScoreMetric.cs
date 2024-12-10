using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения классификации.
    /// Наследует <see cref="ISupervisedScoreMetric{int}"/>.
    /// </summary>
    public interface IClassificationScoreMetric : ISupervisedScoreMetric<int> { }
}
