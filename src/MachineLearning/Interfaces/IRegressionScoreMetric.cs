using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения регрессии.
    /// </summary>
    public interface IRegressionScoreMetric : ISupervisedScoreMetric<double> { }
}
