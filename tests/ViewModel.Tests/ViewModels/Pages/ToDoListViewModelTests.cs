using Autofac;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(ToDoListViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(ToDoListViewModel)}.")]
    public class ToDoListViewModelTests
    {
        private static string _dbPath = "ToDoListViewModel_database.db";

        private static string _settingsPath = "ToDoListViewModel_settings.json";

        private ToDoListViewModel _viewModel;

        private DbSession _session;

        private IFactory<ITaskElement> _taskElementFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _viewModel = mockContainer.Resolve<ToDoListViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }
    }
}
