using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения кластеризации.
    /// Наследует <see cref="IUnsupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </summary>
    public interface IClusteringEvaluator :
        IUnsupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
