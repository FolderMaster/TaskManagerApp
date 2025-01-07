using Autofac;
using System.Reactive.Threading.Tasks;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(TimeViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(TimeViewModel)}.")]
    public class TimeViewModelTests
    {
        private static string _dbPath = "TimeViewModel_database.db";

        private TimeViewModel _viewModel;

        private DbSession _session;

        private MockResourceService _resourceService;

        private MockNotificationManager _notificationManager;

        private IFactory<ITaskElement> _taskElementFactory;

        private IFactory<ITimeIntervalElement> _timeIntervalElementFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
            _resourceService = (MockResourceService)mockContainer.Resolve<IResourceService>();
            _notificationManager =
                (MockNotificationManager)mockContainer.Resolve<INotificationManager>();
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _timeIntervalElementFactory = mockContainer.Resolve<IFactory<ITimeIntervalElement>>();
            _viewModel = mockContainer.Resolve<TimeViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Test(Description = "Тестирование свойства " +
            $"{nameof(TimeViewModel.CalendarIntervals)} " +
            $"при выполнении команды {nameof(TimeViewModel.UpdateCommand)}.")]
        public async Task GetCalendarIntervals_Update_ReturnCorrectResult()
        {
            var now = DateTime.Now;

            var task1 = _taskElementFactory.Create();
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = now + new TimeSpan(0, 5, 0, 0);
            timeInterval1.End = now + new TimeSpan(0, 6, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);

            var task2 = _taskElementFactory.Create();
            var timeInterval2 = _timeIntervalElementFactory.Create();
            timeInterval2.Start = now + new TimeSpan(0, 5, 0, 0);
            timeInterval2.End = now + new TimeSpan(0, 6, 0, 0);
            task2.TimeIntervals.Add(timeInterval2);
            var timeInterval3 = _timeIntervalElementFactory.Create();
            timeInterval3.Start = now + new TimeSpan(0, 7, 0, 0);
            timeInterval3.End = now + new TimeSpan(0, 10, 0, 0);
            task2.TimeIntervals.Add(timeInterval3);

            var task3 = _taskElementFactory.Create();

            var tasks = new ITask[] { task1, task2, task3 };
            var expected = new CalendarInterval[]
            {
                new(timeInterval1, task1),
                new(timeInterval2, task2),
                new(timeInterval3, task2)
            };

            await _session.Load();
            _session.AddTasks(tasks, null);

            var result = _viewModel.CalendarIntervals;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно получен календарь!");
        }

        [Test(Description = $"Тестирование команды {nameof(TimeViewModel.AddCommand)}.")]
        public async Task AddCommand_TimeIntervalElementAdded()
        {
            var task1 = _taskElementFactory.Create();
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = DateTime.Now + new TimeSpan(0, 5, 0, 0);
            timeInterval1.End = DateTime.Now + new TimeSpan(0, 6, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);

            var tasks = new ITask[] { task1 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            var commandTask = _viewModel.AddCommand.Execute().ToTask();
            var dialog = (AddTimeIntervalViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            dialog.SelectedTaskElement = (ITaskElement)dialog.List.ElementAt(0);
            dialog.TimeIntervalElement.Start += new TimeSpan(0, 5, 0, 0);
            dialog.TimeIntervalElement.End += new TimeSpan(0, 7, 0, 0);
            var expected = new CalendarInterval[]
            {
                new(timeInterval1, task1),
                new(dialog.TimeIntervalElement, task1)
            };
            await dialog.OkCommand.Execute().ToTask();
            await commandTask;
            var result = _viewModel.CalendarIntervals;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно добавлен временной интервал!");
        }

        [Test(Description = $"Тестирование команды {nameof(TimeViewModel.RemoveCommand)}.")]
        public async Task RemoveCommand_TimeIntervalElementRemoved()
        {
            var task1 = _taskElementFactory.Create();
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = DateTime.Now + new TimeSpan(0, 5, 0, 0);
            timeInterval1.End = DateTime.Now + new TimeSpan(0, 6, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);
            var timeInterval2 = _timeIntervalElementFactory.Create();
            timeInterval2.Start = DateTime.Now + new TimeSpan(0, 4, 0, 0);
            timeInterval2.End = DateTime.Now + new TimeSpan(0, 7, 0, 0);
            task1.TimeIntervals.Add(timeInterval2);

            var tasks = new ITask[] { task1 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            var expected = _viewModel.CalendarIntervals.Skip(0);
            _viewModel.SelectedCalendarInterval = _viewModel.CalendarIntervals.First();
            await _viewModel.RemoveCommand.Execute().ToTask();
            var result = _viewModel.CalendarIntervals;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно удалён временной интервал!");
        }

        [Test(Description = $"Тестирование команды {nameof(TimeViewModel.EditCommand)}.")]
        public async Task EditCommand_TimeIntervalElementEdited()
        {
            var task1 = _taskElementFactory.Create();
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = DateTime.Now + new TimeSpan(0, 5, 0, 0);
            timeInterval1.End = DateTime.Now + new TimeSpan(0, 6, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);
            var timeInterval2 = _timeIntervalElementFactory.Create();
            timeInterval2.Start = DateTime.Now + new TimeSpan(0, 4, 0, 0);
            timeInterval2.End = DateTime.Now + new TimeSpan(0, 7, 0, 0);
            task1.TimeIntervals.Add(timeInterval2);

            var tasks = new ITask[] { task1 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            var expected = _viewModel.CalendarIntervals;
            _viewModel.SelectedCalendarInterval = _viewModel.CalendarIntervals.First();
            var commandTask = _viewModel.EditCommand.Execute().ToTask();
            var dialog = (EditTimeIntervalViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            dialog.TimeIntervalElement.Start = DateTime.Now + new TimeSpan(0, 1, 0, 0);
            dialog.TimeIntervalElement.End = DateTime.Now + new TimeSpan(0, 2, 0, 0);
            await dialog.OkCommand.Execute().ToTask();
            await commandTask;
            var result = _viewModel.CalendarIntervals;

            Assert.That(result, Is.EqualTo(expected).UsingPropertiesComparer(),
                "Неправильно изменён временной интервал!");
        }

        [Repeat(5)]
        [TestCase([1000, 100])]
        [TestCase([100, 10])]
        [TestCase([10, 1])]
        [TestCase([1, 0])]
        [Test(Description = "Тестирование отправки уведомления при достижении " +
            $"{nameof(TimeViewModel.CalendarIntervals)} " +
            $"при выполнении команды {nameof(TimeViewModel.UpdateCommand)}.")]
        public async Task TimeIntervalReached_Update_NotificationSended
            (int time, int toleranceTime)
        {
            var timeSchedulerNotificationTitle = "Time!";
            var timeSchedulerNotificationContent = "Time reached for:";
            var now = DateTime.Now;

            var task1 = _taskElementFactory.Create();
            var timeInterval1 = _timeIntervalElementFactory.Create();
            timeInterval1.Start = now.AddMilliseconds(time);
            timeInterval1.End = now + new TimeSpan(0, 1, 0, 0);
            task1.TimeIntervals.Add(timeInterval1);

            var task2 = _taskElementFactory.Create();
            var timeInterval2 = _timeIntervalElementFactory.Create();
            timeInterval2.Start = now.AddMilliseconds(time);
            timeInterval2.End = now + new TimeSpan(0, 2, 0, 0);
            task2.TimeIntervals.Add(timeInterval2);

            var tasks = new ITask[] { task1, task2 };
            var result = false;

            _notificationManager.NotificationSended += (sender, e) =>
            {
                if (e.Title == timeSchedulerNotificationTitle &&
                    e.Description == $"{timeSchedulerNotificationContent}\n" +
                    $"- {task1.Metadata}\n- {task2.Metadata}\n")
                {
                    result = true;
                }
            };
            _resourceService.Resources.Add("TimeSchedulerNotificationTitle",
                timeSchedulerNotificationTitle);
            _resourceService.Resources.Add("TimeSchedulerNotificationContent",
                timeSchedulerNotificationContent);
            await _session.Load();
            _session.AddTasks(tasks, null);
            await Task.Delay(time + toleranceTime);

            Assert.That(result, "Неправильно отправлено уведомление!");
        }
    }
}
