using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения классификации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedLearningEvaluator{IEnumerable{double}, double}"/>.
    /// </remarks>
    public interface IRegressionEvaluator :
        ISupervisedLearningEvaluator<IEnumerable<double>, double> { }
}
