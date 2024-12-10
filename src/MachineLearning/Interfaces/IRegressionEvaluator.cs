using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения классификации.
    /// Наследует <see cref="ISupervisedLearningEvaluator{IEnumerable{double}, double}"/>.
    /// </summary>
    public interface IRegressionEvaluator :
        ISupervisedLearningEvaluator<IEnumerable<double>, double> { }
}
