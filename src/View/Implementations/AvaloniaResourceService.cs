using Avalonia;
using Avalonia.Styling;
using System;

using ViewModel.Interfaces.AppStates;

namespace View.Implementations
{
    /// <summary>
    /// Класс сервис ресурсов Avalonia.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IResourceService"/>.
    /// </remarks>
    public class AvaloniaResourceService : IResourceService
    {
        /// <summary>
        /// Приложение.
        /// </summary>
        private readonly Application _application;

        /// <summary>
        /// Вариант темы.
        /// </summary>
        private ThemeVariant _themeVariant;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AvaloniaResourceService"/> по умолчанию.
        /// </summary>
        public AvaloniaResourceService()
        {
            _application = Application.Current;
            _themeVariant = _application.ActualThemeVariant;
            _application.ActualThemeVariantChanged += Application_ActualThemeVariantChanged;
        }

        /// <inheritdoc/>
        public object? GetResource(object key)
        {
            _application.Resources.TryGetResource(key, _themeVariant, out var result);
            return result;
        }

        private void Application_ActualThemeVariantChanged(object? sender, EventArgs e)
        {
            _themeVariant = _application.ActualThemeVariant;
        }
    }
}
