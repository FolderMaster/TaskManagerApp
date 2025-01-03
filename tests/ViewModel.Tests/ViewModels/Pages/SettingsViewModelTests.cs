using Autofac;
using System.Globalization;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(SettingsViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(SettingsViewModel)}.")]
    public class SettingsViewModelTests
    {
        private static string _filePath = "settings.json";

        private static string _connectionString = "Data Source=test.db";

        private static string[] _themes = ["Light", "Dark"];

        private static CultureInfo[] _localizations =
        [
            new CultureInfo("en"),
            new CultureInfo("ru")
        ];

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
            _localizationManager =
                (MockLocalizationManager)mockContainer.Resolve<ILocalizationManager>();
            _localizationManager.Localizations = _localizations;
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = _connectionString;
            var settings = (AppSettings)mockContainer.Resolve<ISettings>();
            settings.FilePath = _filePath;
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_filePath);
        }

        [Test(Description = "Тестирование изменения свойства " +
            $"{nameof(SettingsViewModel.Configuration)}")]
        public void EditConfiguration_EditServicesProperties()
        {
            var expectedTheme = _themes[1];
            var expectedLocalization = _localizations[1];
            var expectedConnectionString = "Test";

            var configuration = (AppConfiguration)_viewModel.Configuration;
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
