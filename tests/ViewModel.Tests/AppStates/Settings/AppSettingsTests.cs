using Autofac;
using System.Globalization;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;

namespace ViewModel.Tests.AppStates.Settings
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(AppSettings), Category = "Integration",
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
            _localizationManager =
                (MockLocalizationManager)mockContainer.Resolve<ILocalizationManager>();
            _localizationManager.Localizations = _localizations;
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = _connectionString;
            _settings = (AppSettings)mockContainer.Resolve<ISettings>();
            _settings.FilePath = _settingsPath;
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
            var expected = new AppConfiguration()
            {
                Themes = _themes,
                ActualTheme = _themes[0],
                Localizations = _localizations,
                ActualLocalization = _localizations[0],
                SavePath = _connectionString
            };

            var result = _settings.Configuration;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно инициализирован!");
        }

        [Test(Description = $"Тестирование свойства {nameof(AppSettings.Configuration)} " +
            "при загрузке и сохранении.")]
        public async Task GetConfiguration_SaveAndLoad_ReturnSavedValues()
        {
            var expected = new AppConfiguration()
            {
                Themes = _themes,
                ActualTheme = _themes[1],
                Localizations = _localizations,
                ActualLocalization = _localizations[1],
                SavePath = _connectionString
            };

            var configuration = (AppConfiguration)_settings.Configuration;
            configuration.ActualLocalization = _localizations[1];
            configuration.ActualTheme = _themes[1];
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
            var expected = new AppConfiguration()
            {
                Themes = _themeManager.Themes,
                ActualTheme = _themeManager.ActualTheme,
                Localizations = _localizationManager.Localizations,
                ActualLocalization = _localizationManager.ActualLocalization,
                SavePath = _session.SavePath
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

            var configuration = (AppConfiguration)_settings.Configuration;
            configuration.ActualLocalization = expectedLocalization;
            configuration.ActualTheme = expectedTheme;
            configuration.SavePath = expectedConnectionString;

            Assert.Multiple(() =>
            {
                Assert.That(_themeManager.ActualTheme, Is.EqualTo(expectedTheme),
                    "Неправильно изменён сервис!");
                Assert.That(_localizationManager.ActualLocalization,
                    Is.EqualTo(expectedLocalization), "Неправильно изменён сервис!");
                Assert.That(_session.SavePath, Is.EqualTo(expectedConnectionString),
                    "Неправильно изменён сервис!");
            });
        }
    }
}
