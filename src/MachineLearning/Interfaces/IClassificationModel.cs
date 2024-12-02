using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения классификации.
    /// </summary>
    public interface IClassificationModel : ISupervisedLearningModel<double, int> { }
}
