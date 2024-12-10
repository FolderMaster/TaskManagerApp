using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения классификации.
    /// Наследует <see cref="ISupervisedLearningModel{IEnumerable{double}, int}"/>.
    /// </summary>
    public interface IClassificationModel : ISupervisedLearningModel<IEnumerable<double>, int> { }
}
