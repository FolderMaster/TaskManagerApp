using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика данных в точечные данные.
    /// Наследует <see cref="IDataProcessor{IEnumerable{T}, IEnumerable{double}}"/>.
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    public interface IPointDataProcessor<T> :
        IDataProcessor<IEnumerable<T>, IEnumerable<double>> { }
}
