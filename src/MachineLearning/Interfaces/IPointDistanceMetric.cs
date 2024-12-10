using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики расстояния для точечных данных.
    /// Наследует <see cref="IDistanceMetric{IEnumerable{double}}"/>.
    /// </summary>
    public interface IPointDistanceMetric : IDistanceMetric<IEnumerable<double>> { }
}
