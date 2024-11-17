using Model.Interfaces;
using Model.Tasks;

using ViewModel.Interfaces;

namespace ViewModel.Implementations
{
    public class TaskElementFactory : IFactory<ITaskElement>
    {
        private IFactory<object> _metadataFactory;

        public TaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public ITaskElement Create() => new TaskElement()
        {
            Metadata = _metadataFactory.Create()
        };
    }
}
