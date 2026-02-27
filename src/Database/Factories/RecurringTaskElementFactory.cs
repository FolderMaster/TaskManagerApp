using Database.Domains;
using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace Database.Factories
{
    public class RecurringTaskElementFactory : IFactory<IRecurringTaskElement>
    {
        /// <summary>
        /// Фабрика, создающая метаданные.
        /// </summary>
        private IFactory<object> _metadataFactory;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskCompositeFactory"/>.
        /// </summary>
        /// <param name="metadataFactory">Фабрика, создающая метаданные.</param>
        /// <exception cref="ArgumentException"/>
        public RecurringTaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public IRecurringTaskElement Create()
        {
            var result = new RecurringTaskElementDomain()
            {
                Metadata = _metadataFactory.Create(),
                Entity = new()
                {
                    TaskElement = new()
                    {
                        Task = new()
                    }
                }
            };
            result.Entity.Task.TaskElement = result.Entity;
            return result;
        }
    }
}
