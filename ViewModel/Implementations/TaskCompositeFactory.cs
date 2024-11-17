using Model.Interfaces;
using Model.Tasks;

using ViewModel.Interfaces;

namespace ViewModel.Implementations
{
    public class TaskCompositeFactory : IFactory<ITaskComposite>
    {
        private IFactory<object> _metadataFactory;

        public TaskCompositeFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public ITaskComposite Create() => new TaskComposite()
        {
            Metadata = _metadataFactory.Create()
        };
    }
}
