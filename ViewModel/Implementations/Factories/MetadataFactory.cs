using ViewModel.Interfaces;
using ViewModel.Technicals;

namespace ViewModel.Implementations.Factories
{
    public class MetadataFactory : IFactory<object>
    {
        public MetadataFactory() { }

        public object Create() => new Metadata() { Name = "Task" };
    }
}
