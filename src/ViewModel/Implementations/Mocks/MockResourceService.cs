using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Mocks
{
    /// <summary>
    /// Класс-заглушка сервиса ресурсов.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IResourceService"/>.
    /// </remarks>
    public class MockResourceService : IResourceService
    {
        /// <inheritdoc/>
        public object? GetResource(object key)
        {
            throw new NotImplementedException();
        }
    }
}
