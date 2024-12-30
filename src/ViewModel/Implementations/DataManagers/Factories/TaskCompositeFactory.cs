using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая составные задачи.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{ITaskComposite}"/>.
    /// </remarks>
    public class TaskCompositeFactory : IFactory<ITaskComposite>
    {
        /// <summary>
        /// Фабрика, создающая метаданные.
        /// </summary>
        private IFactory<object> _metadataFactory;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskCompositeFactory"/>.
        /// </summary>
        /// <param name="metadataFactory">Фабрика, создающая метаданные.</param>
        public TaskCompositeFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        /// <inheritdoc/>
        public ITaskComposite Create()
        {
            var result = new TaskCompositeDomain()
            {
                Metadata = _metadataFactory.Create(),
                Entity = new()
                {
                    Task = new()
                }
            };
            result.Entity.Task.TaskComposite = result.Entity;
            return result;
        }
    }
}
