using Avalonia;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;

using TrackableFeatures;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace View.Implementations
{
    /// <summary>
    /// Класс менеджер тем Avalonia.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="IThemeManager"/> и <see cref="IConfigurable"/>.
    /// </remarks>
    public class AvaloniaThemeManager : TrackableObject, IThemeManager, IConfigurable
    {
        /// <summary>
        /// Тема.
        /// </summary>
        private object _theme;

        /// <inheritdoc/>
        public IEnumerable<object> Themes { get; private set; }

        /// <inheritdoc/>
        public object ActualTheme
        {
            get => _theme;
            set => UpdateProperty(ref _theme, value, SetTheme);
        }

        /// <inheritdoc/>
        public object SettingsKey => ConfigurableKey.Theme;

        /// <inheritdoc/>
        public Type SettingsType => typeof(object);

        /// <inheritdoc/>
        public object Settings
        {
            get => ActualTheme;
            set => ActualTheme = value;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AvaloniaThemeManager"/> по умолчанию.
        /// </summary>
        public AvaloniaThemeManager()
        {
            UpdateThemes();
            ActualTheme = Application.Current.ActualThemeVariant;
        }

        /// <summary>
        /// Обновляет темы.
        /// </summary>
        private void UpdateThemes()
        {
            var themes = Application.Current.Resources.ThemeDictionaries.Keys.ToList();
            themes.AddRange([ThemeVariant.Light, ThemeVariant.Dark]);
            Themes = themes;
        }

        /// <summary>
        /// Устанавливают тему.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение.</param>
        private void SetTheme(object oldValue, object newValue) =>
            Application.Current.RequestedThemeVariant = (ThemeVariant)ActualTheme;
    }
}
