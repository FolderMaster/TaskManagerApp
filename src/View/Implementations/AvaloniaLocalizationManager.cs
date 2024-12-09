using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using TrackableFeatures;

using ViewModel.Interfaces.AppStates.Settings;

namespace View.Implementations
{
    public class AvaloniaLocalizationManager : TrackableObject, ILocalizationManager
    {
        private static readonly Dictionary<CultureInfo, Uri> _localizations = new()
        {
            [new CultureInfo("en")] = new Uri("avares://View/Assets/Localizations/English.axaml"),
            [new CultureInfo("ru")] = new Uri("avares://View/Assets/Localizations/Russian.axaml")
        };

        private CultureInfo _localization;

        private static ResourceDictionary? _currentResources;

        public IEnumerable<CultureInfo> Localizations => _localizations.Keys;

        public CultureInfo ActualLocalization
        {
            get => _localization;
            set => UpdateProperty(ref _localization, value, SetLocalization);
        }

        public AvaloniaLocalizationManager()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            ActualLocalization =
                Localizations.FirstOrDefault(c => c.Name == currentCulture.Name) ??
                Localizations.FirstOrDefault(c =>
                    c.TwoLetterISOLanguageName == currentCulture.TwoLetterISOLanguageName) ??
                Localizations.First();
        }

        private void SetLocalization()
        {
            CultureInfo.CurrentCulture = ActualLocalization;
            var app = Application.Current;
            var newDictionary = (ResourceDictionary)
                AvaloniaXamlLoader.Load(_localizations[ActualLocalization]);
            if (_currentResources != null)
            {
                app.Resources.MergedDictionaries.Remove(_currentResources);
            }
            _currentResources = newDictionary;
            app.Resources.MergedDictionaries.Add(newDictionary);
        }
    }
}
