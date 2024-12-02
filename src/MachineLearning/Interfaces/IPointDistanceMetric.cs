using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс метрики расстояния для точечных данных.
    /// </summary>
    public interface IPointDistanceMetric : IDistanceMetric<IEnumerable<double>> { }
}
