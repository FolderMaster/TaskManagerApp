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

        private AddViewModel _addDialog = new();

        private RemoveViewModel _removeDialog = new();

        private MoveViewModel _moveDialog = new();

        private EditViewModel _editDialog = new();

        private MetadataFactory _metadataFactory = new();

        private TaskCompositeFactory _taskCompositeFactory;

        private TaskElementFactory _taskElementFactory;

        private IList<ITask> _mainTaskList;

        [Reactive]
        private IList<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        public EditorViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
        {
            _mainTaskList = mainTaskList;
            TaskListView = _mainTaskList;

            _taskElementFactory = new(_metadataFactory);
            _taskCompositeFactory = new(_metadataFactory);

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
            TaskListView = composite.ParentTask ?? _mainTaskList;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
        private async Task Remove()
        {
            var items = SelectedTasks.ToList();
            _removeDialog.Items = items;
            _removeDialog.MainList = _mainTaskList;
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
            _moveDialog.MainList = _mainTaskList;
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
