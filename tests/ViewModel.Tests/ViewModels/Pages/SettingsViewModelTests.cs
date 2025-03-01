using Autofac;
using Common.Tests;
using System.Globalization;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Fast)]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(SettingsViewModel),
        Description = $"Тестирование класса {nameof(SettingsViewModel)}.")]
    public class SettingsViewModelTests
    {
        private static string _settingsPath = "SettingsViewModel_settings.json";

        private static string _connectionString = "Data Source=SettingsViewModel_database.db";

        private static string[] _themes = ["Light", "Dark"];

        private static CultureInfo[] _localizations =
        [
            new CultureInfo("en"),
            new CultureInfo("ru")
        ];

        private object _themeKey;

        private object _localizationKey;

        private object _sessionKey;

        private SettingsViewModel _viewModel;

        private DbSession _session;

        private MockThemeManager _themeManager;

        private MockLocalizationManager _localizationManager;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<SettingsViewModel>();
            _themeManager = (MockThemeManager)mockContainer.Resolve<IThemeManager>();
            _themeManager.Themes = _themes;
            _themeKey = _themeManager.SettingsKey;
            _localizationManager =
                (MockLocalizationManager)mockContainer.Resolve<ILocalizationManager>();
            _localizationManager.Localizations = _localizations;
            _localizationKey = _localizationManager.SettingsKey;
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.ConnectionString = _connectionString;
            _sessionKey = _session.SettingsKey;
            var settings = (AppSettings)mockContainer.Resolve<ISettings>();
            settings.FilePath = _settingsPath;
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_settingsPath);
        }

        [Test(Description = "Тестирование изменения свойства " +
            $"{nameof(SettingsViewModel.Configuration)}.")]
        public void EditConfiguration_EditServicesProperties()
        {
            var expectedTheme = _themes[1];
            var expectedLocalization = _localizations[1];
            var expectedConnectionString = "Test";

            var configuration = _viewModel.Configuration;
            configuration.ActualLocalization = expectedLocalization;
            configuration.ActualTheme = expectedTheme;
            configuration.ConnectionString = expectedConnectionString;

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
