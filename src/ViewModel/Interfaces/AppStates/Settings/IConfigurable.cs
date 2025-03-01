namespace ViewModel.Interfaces.AppStates.Settings
{
    /// <summary>
    /// Интерфейс настраиваемого объекта.
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// Возвращает ключ конфигурации.
        /// </summary>
        public object SettingsKey { get; } 

        /// <summary>
        /// Возвращает тип настроек.
        /// </summary>
        public Type SettingsType { get; }

        /// <summary>
        /// Возвращает и задаёт настройки.
        /// </summary>
        public object Settings { get; set; }
    }
}
