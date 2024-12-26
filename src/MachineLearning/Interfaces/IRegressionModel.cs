using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения регресии.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedLearningModel{IEnumerable{double}, double}"/>.
    /// </remarks>
    public interface IRegressionModel : ISupervisedLearningModel<IEnumerable<double>, double> { }
}
