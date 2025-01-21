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
using ViewModel.Interfaces.DataManagers;
using ViewModel.ViewModels;

namespace ViewModel.Tests.ViewModels.Pages
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(EditorViewModel), Category = "Integration, Functional",
        Description = $"Тестирование класса {nameof(EditorViewModel)}.")]
    public class EditorViewModelTests
    {
        private static string _dbPath = "EditorViewModel_database.db";

        private EditorViewModel _viewModel;

        private MainViewModel _mainViewModel;

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
            _mainViewModel = mockContainer.Resolve<MainViewModel>();
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
            $"{nameof(EditorViewModel.AddTaskElementCommand)}.")]
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

        [Test(Description = "Тестирование команды " +
            $"{nameof(EditorViewModel.AddTaskCompositeCommand)} " +
            "при обучении моделей для прогнозирования.")]
        public void AddTaskElementCommand_TeachLearningModels_ModelsPredictCorrectResults()
        {
            var expectedPredictedDeadline = new DateTime(2025, 1, 20);
            var expectedPredictedPlannedReal = 8.5;
            var expectedPredictedPlannedTime = new TimeSpan(10, 0, 0);

            var task1 = _taskElementFactory.Create();
            task1.Difficult = 3;
            task1.Priority = 2;
            task1.PlannedTime = new TimeSpan(10, 0, 0);
            task1.PlannedReal = 8.5;
            task1.Deadline = new DateTime(2025, 1, 20);

            var task2 = _taskElementFactory.Create();
            task2.Difficult = 1;
            task2.Priority = 1;
            task2.PlannedTime = new TimeSpan(5, 0, 0);
            task2.PlannedReal = 4.2;
            task2.Deadline = new DateTime(2025, 1, 10);

            var task3 = _taskElementFactory.Create();
            task3.Difficult = 5;
            task3.Priority = 3;
            task3.PlannedTime = new TimeSpan(20, 0, 0);
            task3.PlannedReal = 18.3;
            task3.Deadline = new DateTime(2025, 1, 30);

            var task4 = _taskElementFactory.Create();
            task4.Difficult = 2;
            task4.Priority = 1;
            task4.PlannedTime = new TimeSpan(8, 0, 0);
            task4.PlannedReal = 6.7;
            task4.Deadline = new DateTime(2025, 1, 12);

            var tasks = new ITask[] { task1, task2 };

            _mainViewModel.Activator.Activate();
            _session.AddTasks(tasks, null);
            var commandTask = _viewModel.AddTaskElementCommand.Execute().ToTask();
            var dialog = (AddTaskViewModel?)_viewModel.Dialogs.FirstOrDefault();

            Assert.That(dialog, Is.Not.Null, "Должен быть диалог!");

            var proxy = (ITaskElementProxy)dialog.Item;
            var isValidPredictedDeadline = proxy.IsValidPredictedDeadline;
            var isValidPredictedPlannedReal = proxy.IsValidPredictedPlannedReal;
            var isValidPredictedPlannedTime = proxy.IsValidPredictedPlannedTime;

            Assert.Multiple(() =>
            {
                Assert.That(isValidPredictedDeadline,
                    "Не обучена модель для прогнозирования сроков!");
                Assert.That(isValidPredictedPlannedReal,
                    "Не обучена модель для прогнозирования запланированого реального показателя!");
                Assert.That(isValidPredictedPlannedTime,
                    "Не обучена модель для прогнозирования запланированого времени!");
            });

            proxy.Difficult = 4;
            proxy.Priority = 2;
            var predictedDeadline = proxy.PredictedDeadline;
            var predictedPlannedReal = proxy.PredictedPlannedReal;
            var predictedPlannedTime = proxy.PredictedPlannedTime;

            Assert.Multiple(() =>
            {
                Assert.That(predictedDeadline,
                    Is.EqualTo(expectedPredictedDeadline).Within(TimeSpan.FromHours(1)),
                    "Неправильно обучена модель для прогнозирования сроков!");
                Assert.That(predictedPlannedReal,
                    Is.EqualTo(expectedPredictedPlannedReal).Within(1),
                    "Неправильно обучена модель для прогнозирования запланированого " +
                    "реального показателя!");
                Assert.That(predictedPlannedTime,
                    Is.EqualTo(expectedPredictedPlannedTime).Within(TimeSpan.FromDays(1)),
                    "Неправильно обучена модель для прогнозирования запланированого времени!");
            });
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

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.EditCommand)} " +
            "при выборе составной задачи.")]
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

        [Test(Description = $"Тестирование команды {nameof(EditorViewModel.EditCommand)} " +
            "при выборе элементарной задачи.")]
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
            "при выборе составной задачи.")]
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
