using Model;

namespace ViewModel.Technicals
{
    public class TaskCompositeFactory : IFactory<TaskComposite>
    {
        private IFactory<object> _metadataFactory;

        public TaskCompositeFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public TaskComposite Create() => new TaskComposite(_metadataFactory.Create());
    }
}
