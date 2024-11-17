using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.ViewModels.Modals;
using ViewModel.AppState;

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

        private readonly AppStateManager _appStateManager;

        [Reactive]
        private IList<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        public EditorViewModel(AppStateManager appStateManager)
        {
            _appStateManager = appStateManager;
            TaskListView = _appStateManager.Session.Tasks;

            this.WhenAnyValue(x => x._appStateManager.Session.Tasks).Subscribe
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

            Metadata = _appStateManager.Services.ResourceService.GetResource("EditorPageMetadata");
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _appStateManager.Session.Tasks;
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
            var result = await AddModal(_appStateManager.Services.RemoveTasksDialog, items);
            if (result)
            {
                foreach (var item in items)
                {
                    if (item.ParentTask == null)
                    {
                        _appStateManager.Session.Tasks.Remove(item);
                    }
                    else
                    {
                        item.ParentTask.Remove(item);
                    }
                }
                _appStateManager.UpdateSessionItems();
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskElement() => await AddTask
            (_appStateManager.Services.TaskElementFactory.Create());

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskComposite() => await AddTask
            (_appStateManager.Services.TaskCompositeFactory.Create());

        private async Task AddTask(ITask task)
        {
            var list = TaskListView;
            if (SelectedTasks.Count == 1)
            {
                list = (ITaskComposite)SelectedTasks.First();
                SelectedTasks.Clear();
            }
            var result = await AddModal(_appStateManager.Services.AddTaskDialog, task);
            if (result)
            {
                list.Add(task);
                _appStateManager.UpdateSessionItems();
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
        private async Task Edit()
        {
            var item = SelectedTasks.First();
            SelectedTasks.Clear();
            var result = await AddModal(_appStateManager.Services.EditTaskDialog, item);
            if (result)
            {
                _appStateManager.UpdateSessionItems();
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private async Task Move()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var list = await AddModal(_appStateManager.Services.MoveTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _appStateManager.Session.Tasks));
            if (list != null)
            {
                foreach (var item in items)
                {
                    if (item.ParentTask != null)
                    {
                        item.ParentTask.Remove(item);
                    }
                    else
                    {
                        _appStateManager.Session.Tasks.Remove(item);
                    }
                    list.Add(item);
                }
                _appStateManager.UpdateSessionItems();
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteCopy))]
        private async Task Copy()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var list = await AddModal(_appStateManager.Services.CopyTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _appStateManager.Session.Tasks));
            if (list != null)
            {
                var copyList = new List<ITask>();
                foreach (var task in items)
                {
                    if (task is ICloneable cloneable)
                    {
                        copyList.Add((ITask)cloneable.Clone());
                    }
                }
                foreach (var task in copyList)
                {
                    list.Add(task);
                }
                _appStateManager.UpdateSessionItems();
            }
        }
    }
}
