using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения классификации.
    /// Наследует <see cref="ISupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </summary>
    public interface IClassificationEvaluator :
        ISupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
