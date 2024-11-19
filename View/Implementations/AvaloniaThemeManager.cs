using Avalonia;
using Avalonia.Styling;
using System.Collections.Generic;
using System.Linq;

using Model.Technicals;

using ViewModel.Interfaces;

namespace View.Implementations
{
    public class AvaloniaThemeManager : TrackableObject, IThemeManager
    {
        private object _theme;

        public IEnumerable<object> Themes { get; private set; }

        public object ActualTheme
        {
            get => _theme;
            set => UpdateProperty(ref _theme, value, SetTheme);
        }

        public AvaloniaThemeManager()
        {
            var application = Application.Current;
            UpdateThemes();
            ActualTheme = Application.Current.ActualThemeVariant;
        }

        private void UpdateThemes()
        {
            var themes = Application.Current.Resources.ThemeDictionaries.Keys.ToList();
            themes.AddRange([ThemeVariant.Light, ThemeVariant.Dark]);
            Themes = themes;
        }

        private void SetTheme()
        {
            Application.Current.RequestedThemeVariant = (ThemeVariant)ActualTheme;
        }
    }
}
