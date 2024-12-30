namespace ViewModel.Interfaces.AppStates.Settings
{
    /// <summary>
    /// Интерфейс менеджера тем.
    /// </summary>
    public interface IThemeManager
    {
        /// <summary>
        /// Возвращает темы.
        /// </summary>
        public IEnumerable<object> Themes { get; }

        /// <summary>
        /// Возвращает и задаёт актуальную тему.
        /// </summary>
        public object ActualTheme { get; set; }
    }
}
