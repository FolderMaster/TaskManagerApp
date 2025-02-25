using Autofac;
using Common.Tests;
using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;
using ViewModel.ViewModels;
using ViewModel.ViewModels.Pages;

using CategoryAttribute = Common.Tests.CategoryAttribute;
using TaskStatus = Model.TaskStatus;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Medium)]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(ToDoListViewModel),
        Description = $"Тестирование класса {nameof(ToDoListViewModel)}.")]
    public class ToDoListViewModelTests
    {
        private static string _dbPath = "ToDoListViewModel_database.db";

        private ToDoListViewModel _viewModel;

        private MainViewModel _mainViewModel;

        private DbSession _session;

        private IFactory<ITaskElement> _taskElementFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _mainViewModel = mockContainer.Resolve<MainViewModel>();
            _viewModel = mockContainer.Resolve<ToDoListViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Order(1)]
        [Test(Description = $"Тестирование свойства {nameof(ToDoListViewModel.ToDoList)} " +
            $"при выполнении команды {nameof(ToDoListViewModel.UpdateCommand)}.")]
        public async Task GetToDoList_Update_ReturnCorrectResult()
        {
            var task1 = _taskElementFactory.Create();
            task1.Deadline = DateTime.Now - new TimeSpan(0, 5, 0, 0);
            task1.PlannedTime = new TimeSpan(0, 3, 0, 0);
            task1.SpentTime = new TimeSpan(0, 2, 0, 0);
            task1.Status = TaskStatus.OnHold;

            var task2 = _taskElementFactory.Create();
            task2.Deadline = DateTime.Now + new TimeSpan(0, 1, 0, 0);
            task2.PlannedTime = new TimeSpan(0, 4, 0, 0);
            task2.SpentTime = new TimeSpan(0, 2, 0, 0);
            task2.Status = TaskStatus.InProgress;

            var task3 = _taskElementFactory.Create();
            task3.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task3.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task3.SpentTime = new TimeSpan(0, 2, 0, 0);
            task3.Status = TaskStatus.Planned;

            var task4 = _taskElementFactory.Create();
            task4.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task4.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task4.SpentTime = new TimeSpan(0, 2, 0, 0);
            task4.Status = TaskStatus.OnHold;

            var selectedTime = new TimeSpan(0, 7, 0, 0);
            var tasks = new ITask[] { task1, task2, task3, task4 };

            var expected = new ToDoListElement[]
            {
                new(task1, null, false, true),
                new(task2, null, true, false),
                new(task4, null, false, false)
            };

            await _session.Load();
            _session.AddTasks(tasks, null);

            var result = _viewModel.ToDoList;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно рассчитан список дел!");
        }

        [Order(3)]
        [Test(Description = $"Тестирование свойства {nameof(ToDoListViewModel.ToDoList)} " +
            $"при выполнении команды {nameof(ToDoListViewModel.UpdateCommand)} " +
            "и обучении моделей для прогнозирования.")]
        public void GetToDoList_TeachLearningModel_ModelPredictsCorrectResults()
        {
            var task1 = _taskElementFactory.Create();
            task1.Difficult = 3;
            task1.Priority = 2;
            task1.Deadline = DateTime.Now - new TimeSpan(0, 5, 0, 0);
            task1.PlannedTime = new TimeSpan(0, 3, 0, 0);
            task1.SpentTime = new TimeSpan(0, 2, 0, 0);
            task1.Status = TaskStatus.OnHold;

            var task2 = _taskElementFactory.Create();
            task2.Difficult = 1;
            task2.Priority = 1;
            task2.Deadline = DateTime.Now + new TimeSpan(0, 1, 0, 0);
            task2.PlannedTime = new TimeSpan(0, 4, 0, 0);
            task2.SpentTime = new TimeSpan(0, 2, 0, 0);
            task2.Status = TaskStatus.InProgress;

            var task3 = _taskElementFactory.Create();
            task3.Difficult = 5;
            task3.Priority = 3;
            task3.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task3.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task3.SpentTime = new TimeSpan(0, 2, 0, 0);
            task3.Status = TaskStatus.Planned;

            var task4 = _taskElementFactory.Create();
            task4.Difficult = 2;
            task4.Priority = 1;
            task4.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task4.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task4.SpentTime = new TimeSpan(0, 2, 0, 0);
            task4.Status = TaskStatus.OnHold;

            var tasks = new ITask[] { task1, task2, task3, task4 };

            var expected = new ToDoListElement[]
            {
                new(task1, 0, false, true),
                new(task2, 0.3, true, false),
                new(task4, 0.8, false, false)
            };

            _mainViewModel.Activator.Activate();
            _session.AddTasks(tasks, null);
            var result = _viewModel.ToDoList;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer().Within(0.1),
                "Неправильно рассчитан список дел!");
        }

        [Order(2)]
        [Pairwise]
        [Test(Description = $"Тестирование свойства {nameof(ToDoListViewModel.ToDoList)}, " +
            $"при настройке фильтров {nameof(ToDoListViewModel.IsExpiredFilter)}, " +
            $"{nameof(ToDoListViewModel.IsLaggingFilter)} и сортировки " +
            $"{nameof(ToDoListViewModel.IsRealSort)}, {nameof(ToDoListViewModel.IsTimeSort)}, " +
            $"{nameof(ToDoListViewModel.IsExecutionChanceSort)}, " +
            $"{nameof(ToDoListViewModel.IsDifficultSort)}," +
            $"{nameof(ToDoListViewModel.IsPrioritySort)}.")]
        public async Task GetToDoList_SetFiltersAndSorts_ReturnCorrectResult
            ([Values(true, false)] bool isExpiredFilter,
            [Values(true, false)] bool isLaggingFilter, 
            [Values(true, false)] bool isTimeSort, [Values(true, false)] bool isRealSort,
            [Values(true, false)] bool isExecutionChanceSort,
            [Values(true, false)] bool isDifficultSort, [Values(true, false)] bool isPrioritySort)
        {
            var sorts = new (bool, Func<ToDoListElement, object?>)[]
            {
                (isTimeSort, e => e.TaskElement.PlannedTime - e.TaskElement.SpentTime),
                (isRealSort, e => e.TaskElement.PlannedReal - e.TaskElement.ExecutedReal),
                (isExecutionChanceSort, e => e.ExecutionChance),
                (isDifficultSort, e => e.TaskElement.Difficult),
                (isPrioritySort, e => e.TaskElement.Priority)
            };

            var task1 = _taskElementFactory.Create();
            task1.Deadline = DateTime.Now - new TimeSpan(0, 5, 0, 0);
            task1.PlannedTime = new TimeSpan(0, 3, 0, 0);
            task1.SpentTime = new TimeSpan(0, 2, 0, 0);
            task1.Status = TaskStatus.OnHold;

            var task2 = _taskElementFactory.Create();
            task2.Deadline = DateTime.Now + new TimeSpan(0, 1, 0, 0);
            task2.PlannedTime = new TimeSpan(0, 4, 0, 0);
            task2.SpentTime = new TimeSpan(0, 2, 0, 0);
            task2.Status = TaskStatus.InProgress;

            var task3 = _taskElementFactory.Create();
            task3.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task3.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task3.SpentTime = new TimeSpan(0, 2, 0, 0);
            task3.Status = TaskStatus.Planned;

            var task4 = _taskElementFactory.Create();
            task4.Deadline = DateTime.Now + new TimeSpan(0, 8, 0, 0);
            task4.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task4.SpentTime = new TimeSpan(0, 2, 0, 0);
            task4.Status = TaskStatus.OnHold;

            var selectedTime = new TimeSpan(0, 7, 0, 0);
            var tasks = new ITask[] { task1, task2, task3, task4 };

            var expected = new ToDoListElement[]
            {
                new(task1, null, false, true),
                new(task2, null, true, false),
                new(task4, null, false, false)
            }.Where(e => (!isExpiredFilter || e.IsExpired) && (!isLaggingFilter || e.IsLagging));
            foreach (var sort in sorts)
            {
                if (sort.Item1)
                {
                    expected = expected.OrderBy(sort.Item2);
                }
            }

            await _session.Load();
            _session.AddTasks(tasks, null);

            _viewModel.IsExpiredFilter = isExpiredFilter;
            _viewModel.IsLaggingFilter = isLaggingFilter;
            var result = _viewModel.ToDoList;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно отфильтрован список дел!");
        }
    }
}
