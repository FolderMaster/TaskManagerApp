using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения классификации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IClassificationEvaluator :
        ISupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
