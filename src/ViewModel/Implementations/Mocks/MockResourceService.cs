using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    public class MockResourceService : IResourceService
    {
        public object? GetResource(object key)
        {
            throw new NotImplementedException();
        }
    }
}
