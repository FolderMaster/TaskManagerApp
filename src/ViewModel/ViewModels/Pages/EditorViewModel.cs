using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.ViewModels.Modals;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.AppStates.Sessions;

using IResourceService = ViewModel.Interfaces.AppStates.IResourceService;

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

        private ISession _session;

        private DialogViewModel<IList<ITask>, bool> _removeTasksDialog;

        private DialogViewModel<ITask, bool> _addTasksDialog;

        private DialogViewModel<object, bool> _editTaskDialog;

        private DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> _moveTasksDialog;

        private DialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?>
            _copyTasksDialog;

        private IFactory<ITaskComposite> _taskCompositeFactory;

        private IFactory<ITaskElementProxy> _taskElementProxyFactory;

        private ITasksEditorProxy _tasksEditorProxy;

        private ITaskElementsEditorProxy _taskElementsEditorProxy;

        [Reactive]
        private IEnumerable<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        public EditorViewModel(ISession session, IResourceService resourceService,
            DialogViewModel<IList<ITask>, bool> removeTasksDialog,
            DialogViewModel<ITask, bool> addTasksDialog,
            DialogViewModel<object, bool> editTaskDialog,
            DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> moveTasksDialog,
            DialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?> copyTasksDialog,
            IFactory<ITaskComposite> taskCompositeFactory,
            IFactory<ITaskElementProxy> taskElementProxyFactory,
            ITasksEditorProxy tasksEditorProxy,
            ITaskElementsEditorProxy taskElementsEditorProxy)
        {
            _session = session;
            _removeTasksDialog = removeTasksDialog;
            _addTasksDialog = addTasksDialog;
            _editTaskDialog = editTaskDialog;
            _moveTasksDialog = moveTasksDialog;
            _copyTasksDialog = copyTasksDialog;
            _taskCompositeFactory = taskCompositeFactory;
            _taskElementProxyFactory = taskElementProxyFactory;
            _tasksEditorProxy = tasksEditorProxy;
            _taskElementsEditorProxy = taskElementsEditorProxy;

            TaskListView = _session.Tasks;
            this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => TaskListView = t);

            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.TaskListView).
                Select(i => TaskListView is ITask).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 && SelectedTasks.First() is ITaskComposite).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteRemove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteAdd = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 0 || (i == 1 && SelectedTasks.First() is ITaskComposite)).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteEdit = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteCopy = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteMove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteSwap = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 2 &&
                SelectedTasks.First().ParentTask == SelectedTasks.Last().ParentTask).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);

            Metadata = resourceService.GetResource("EditorPageMetadata");
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _session.Tasks;
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
            var result = await AddModal(_removeTasksDialog, items);
            if (result)
            {
                _session.RemoveTasks(items);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskElement() => await AddTask(_taskElementProxyFactory.Create());

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskComposite() => await AddTask(_taskCompositeFactory.Create());

        private async Task AddTask(ITask task)
        {
            var taskComposite = TaskListView as ITaskComposite;
            if (SelectedTasks.Count == 1)
            {
                taskComposite = (ITaskComposite)SelectedTasks.First();
                SelectedTasks.Clear();
            }
            var result = await AddModal(_addTasksDialog, task);
            if (result)
            {
                _session.AddTasks([task is IProxy<ITask> proxy ? proxy.Target : task],
                    taskComposite);
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
                var taskElementsEditorService = _taskElementsEditorProxy;
                taskElementsEditorService.Target = taskElement;
                editorService = taskElementsEditorService;
            }
            else
            {
                var tasksEditorService = _tasksEditorProxy;
                tasksEditorService.Target = item;
                editorService = tasksEditorService;
            }
            var result = await AddModal(_editTaskDialog, editorService);
            if (result)
            {
                editorService.ApplyChanges();
                _session.EditTask(item);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private async Task Move()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var list = await AddModal(_moveTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _session.Tasks));
            if (list != null)
            {
                _session.MoveTasks(items, (ITaskComposite?)list);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteCopy))]
        private async Task Copy()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var result = await AddModal(_copyTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _session.Tasks));
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
                _session.AddTasks(copyList, result.List as ITaskComposite);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteSwap))]
        private void Swap()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();

            var item1 = items.First();
            var item2 = items.Last();

            var list = item1.ParentTask ?? _session.Tasks as IList<ITask>;

            var index1 = list.IndexOf(item1);
            var index2 = list.IndexOf(item2);

            list[index1] = item2;
            list[index2] = item1;
        }
    }
}
