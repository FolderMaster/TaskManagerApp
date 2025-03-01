using Autofac;
using Common.Tests;
using System.Globalization;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace ViewModel.Tests.AppStates.Settings
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Major)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Fast)]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(AppSettings),
        Description = $"Тестирование класса {nameof(AppSettings)}.")]
    public class AppSettingsTests
    {
        private static string _settingsPath = "AppSettings_settings.json";

        private static string _connectionString = "Data Source=AppSettings_database.db";

        private static string[] _themes = ["Light", "Dark"];

        private static CultureInfo[] _localizations =
        [
            new CultureInfo("en"),
            new CultureInfo("ru")
        ];

        private object _themeKey;

        private object _localizationKey;

        private object _sessionKey;

        private AppSettings _settings;

        private DbSession _session;

        private MockThemeManager _themeManager;

        private MockLocalizationManager _localizationManager;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _themeManager = (MockThemeManager)mockContainer.Resolve<IThemeManager>();
            _themeManager.Themes = _themes;
            _themeKey = _themeManager.SettingsKey;
            _localizationManager =
                (MockLocalizationManager)mockContainer.Resolve<ILocalizationManager>();
            _localizationManager.Localizations = _localizations;
            _localizationKey = _localizationManager.SettingsKey;
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.ConnectionString = _connectionString;
            _settings = (AppSettings)mockContainer.Resolve<ISettings>();
            _settings.FilePath = _settingsPath;
            _sessionKey = _session.SettingsKey;
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_settingsPath);
        }

        [Test(Description = $"Тестирование свойства {nameof(AppSettings.Configuration)} " +
            "при инициализации.")]
        public void GetConfiguration_InitializeConfiguration_ReturnDefaultValues()
        {
            var expected = new Dictionary<object, object>()
            {
                [_themeKey] = _themes[0],
                [_localizationKey] = _localizations[0],
                [_sessionKey] = _connectionString
            };

            var result = _settings.Configuration;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно инициализирован!");
        }

        [Test(Description = $"Тестирование свойства {nameof(AppSettings.Configuration)} " +
            "при загрузке и сохранении.")]
        public async Task GetConfiguration_SaveAndLoad_ReturnSavedValues()
        {
            var expected = new Dictionary<object, object>()
            {
                [_themeKey] = _themes[1],
                [_localizationKey] = _localizations[1],
                [_sessionKey] = _connectionString
            };

            var configuration = _settings.Configuration;
            configuration[_localizationKey] = _localizations[1];
            configuration[_themeKey] = _themes[1];
            await _settings.Save();
            await _settings.Load();
            var result = _settings.Configuration;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно загружен!");
        }

        [Test(Description = $"Тестирование свойства {nameof(AppSettings.Configuration)} " +
            "при загрузке без файла.")]
        public async Task GetConfiguration_LoadWithoutFile_ReturnDefaultValues()
        {
            var expected = new Dictionary<object, object>()
            {
                [_themeKey] = _themeManager.ActualTheme,
                [_localizationKey] = _localizationManager.ActualLocalization,
                [_sessionKey] = _session.ConnectionString
            };

            await _settings.Load();
            var result = _settings.Configuration;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно загружен!");
        }

        [Test(Description = "Тестирование изменения свойства " +
            $"{nameof(AppSettings.Configuration)}.")]
        public void EditConfiguration_EditServicesProperties()
        {
            var expectedTheme = _themes[1];
            var expectedLocalization = _localizations[1];
            var expectedConnectionString = "Test";

            var configuration = _settings.Configuration;
            configuration[_localizationKey] = expectedLocalization;
            configuration[_themeKey] = expectedTheme;
            configuration[_sessionKey] = expectedConnectionString;

            Assert.Multiple(() =>
            {
                Assert.That(_themeManager.ActualTheme, Is.EqualTo(expectedTheme),
                    "Неправильно изменён сервис!");
                Assert.That(_localizationManager.ActualLocalization,
                    Is.EqualTo(expectedLocalization), "Неправильно изменён сервис!");
                Assert.That(_session.ConnectionString, Is.EqualTo(expectedConnectionString),
                    "Неправильно изменён сервис!");
            });
        }
    }
}
