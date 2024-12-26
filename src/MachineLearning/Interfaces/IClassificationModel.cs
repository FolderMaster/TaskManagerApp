using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения классификации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ISupervisedLearningModel{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IClassificationModel : ISupervisedLearningModel<IEnumerable<double>, int> { }
}
