using Model.Interfaces;

using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    public class TaskElementCreatorProxyFactory : IFactory<ITaskElementProxy>
    {
        private IFactory<ITaskElement> _factory;

        public TaskElementCreatorProxyFactory(IFactory<ITaskElement> factory)
        {
            _factory = factory;
        }

        public ITaskElementProxy Create() => new TaskElementCreatorProxy(_factory.Create());
    }
}
