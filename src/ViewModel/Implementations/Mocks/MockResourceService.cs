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
        /// <summary>
        /// Возвращает и задаёт ресурсы.
        /// </summary>
        public Dictionary<object, object> Resources { get; set; } = new();

        /// <inheritdoc/>
        public object? GetResource(object key)
        {
            if (Resources.ContainsKey(key))
            {
                return Resources[key];
            }
            else
            {
                return null;
            }
        }
    }
}
