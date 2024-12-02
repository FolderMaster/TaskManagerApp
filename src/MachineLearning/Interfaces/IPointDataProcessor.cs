using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика данных в точечные данные.
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    public interface IPointDataProcessor<T> : IDataProcessor<T, double> { }
}
