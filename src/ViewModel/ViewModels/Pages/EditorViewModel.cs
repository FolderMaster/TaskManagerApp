using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.ViewModels.Modals;
using ViewModel.Implementations.AppStates;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.ViewModels.Pages
{
    public partial class EditorViewModel : PageViewModel
    {
        private readonly IObservable<bool> _canExecuteGoToPrevious;

        private readonly IObservable<bool> _canExecuteGo;

        private readonly IObservable<bool> _canExecuteRemove;

        private readonly IObservable<bool> _canExecuteAdd;

        private readonly IObservable<bool> _canExecuteEdit;

        private readonly IObservable<bool> _canExecuteCopy;

        private readonly IObservable<bool> _canExecuteMove;

        private readonly IObservable<bool> _canExecuteSwap;

        private readonly AppState _appState;

        [Reactive]
        private IEnumerable<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        public EditorViewModel(AppState appState)
        {
            _appState = appState;
            TaskListView = _appState.Session.Tasks;

            this.WhenAnyValue(x => x._appState.Session.Tasks).Subscribe
                (t => TaskListView = t);

            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.TaskListView).
                Select(i => TaskListView is ITask);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 && SelectedTasks.First() is ITaskComposite);
            _canExecuteRemove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteAdd = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 0 || (i == 1 && SelectedTasks.First() is ITaskComposite));
            _canExecuteEdit = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1);
            _canExecuteCopy = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteMove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteSwap = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 2 &&
                SelectedTasks.First().ParentTask == SelectedTasks.Last().ParentTask);

            Metadata = _appState.Services.ResourceService.GetResource("EditorPageMetadata");
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _appState.Session.Tasks;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTasks.First();
            SelectedTasks.Clear();
            TaskListView = composite;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
        private async Task Remove()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var result = await AddModal(_appState.Services.RemoveTasksDialog, items);
            if (result)
            {
                _appState.Session.RemoveTasks(items);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskElement() => await AddTask
            (_appState.Services.TaskElementProxyFactory.Create());

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskComposite() => await AddTask
            (_appState.Services.TaskCompositeFactory.Create());

        private async Task AddTask(ITask task)
        {
            var taskComposite = TaskListView as ITaskComposite;
            if (SelectedTasks.Count == 1)
            {
                taskComposite = (ITaskComposite)SelectedTasks.First();
                SelectedTasks.Clear();
            }
            var result = await AddModal(_appState.Services.AddTaskDialog, task);
            if (result)
            {
                _appState.Session.AddTasks
                    ([task is IProxy<ITask> proxy ? proxy.Target : task], taskComposite);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
        private async Task Edit()
        {
            var item = SelectedTasks.First();
            SelectedTasks.Clear();
            var editorService = (IEditorService)null;
            if (item is ITaskElement taskElement)
            {
                var taskElementsEditorService = _appState.Services.TaskElementsEditorProxy;
                taskElementsEditorService.Target = taskElement;
                editorService = taskElementsEditorService;
            }
            else
            {
                var tasksEditorService = _appState.Services.TasksEditorProxy;
                tasksEditorService.Target = item;
                editorService = tasksEditorService;
            }
            var result = await AddModal(_appState.Services.EditTaskDialog, editorService);
            if (result)
            {
                editorService.ApplyChanges();
                _appState.Session.EditTask(item);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private async Task Move()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var list = await AddModal(_appState.Services.MoveTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _appState.Session.Tasks));
            if (list != null)
            {
                _appState.Session.MoveTasks(items, (ITaskComposite?)list);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteCopy))]
        private async Task Copy()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var result = await AddModal(_appState.Services.CopyTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _appState.Session.Tasks));
            if (result != null)
            {
                var copyList = new List<ITask>();
                foreach (var task in items)
                {
                    if (task is ICloneable cloneable)
                    {
                        for (var i = 0; i < result.Count; ++i)
                        {
                            copyList.Add((ITask)cloneable.Clone());
                        }
                    }
                }
                _appState.Session.AddTasks(copyList, result.List as ITaskComposite);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteSwap))]
        private void Swap()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();

            var item1 = items.First();
            var item2 = items.Last();

            var list = item1.ParentTask ?? _appState.Session.Tasks as IList<ITask>;

            var index1 = list.IndexOf(item1);
            var index2 = list.IndexOf(item2);

            list[index1] = item2;
            list[index2] = item1;
        }
    }
}
