using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения классификации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedScoreMetric{int}"/>.
    /// </remarks>
    public interface IClassificationScoreMetric : ISupervisedScoreMetric<int> { }
}
