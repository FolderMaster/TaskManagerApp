using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс оценки модели обучения кластеризации на данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IDataUnsupervisedLearningEvaluator{IEnumerable{double}, int}"/>.
    /// </remarks>
    public interface IDataClusteringEvaluator :
        IDataUnsupervisedLearningEvaluator<IEnumerable<double>, int> { }
}
