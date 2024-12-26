using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс первичного обработчика точечных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IDataProcessor{IEnumerable{Nullable{double}}, IEnumerable{double}}"/>
    /// и <see cref="IDataProcessor{Nullable{double}, double}"/>.
    /// </remarks>
    public interface IPrimaryPointDataProcessor : IDataProcessor<double?, double>,
        IDataProcessor<IEnumerable<double?>, IEnumerable<double>> { }
}
