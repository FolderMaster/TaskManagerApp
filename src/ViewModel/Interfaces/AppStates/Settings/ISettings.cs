using System.Collections;

namespace ViewModel.Interfaces.AppStates.Settings
{
    /// <summary>
    /// Интерфейс настроек.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IStorageService"/>.
    /// </remarks>
    public interface ISettings : IStorageService
    {
        /// <summary>
        /// Возвращает конфигурацию.
        /// </summary>
        public IDictionary Configuration { get; }
    }
}
