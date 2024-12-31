using MachineLearning.Interfaces;
using MachineLearning.Scalers;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс фабрики, создающая масштабирования данных.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{IScaler}"/>.
    /// </remarks>
    public class ScalerFactory : IFactory<IScaler>
    {
        /// <inheritdoc/>
        public IScaler Create() => new MinMaxScaler();
    }
}
