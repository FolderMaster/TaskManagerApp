using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения кластеризации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedLearningModel{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IClusteringModel : IUnsupervisedLearningModel<IEnumerable<double>, int> { }
}
