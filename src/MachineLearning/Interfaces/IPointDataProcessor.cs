using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика точечных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IDataProcessor{IEnumerable{double}, IEnumerable{double}}"/>.
    /// </remarks>
    public interface IPointDataProcessor :
        IDataProcessor<IEnumerable<double>, IEnumerable<double>> { }
}
