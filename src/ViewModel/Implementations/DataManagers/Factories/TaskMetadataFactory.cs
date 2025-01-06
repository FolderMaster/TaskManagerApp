using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая метаданные задачи.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{object}"/>.
    /// </remarks>
    public class TaskMetadataFactory : IFactory<object>
    {
        /// <inheritdoc/>
        public object Create() => new TaskMetadata() { Title = "Task" };
    }
}
