using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    public class MetadataFactory : IFactory<object>
    {
        public MetadataFactory() { }

        public object Create() => new Metadata() { Title = "Task" };
    }
}
