using Avalonia;
using Avalonia.Styling;
using System;

using ViewModel.Interfaces.AppStates;

namespace View.Implementations
{
    public class AvaloniaResourceService : IResourceService
    {
        private readonly Application _application;

        private ThemeVariant _themeVariant;

        public AvaloniaResourceService()
        {
            _application = Application.Current;
            _themeVariant = _application.ActualThemeVariant;
            _application.ActualThemeVariantChanged += Application_ActualThemeVariantChanged;
        }

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
