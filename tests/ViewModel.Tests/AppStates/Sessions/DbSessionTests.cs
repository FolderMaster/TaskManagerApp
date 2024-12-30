using Autofac;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;

namespace ViewModel.Tests.AppStates.Sessions
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(DbSession), Category = "Integration",
        Description = $"Тестирование класса {nameof(DbSession)}.")]
    public class DbSessionTests
    {
        private static string _dbPath = "test.db";

        private DbSession _session;

        private IFactory<ITaskElement> _taskElementFactory;

        private IFactory<ITaskComposite> _taskCompositeFactory;

        private IFactory<ITimeIntervalElement> _timeIntervalElementFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _taskCompositeFactory = mockContainer.Resolve<IFactory<ITaskComposite>>();
            _timeIntervalElementFactory = mockContainer.Resolve<IFactory<ITimeIntervalElement>>();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.AddTasks)}.")]
        public async Task Load_ReturnEmptyAndInvokeEventHandler()
        {
            var tasks = new ITask[] { };
            var isHandledEvent = false;

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Reset && args.ItemsType == typeof(ITask) &&
                    args.Items.SequenceEqual(tasks))
                {
                    isHandledEvent = true;
                }
            };
            await _session.Load();
            var result = _session.Tasks;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(tasks), "Неправильно загружен!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.AddTasks)}.")]
        public async Task AddTasks_SaveAndLoad_ReturnAddedTasksAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[]
            {
                _taskElementFactory.Create(),
                _taskCompositeFactory.Create()
            };
            var isHandledEvent = false;

            await _session.Load();
            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Add && args.ItemsType == typeof(ITask) &&
                    args.Items.SequenceEqual(tasks))
                {
                    isHandledEvent = true;
                }
            };
            _session.AddTasks(tasks, taskComposite);
            var savedTasks = _session.Tasks.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(savedTasks, Is.EqualTo(tasks).
                    UsingPropertiesComparer(), "Неправильно добавлены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTasks = _session.Tasks.ToList();

            Assert.That(loadedTasks, Is.EqualTo(tasks).
                UsingPropertiesComparer(), "Неправильно сохранены объект!");
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.RemoveTasks)}.")]
        public async Task RemoveTasks_SaveAndLoad_ReturnUnremovedTasksAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[] { _taskElementFactory.Create(), _taskElementFactory.Create() };
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            await _session.Save();
            await _session.Load();

            var removedTasks = new ITask[] { _session.Tasks.ElementAt(0) };
            var unremovedTasks = new ITask[] { _session.Tasks.ElementAt(1) };

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Remove && args.ItemsType == typeof(ITask) &&
                    args.Items.SequenceEqual(removedTasks))
                {
                    isHandledEvent = true;
                }
            };
            _session.RemoveTasks(removedTasks);
            var savedTasks = _session.Tasks.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(savedTasks, Is.EqualTo(unremovedTasks).
                    UsingPropertiesComparer(), "Неправильно удалены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTasks = _session.Tasks.ToList();

            Assert.That(loadedTasks, Is.EqualTo(unremovedTasks).
                UsingPropertiesComparer(), "Неправильно сохранены объекты!");
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.EditTask)}.")]
        public async Task EditTask_SaveAndLoad_ReturnEditedTasksAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[] { _taskElementFactory.Create(), _taskElementFactory.Create() };
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            await _session.Save();
            await _session.Load();

            var editedTasks = _session.Tasks.ToList();
            var editedTask = (ITaskElement)_session.Tasks.ElementAt(0);
            editedTask.Difficult = 1;

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Edit &&
                    args.ItemsType.IsAssignableTo(typeof(ITaskElement)) &&
                    args.Items.SequenceEqual([editedTask]))
                {
                    isHandledEvent = true;
                }
            };
            _session.EditTask(editedTask);
            var savedTasks = _session.Tasks.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(savedTasks, Is.EqualTo(editedTasks).
                    UsingPropertiesComparer(), "Неправильно изменены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTasks = _session.Tasks.ToList();

            Assert.That(loadedTasks, Is.EqualTo(editedTasks).
                UsingPropertiesComparer(), "Неправильно сохранены объекты!");
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.MoveTasks)}.")]
        public async Task MoveTasks_SaveAndLoad_ReturnMovedTasksAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[]
            {
                _taskElementFactory.Create(),
                _taskCompositeFactory.Create()
            };
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            await _session.Save();
            await _session.Load();

            var loadedPrimaryTasks = _session.Tasks.ToList();
            var parentTask = (ITaskComposite)_session.Tasks.ElementAt(1);
            var movedTask = _session.Tasks.ElementAt(0);

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Move && args.ItemsType == typeof(ITask) &&
                    args.Items.SequenceEqual([movedTask]))
                {
                    isHandledEvent = true;
                }
            };
            _session.MoveTasks([movedTask], parentTask);
            var savedTasks = _session.Tasks.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(movedTask.ParentTask, Is.EqualTo(parentTask).
                    UsingPropertiesComparer(), "Неправильно перемещены объекты!");
                Assert.That(savedTasks.Contains(movedTask), Is.False, 
                    "Неправильно перемещены объекты!");
                Assert.That(savedTasks.Contains(parentTask),
                    "Неправильно перемещены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTasks = _session.Tasks.ToList();
            var loadedParentTask = (ITaskComposite)loadedTasks.First();

            Assert.Multiple(() =>
            {
                Assert.That(loadedParentTask, Is.EqualTo(parentTask).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(loadedParentTask.First(), Is.EqualTo(movedTask).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.AddTimeInterval)}.")]
        public async Task
            AddTimeInterval_SaveAndLoad_ReturnAddedTimeIntervalAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[] { _taskElementFactory.Create() };
            var timeIntervalElement = _timeIntervalElementFactory.Create();
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            await _session.Save();
            await _session.Load();

            var taskElement = (ITaskElement)_session.Tasks.ElementAt(0);

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Add &&
                    args.ItemsType.IsAssignableTo(typeof(ITimeInterval)) &&
                    args.Items.SequenceEqual([timeIntervalElement]))
                {
                    isHandledEvent = true;
                }
            };
            _session.AddTimeInterval(timeIntervalElement, taskElement);
            var savedTimeIntervals = taskElement.TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(savedTimeIntervals, Is.EqualTo([timeIntervalElement]).
                    UsingPropertiesComparer(), "Неправильно добавлены объекты!");
                Assert.That(taskElement.TimeIntervals.First(), Is.EqualTo(timeIntervalElement).
                    UsingPropertiesComparer(), "Неправильно добавлены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTimeIntervals = ((ITaskElement)_session.Tasks.ElementAt(0)).TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(loadedTimeIntervals, Is.EqualTo([timeIntervalElement]).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(taskElement.TimeIntervals.First(), Is.EqualTo(timeIntervalElement).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.RemoveTimeInterval)}.")]
        public async Task
            RemoveTimeInterval_SaveAndLoad_ReturnUnremovedTimeIntervalAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[] { _taskElementFactory.Create() };
            var timeIntervalElement = _timeIntervalElementFactory.Create();
            var unremovedTimeIntervalElements = new ITimeIntervalElement[] { };
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            _session.AddTimeInterval(timeIntervalElement,
                (ITaskElement)_session.Tasks.ElementAt(0));
            await _session.Save();
            await _session.Load();

            var taskElement = (ITaskElement)_session.Tasks.ElementAt(0);
            timeIntervalElement = taskElement.TimeIntervals.ElementAt(0);
            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Remove &&
                    args.ItemsType.IsAssignableTo(typeof(ITimeInterval)) &&
                    args.Items.SequenceEqual([timeIntervalElement]))
                {
                    isHandledEvent = true;
                }
            };
            _session.RemoveTimeInterval(timeIntervalElement, taskElement);
            var savedTimeIntervals = taskElement.TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(savedTimeIntervals, Is.EqualTo(unremovedTimeIntervalElements).
                    UsingPropertiesComparer(), "Неправильно удалены объекты!");
                Assert.That(taskElement.TimeIntervals.Count(), Is.Zero,
                    "Неправильно удалены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTimeIntervals = ((ITaskElement)_session.Tasks.ElementAt(0)).TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(loadedTimeIntervals, Is.EqualTo(unremovedTimeIntervalElements).
                    UsingPropertiesComparer(), "Неправильно удалены объекты!");
                Assert.That(taskElement.TimeIntervals.Count(), Is.Zero,
                    "Неправильно удалены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });
        }

        [Test(Description = $"Тестирование метода {nameof(DbSession.EditTimeInterval)}.")]
        public async Task
            EditTimeInterval_SaveAndLoad_ReturnEditedTimeIntervalAndInvokeEventHandler()
        {
            var taskComposite = (ITaskComposite?)null;
            var tasks = new ITask[] { _taskElementFactory.Create() };
            var timeIntervalElement = _timeIntervalElementFactory.Create();
            var unremovedTimeIntervalElements = new ITimeIntervalElement[] { };
            var isHandledEvent = false;

            await _session.Load();
            _session.AddTasks(tasks, taskComposite);
            _session.AddTimeInterval(timeIntervalElement,
                (ITaskElement)_session.Tasks.ElementAt(0));
            await _session.Save();
            await _session.Load();

            var taskElement = (ITaskElement)_session.Tasks.ElementAt(0);
            timeIntervalElement = taskElement.TimeIntervals.ElementAt(0);
            timeIntervalElement.Start = DateTime.Now;
            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Edit &&
                    args.ItemsType.IsAssignableTo(typeof(ITimeInterval)) &&
                    args.Items.SequenceEqual([timeIntervalElement]))
                {
                    isHandledEvent = true;
                }
            };
            _session.EditTimeInterval(timeIntervalElement);
            var savedTimeIntervals = taskElement.TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(savedTimeIntervals, Is.EqualTo([timeIntervalElement]).
                    UsingPropertiesComparer(), "Неправильно добавлены объекты!");
                Assert.That(taskElement.TimeIntervals.First(), Is.EqualTo(timeIntervalElement).
                    UsingPropertiesComparer(), "Неправильно добавлены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });

            await _session.Save();
            await _session.Load();
            var loadedTimeIntervals = ((ITaskElement)_session.Tasks.ElementAt(0)).TimeIntervals;

            Assert.Multiple(() =>
            {
                Assert.That(loadedTimeIntervals, Is.EqualTo([timeIntervalElement]).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(taskElement.TimeIntervals.First(), Is.EqualTo(timeIntervalElement).
                    UsingPropertiesComparer(), "Неправильно сохранены объекты!");
                Assert.That(isHandledEvent, "Событие не отработано!");
            });
        }
    }
}
