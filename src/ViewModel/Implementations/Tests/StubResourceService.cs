using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.Tests
{
    /// <summary>
    /// Класс-заглушка сервиса ресурсов.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IResourceService"/>.
    /// </remarks>
    public class StubResourceService : IResourceService
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
