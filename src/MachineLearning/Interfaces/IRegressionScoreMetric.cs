using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения регрессии.
    /// Наследует <see cref="ISupervisedScoreMetric{double}"/>.
    /// </summary>
    public interface IRegressionScoreMetric : ISupervisedScoreMetric<double> { }
}
