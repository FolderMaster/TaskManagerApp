using Autofac;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(StatisticViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(StatisticViewModel)}.")]
    public class StatisticViewModelTests
    {
        private static string _dbPath = "StatisticViewModel_database.db";

        private StatisticViewModel _viewModel;

        private DbSession _session;

        private MockResourceService _resourceService;

        private IFactory<ITaskElement> _taskElementFactory;

        private IFactory<ITimeIntervalElement> _timeIntervalElementFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
            _resourceService = (MockResourceService)mockContainer.Resolve<IResourceService>();
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _timeIntervalElementFactory = mockContainer.Resolve<IFactory<ITimeIntervalElement>>();
            _viewModel = mockContainer.Resolve<StatisticViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Test(Description = "Тестирование свойств " +
            $"{nameof(StatisticViewModel.UncompletedTasksCountByDifficultStatistic)}, " +
            $"{nameof(StatisticViewModel.UncompletedTasksCountByPriorityStatistic)}, " +
            $"{nameof(StatisticViewModel.UncompletedTasksCountByCategoryStatistic)}, " +
            $"{nameof(StatisticViewModel.UncompletedTasksCountByTagsStatistic)} " +
            $"при выполнении команды {nameof(StatisticViewModel.UpdateCommand)}.")]
        public async Task GetUncompletedTasksCountStatistics_Update_ReturnCorrectResult()
        {
            var difficultDiagramContent = "Difficult";
            var priorityDiagramContent = "Priority";

            var task1 = _taskElementFactory.Create();
            task1.Difficult = 3;
            task1.Priority = 2;
            task1.Status = TaskStatus.OnHold;
            var metadata = (TaskMetadata)task1.Metadata;
            metadata.Category = "Education";
            metadata.Tags = ["Programming"];

            var task2 = _taskElementFactory.Create();
            task2.Difficult = 3;
            task2.Priority = 1;
            task2.Status = TaskStatus.InProgress;
            metadata = (TaskMetadata)task2.Metadata;
            metadata.Category = "Test";
            metadata.Tags = ["Programming", "Testing"];

            var task3 = _taskElementFactory.Create();
            task3.Difficult = 1;
            task3.Priority = 2;
            task3.Status = TaskStatus.Planned;
            metadata = (TaskMetadata)task3.Metadata;
            metadata.Category = "Education";
            metadata.Tags = ["Testing"];

            var tasks = new ITask[] { task1, task2, task3 };

            var difficultExpected = new StatisticElement[]
            {
                new(2, $"{difficultDiagramContent} 3")
            };
            var priorityExpected = new StatisticElement[]
            {
                new(1, $"{priorityDiagramContent} 2"),
                new(1, $"{priorityDiagramContent} 1"),
            };
            var categoryExpected = new StatisticElement[]
            {
                new(1, "Education"),
                new(1, "Test")
            };
            var tagsExpected = new StatisticElement[]
            {
                new(2, "Programming"),
                new(1, "Testing")
            };

            _resourceService.Resources.Add("DifficultDiagramContent", difficultDiagramContent);
            _resourceService.Resources.Add("PriorityDiagramContent", priorityDiagramContent);
            await _session.Load();
            _session.AddTasks(tasks, null);

            var difficultResult = _viewModel.UncompletedTasksCountByDifficultStatistic;
            var priorityResult = _viewModel.UncompletedTasksCountByPriorityStatistic;
            var categoryResult = _viewModel.UncompletedTasksCountByCategoryStatistic;
            var tagsResult = _viewModel.UncompletedTasksCountByTagsStatistic;

            Assert.Multiple(() =>
            {
                Assert.That(difficultResult,
                    Is.EqualTo(difficultExpected).UsingPropertiesComparer(),
                    "Неправильно рассчитана статистика сложности!");
                Assert.That(priorityResult, Is.EqualTo(priorityExpected).UsingPropertiesComparer(),
                    "Неправильно рассчитана статистика приоритета!");
                Assert.That(categoryResult, Is.EqualTo(categoryExpected).UsingPropertiesComparer(),
                    "Неправильно рассчитана статистика категории!");
                Assert.That(tagsResult, Is.EqualTo(tagsExpected).UsingPropertiesComparer(),
                    "Неправильно рассчитана статистика тегов!");
            });
        }

        [Test(Description = "Тестирование свойства " +
            $"{nameof(StatisticViewModel.ExpiredTasksStatistic)} " +
            $"при выполнении команды {nameof(StatisticViewModel.UpdateCommand)}.")]
        public async Task GetExpiredTasksStatistic_Update_ReturnCorrectResult()
        {
            var countDiagramContent = "Difficult";
            var plannedTimeDiagramContent = "Planned time";
            var spentTimeDiagramContent = "Spent time";
            var now = DateTime.Now;

            var task1 = _taskElementFactory.Create();
            task1.Deadline = now + new TimeSpan(0, 5, 0, 0);
            task1.PlannedTime = new TimeSpan(0, 3, 0, 0);
            task1.SpentTime = new TimeSpan(0, 2, 0, 0);
            task1.Status = TaskStatus.OnHold;

            var task2 = _taskElementFactory.Create();
            task2.Deadline = now + new TimeSpan(0, 10, 0, 0);
            task2.PlannedTime = new TimeSpan(0, 4, 0, 0);
            task2.SpentTime = new TimeSpan(0, 6, 0, 0);
            task2.Status = TaskStatus.InProgress;

            var task3 = _taskElementFactory.Create();
            task3.Deadline = now + new TimeSpan(0, 8, 0, 0);
            task3.PlannedTime = new TimeSpan(0, 1, 0, 0);
            task3.SpentTime = new TimeSpan(0, 2, 0, 0);
            task3.Status = TaskStatus.Planned;

            var selectedTime = new TimeSpan(0, 7, 0, 0);
            var tasks = new ITask[] { task1, task2, task3 };

            var expected = new StatisticElement[]
            {
                new(1, countDiagramContent),
                new(3, plannedTimeDiagramContent),
                new(2, spentTimeDiagramContent)
            };

            _resourceService.Resources.Add("CountDiagramContent", countDiagramContent);
            _resourceService.Resources.Add("PlannedTimeDiagramContent", plannedTimeDiagramContent);
            _resourceService.Resources.Add("SpentTimeDiagramContent", spentTimeDiagramContent);
            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTime = selectedTime;

            var result = _viewModel.ExpiredTasksStatistic;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно рассчитана статистика!");
        }

        [Test(Description = "Тестирование свойства " +
            $"{nameof(StatisticViewModel.TasksTimeStatistic)} " +
            $"при выполнении команды {nameof(StatisticViewModel.UpdateCommand)}.")]
        public async Task GetTasksTimeStatistic_Update_ReturnCorrectResult()
        {
            var plannedTimeDiagramContent = "Planned time";
            var unplannedTimeDiagramContent = "Unplanned time";
            var now = DateTime.Now;

            var task1 = _taskElementFactory.Create();
            task1.PlannedTime = new TimeSpan(0, 3, 0, 0);
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = now + new TimeSpan(0, 5, 0, 0);
            timeInterval1.End = now + new TimeSpan(0, 6, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);
            task1.Status = TaskStatus.OnHold;

            var task2 = _taskElementFactory.Create();
            task2.PlannedTime = new TimeSpan(0, 4, 0, 0);
            var timeInterval2 = _timeIntervalElementFactory.Create();
            timeInterval2.Start = now + new TimeSpan(0, 2, 0, 0);
            timeInterval2.End = now + new TimeSpan(0, 5, 0, 0);
            task2.TimeIntervals.Add(timeInterval2);
            task2.Status = TaskStatus.InProgress;

            var task3 = _taskElementFactory.Create();
            task3.PlannedTime = new TimeSpan(0, 1, 0, 0);
            var timeInterval3 = _timeIntervalElementFactory.Create();
            timeInterval3.Start = now + new TimeSpan(0, 2, 0, 0);
            timeInterval3.End = now + new TimeSpan(0, 4, 0, 0);
            task3.TimeIntervals.Add(timeInterval3);
            task3.Status = TaskStatus.Planned;

            var tasks = new ITask[] { task1, task2, task3 };

            var expected = new StatisticElement[]
            {
                new(4, plannedTimeDiagramContent),
                new(3, unplannedTimeDiagramContent)
            };

            _resourceService.Resources.Add("PlannedTimeDiagramContent", plannedTimeDiagramContent);
            _resourceService.Resources.Add("UnplannedTimeDiagramContent",
                unplannedTimeDiagramContent);
            await _session.Load();
            _session.AddTasks(tasks, null);

            var result = _viewModel.TasksTimeStatistic;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(), "Неправильно рассчитана статистика!");
        }
    }
}
