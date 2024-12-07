using Model.Interfaces;

using ViewModel.Implementations.Sessions.Database.Domains;
using ViewModel.Interfaces;

namespace ViewModel.Implementations.Factories
{
    public class TaskElementFactory : IFactory<ITaskElement>
    {
        private IFactory<object> _metadataFactory;

        public TaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

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
