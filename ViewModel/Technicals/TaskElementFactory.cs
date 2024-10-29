using Model;

namespace ViewModel.Technicals
{
    public class TaskElementFactory : IFactory<TaskElement>
    {
        private IFactory<object> _metadataFactory;

        public TaskElementFactory(IFactory<object> metadataFactory)
        {
            ArgumentNullException.ThrowIfNull(metadataFactory, nameof(metadataFactory));
            _metadataFactory = metadataFactory;
        }

        public TaskElement Create() => new TaskElement(_metadataFactory.Create());
    }
}
