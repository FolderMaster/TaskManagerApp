using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая элементарные задачи.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{ITaskElement}"/>.
    /// </remarks>
    public class TaskElementFactory : IFactory<ITaskElement>
    {
        /// <summary>
        /// Фабрика, создающая метаданные.
        /// </summary>
        private IFactory<object> _metadataFactory;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementFactory"/>.
        /// </summary>
        /// <param name="metadataFactory">Фабрика, создающая метаданные.</param>
        public TaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        /// <inheritdoc/>
        public ITaskElement Create()
        {
            var result = new TaskElementDomain()
            {
                Metadata = _metadataFactory.Create(),
                Entity = new()
                {
                    Task = new()
                }
            };
            result.Entity.Task.TaskElement = result.Entity;
            return result;
        }
    }
}
