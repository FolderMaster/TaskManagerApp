using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения кластеризации.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IClusteringEvaluator :
        IUnsupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
