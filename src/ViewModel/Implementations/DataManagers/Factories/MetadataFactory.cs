using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая метаданные.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{object}"/>.
    /// </remarks>
    public class MetadataFactory : IFactory<object>
    {
        /// <inheritdoc/>
        public object Create() => new Metadata() { Title = "Task" };
    }
}
