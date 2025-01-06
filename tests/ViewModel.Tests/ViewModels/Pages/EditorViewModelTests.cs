using Autofac;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.ViewModels.Pages;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(EditorViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(EditorViewModel)}.")]
    public class EditorViewModelTests
    {
        private static string _dbPath = "EditorViewModel_database.db";

        private EditorViewModel _viewModel;

        private DbSession _session;

        private IFactory<ITaskElement> _taskElementFactory;

        private IFactory<ITaskComposite> _taskCompositeFactory;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.SavePath = $"Data Source={_dbPath};Pooling=false";
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _taskCompositeFactory = mockContainer.Resolve<IFactory<ITaskComposite>>();
            _viewModel = mockContainer.Resolve<EditorViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Test(Description = "Тестирование команды " +
            $"{nameof(EditorViewModel.AddTaskCompositeCommand)}.")]
        public async Task AddTaskCompositeCommand_TaskCompositeAdded()
        {
            await _session.Load();
            var commandTask = _viewModel.AddTaskCompositeCommand.Execute().ToTask();
            var dialog = (AddTaskViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            var expected = new ITask[] { dialog.Item };
            var metadata = (TaskMetadata)dialog.Item.Metadata;
            metadata.Title = "Test";
            await dialog.OkCommand.Execute();
            await commandTask;
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно добавлена задача!");
        }

        [Test(Description = "Тестирование команды " +
            $"{nameof(EditorViewModel.AddTaskCompositeCommand)}.")]
        public async Task AddTaskElementCommand_TaskElementAdded()
        {
            await _session.Load();
            var commandTask = _viewModel.AddTaskElementCommand.Execute().ToTask();
            var dialog = (AddTaskViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            var expected = new ITask[] { ((IProxy<ITaskElement>)dialog.Item).Target };
            var metadata = (TaskMetadata)dialog.Item.Metadata;
            metadata.Title = "Test";
            await dialog.OkCommand.Execute();
            await commandTask;
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно добавлена задача!");
        }

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.RemoveCommand)} " +
            "при выборе составной задачи с задачами.")]
        public async Task RemoveCommand_SelectTaskCompositeWithTasks_TasksRemoved()
        {
            var task1 = _taskCompositeFactory.Create();
            task1.Add(_taskElementFactory.Create());
            var task2 = _taskElementFactory.Create();
            var tasks = new ITask[] { task1, task2 };
            var removingTasks = new ITask[] { task1 };
            var expected = new ITask[] { task2 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = removingTasks.ToList();
            var commandTask = _viewModel.RemoveCommand.Execute().ToTask();
            var dialog = (RemoveTasksViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            await dialog.OkCommand.Execute();
            await commandTask;
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно удалены задачи!");
        }

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.EditCommand)}.")]
        public async Task EditCommand_SelectTaskComposite_TaskCompositeEdited()
        {
            var task1 = _taskCompositeFactory.Create();
            task1.Add(_taskElementFactory.Create());
            var tasks = new ITask[] { task1 };
            var selectedTasks = new ITask[] { task1 };
            var expected = new ITask[] { task1 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            var commandTask = _viewModel.EditCommand.Execute().ToTask();
            var dialog = (EditTaskViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            var task = (ITask)dialog.Item;
            var metadata = (TaskMetadata)task.Metadata;
            metadata.Title = "Test";
            await dialog.OkCommand.Execute();
            await commandTask;
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно изменена задача!");
        }

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.EditCommand)}.")]
        public async Task EditCommand_SelectTaskElement_TaskElementEdited()
        {
            var task1 = _taskElementFactory.Create();
            var tasks = new ITask[] { task1 };
            var selectedTasks = new ITask[] { task1 };
            var expected = new ITask[] { task1 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            var commandTask = _viewModel.EditCommand.Execute().ToTask();
            var dialog = (EditTaskViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            var task = (ITask)dialog.Item;
            var metadata = (TaskMetadata)task.Metadata;
            metadata.Title = "Test";
            await dialog.OkCommand.Execute();
            await commandTask;
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно изменена задача!");
        }

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.GoCommand)} " +
            "выборе составной задачи.")]
        public async Task GoCommand_SelectTaskComposite_TaskListViewIsSelectedTaskComposite()
        {
            var task1 = _taskCompositeFactory.Create();
            var task2 = _taskElementFactory.Create();
            task1.Add(task2);
            var task3 = _taskElementFactory.Create();
            var tasks = new ITask[] { task1, task3 };
            var selectedTasks = new ITask[] { task1 };
            var expected = new ITask[] { task2 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            await _viewModel.GoCommand.Execute().ToTask();
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно переходит в составную задачу!");
        }

        [Test(Description = "Тестирование команды " +
            $"{nameof(EditorViewModel.GoToPreviousCommand)} " +
            "при переходе на составную задачу в корне.")]
        public async Task GoToPreviousCommand_GoToRootTaskComposite_TaskListViewIsRoot()
        {
            var task1 = _taskCompositeFactory.Create();
            var task2 = _taskElementFactory.Create();
            task1.Add(task2);
            var task3 = _taskElementFactory.Create();
            var tasks = new ITask[] { task1, task3 };
            var selectedTasks = new ITask[] { task1 };
            var expected = new ITask[] { task1, task3 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            await _viewModel.GoCommand.Execute().ToTask();
            await _viewModel.GoToPreviousCommand.Execute().ToTask();
            var result = _viewModel.TaskListView;

            Assert.That(result, Is.EqualTo(expected), "Неправильно переходит в корень!");
        }

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.MoveCommand)} " +
            "при перемещении составной задачи в составную задачу.")]
        public async Task MoveCommand_MoveCompositeTaskToCompositeTask_TasksMoved()
        {
            var taskComposite1 = _taskCompositeFactory.Create();
            var taskElement1 = _taskElementFactory.Create();
            taskComposite1.Add(taskElement1);
            var taskComposite2 = _taskCompositeFactory.Create();
            var taskElement2 = _taskElementFactory.Create();
            taskComposite2.Add(taskElement2);
            var taskElement3 = _taskElementFactory.Create();
            var tasks = new ITask[] { taskComposite1, taskComposite2, taskElement3 };
            var selectedTasks = new ITask[] { taskComposite2 };
            var expected1 = new ITask[] { taskComposite1, taskElement3 };
            var expected2 = new ITask[] { taskElement1, taskComposite2 };
            var expected3 = new ITask[] { taskElement2 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            var commandTask = _viewModel.MoveCommand.Execute().ToTask();
            var dialog = (MoveTasksViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            dialog.SelectedTask = taskComposite1;
            await dialog.GoCommand.Execute().ToTask();
            await dialog.OkCommand.Execute().ToTask();
            await commandTask;
            var result1 = _viewModel.TaskListView;
            _viewModel.SelectedTasks = new List<ITask>() { taskComposite1 };
            await _viewModel.GoCommand.Execute().ToTask();
            var result2 = _viewModel.TaskListView;
            _viewModel.SelectedTasks = new List<ITask>() { taskComposite2 };
            await _viewModel.GoCommand.Execute().ToTask();
            var result3 = _viewModel.TaskListView;

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.EqualTo(expected1), "Неправильно перемещены задачи!");
                Assert.That(result2, Is.EqualTo(expected2), "Неправильно перемещены задачи!");
                Assert.That(result3, Is.EqualTo(expected3), "Неправильно перемещены задачи!");
            });
        }

        /**[Test(Description = $"Тестирование команды {nameof(EditorViewModel.CopyCommand)} " +
            "при копировании составной задачи в составную задачу.")]
        public async Task CopyCommand_CopyCompositeTaskToCompositeTask_TasksCopied()
        {
            var taskComposite1 = _taskCompositeFactory.Create();
            var taskElement1 = _taskElementFactory.Create();
            taskComposite1.Add(taskElement1);
            var taskComposite2 = _taskCompositeFactory.Create();
            var taskElement2 = _taskElementFactory.Create();
            taskComposite2.Add(taskElement2);
            var taskElement3 = _taskElementFactory.Create();
            var tasks = new ITask[] { taskComposite1, taskComposite2, taskElement3 };
            var selectedTasks = new ITask[] { taskComposite2 };
            var expected1 = new ITask[] { taskComposite1, taskComposite2, taskElement3 };
            var expected2 = new ITask[] { taskElement1, taskComposite2 };
            var expected3 = new ITask[] { taskElement2 };

            await _session.Load();
            _session.AddTasks(tasks, null);
            _viewModel.SelectedTasks = selectedTasks.ToList();
            var commandTask = _viewModel.CopyCommand.Execute().ToTask();
            var dialog = (CopyTasksViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            dialog.SelectedTask = taskComposite1;
            await dialog.GoCommand.Execute().ToTask();
            await dialog.OkCommand.Execute().ToTask();
            await commandTask;

            var result1 = _viewModel.TaskListView;
            _viewModel.SelectedTasks = new List<ITask>() { taskComposite1 };
            await _viewModel.GoCommand.Execute().ToTask();
            var result2 = _viewModel.TaskListView;
            _viewModel.SelectedTasks = new List<ITask>() { taskComposite2 };
            await _viewModel.GoCommand.Execute().ToTask();
            var result3 = _viewModel.TaskListView;

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.EqualTo(expected1), "Неправильно перемещены задачи!");
                Assert.That(result2, Is.EqualTo(expected2), "Неправильно перемещены задачи!");
                Assert.That(result3, Is.EqualTo(expected3), "Неправильно перемещены задачи!");
            });
        }**/
    }
}
