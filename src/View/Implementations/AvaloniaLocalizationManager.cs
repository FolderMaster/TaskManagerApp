using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using TrackableFeatures;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace View.Implementations
{
    /// <summary>
    /// Класс менеджера локализации Avalonia.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ILocalizationManager"/> и <see cref="IConfigurable"/>.
    /// </remarks>
    public class AvaloniaLocalizationManager : TrackableObject, ILocalizationManager, IConfigurable
    {
        /// <summary>
        /// Локализации.
        /// </summary>
        private static readonly Dictionary<CultureInfo, Uri> _localizations = new()
        {
            [new CultureInfo("en")] = new Uri("avares://View/Assets/Localizations/English.axaml"),
            [new CultureInfo("ru")] = new Uri("avares://View/Assets/Localizations/Russian.axaml")
        };

        /// <summary>
        /// Локализация.
        /// </summary>
        private CultureInfo _localization;

        /// <summary>
        /// Текущий словарь ресурсов.
        /// </summary>
        private static ResourceDictionary? _currentResources;

        /// <inheritdoc/>
        public IEnumerable<CultureInfo> Localizations => _localizations.Keys;

        /// <inheritdoc/>
        public CultureInfo ActualLocalization
        {
            get => _localization;
            set => UpdateProperty(ref _localization, value, SetLocalization);
        }

        /// <inheritdoc/>
        public object SettingsKey => ConfigurableKey.Localization;

        /// <inheritdoc/>
        public Type SettingsType => typeof(CultureInfo);

        /// <inheritdoc/>
        public object Settings
        {
            get => ActualLocalization;
            set => ActualLocalization = (CultureInfo)value;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AvaloniaLocalizationManager"/> по умолчанию.
        /// </summary>
        public AvaloniaLocalizationManager()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            ActualLocalization =
                Localizations.FirstOrDefault(c => c.Name == currentCulture.Name) ??
                Localizations.FirstOrDefault(c =>
                    c.TwoLetterISOLanguageName == currentCulture.TwoLetterISOLanguageName) ??
                Localizations.First();
        }
        
        /// <summary>
        /// Устанавливает локализацию.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение.</param>
        private void SetLocalization(CultureInfo oldValue, CultureInfo newValue)
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
