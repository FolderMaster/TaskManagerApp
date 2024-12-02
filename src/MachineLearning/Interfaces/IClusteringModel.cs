using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс модели обучения кластеризации.
    /// </summary>
    public interface IClusteringModel : IUnsupervisedLearningModel<double, int> { }
}
