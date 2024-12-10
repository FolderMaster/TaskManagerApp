using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения регресии.
    /// Наследует <see cref="ISupervisedLearningModel{IEnumerable{double}, double}"/>.
    /// </summary>
    public interface IRegressionModel : ISupervisedLearningModel<IEnumerable<double>, double> { }
}
