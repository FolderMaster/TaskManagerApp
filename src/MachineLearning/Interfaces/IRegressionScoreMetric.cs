using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения регрессии.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedScoreMetric{double}"/>.
    /// </remarks>
    public interface IRegressionScoreMetric : ISupervisedScoreMetric<double> { }
}
