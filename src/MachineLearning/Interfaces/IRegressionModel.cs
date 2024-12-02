using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения регресии.
    /// </summary>
    public interface IRegressionModel : ISupervisedLearningModel<double, double> { }
}
