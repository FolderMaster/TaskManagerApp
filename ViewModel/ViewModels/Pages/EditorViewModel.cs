using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model;
using ViewModel.ViewModels.Modals;
using ViewModel.Technicals;

namespace ViewModel.ViewModels.Pages
{
    public partial class EditorViewModel : PageViewModel
    {
        private readonly IObservable<bool> _canExecuteGoToPrevious;

        private readonly IObservable<bool> _canExecuteRemove;

        private readonly IObservable<bool> _canExecuteAdd;

        private readonly IObservable<bool> _canExecuteEdit;

        private readonly IObservable<bool> _canExecuteMove;

        private readonly IObservable<bool> _canExecuteGo;

        private AddTaskViewModel _addDialog = new();

        private RemoveTasksViewModel _removeDialog = new();

        private MoveTasksViewModel _moveDialog = new();

        private EditTaskViewModel _editDialog = new();

        private MetadataFactory _metadataFactory = new();

        private TaskCompositeFactory _taskCompositeFactory;

        private TaskElementFactory _taskElementFactory;

        private readonly Session _session;

        [Reactive]
        private IList<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        public EditorViewModel(object metadata, Session session) : base(metadata)
        {
            _session = session;
            TaskListView = session.Tasks;

            _taskElementFactory = new(_metadataFactory);
            _taskCompositeFactory = new(_metadataFactory);

            this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => TaskListView = t);

            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.TaskListView).
                Select(i => TaskListView is ITask);
            _canExecuteRemove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteAdd = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 0 || (i == 1 && SelectedTasks.First() is ITaskComposite));
            _canExecuteEdit = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1);
            _canExecuteMove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 && SelectedTasks.First() is ITaskComposite);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _session.Tasks;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
        private async Task Remove()
        {
            var items = SelectedTasks.ToList();
            _removeDialog.Items = items;
            _removeDialog.MainList = _session.Tasks;
            SelectedTasks.Clear();
            var result = await AddModal(_removeDialog);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskElement() => await AddTask(_taskElementFactory.Create());

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskComposite() => await AddTask(_taskCompositeFactory.Create());

        private async Task AddTask(ITask task)
        {
            var list = TaskListView;
            if (SelectedTasks.Count == 1)
            {
                list = (ITaskComposite)SelectedTasks.First();
                SelectedTasks.Clear();
            }
            _addDialog.List = list;
            _addDialog.Item = task;
            var result = await AddModal(_addDialog);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
        private async Task Edit()
        {
            _editDialog.Item = SelectedTasks.First();
            SelectedTasks.Clear();
            var result = await AddModal(_editDialog);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private async Task Move()
        {
            _moveDialog.Items = SelectedTasks.ToList();
            _moveDialog.List = TaskListView;
            _moveDialog.MainList = _session.Tasks;
            SelectedTasks.Clear();
            var result = await AddModal(_moveDialog);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTasks.First();
            SelectedTasks.Clear();
            TaskListView = composite;
        }
    }
}
