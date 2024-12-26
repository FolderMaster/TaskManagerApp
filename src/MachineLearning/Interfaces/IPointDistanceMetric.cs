using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики расстояния для точечных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IDistanceMetric{IEnumerable{double}}"/>.
    /// </remarks>
    public interface IPointDistanceMetric : IDistanceMetric<IEnumerable<double>> { }
}
