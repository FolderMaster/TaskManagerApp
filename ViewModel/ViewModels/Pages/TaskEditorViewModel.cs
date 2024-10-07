using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model;
using ViewModel.ViewModels.Modals;

namespace ViewModel.ViewModels.Pages
{
    public partial class TaskEditorViewModel : PageViewModel
    {
        private readonly IObservable<bool> _canExecuteGoToPrevious;

        private readonly IObservable<bool> _canExecuteRemove;

        private readonly IObservable<bool> _canExecuteAdd;

        private readonly IObservable<bool> _canExecuteEdit;

        private readonly IObservable<bool> _canExecuteMove;

        private readonly IObservable<bool> _canExecuteGo;

        private IList<ITask> _mainTaskList;

        [Reactive]
        private IList<ITask> _taskListView;

        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        [Reactive]
        public AddViewModel _modal = new AddViewModel();

        public TaskEditorViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
        {
            _mainTaskList = mainTaskList;
            TaskListView = _mainTaskList;

            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.TaskListView).
                Select(i => TaskListView is ITask);
            _canExecuteRemove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0);
            _canExecuteAdd = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 0 || (i == 1 && SelectedTasks.First() is ITaskComposite));
            _canExecuteEdit = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1);
            _canExecuteMove = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 || (i == 2 && SelectedTasks.Last() is ITaskComposite));
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 && SelectedTasks.First() is ITaskComposite);
        }

        public TaskEditorViewModel() : this("Task tree", new ObservableCollection<ITask>()) { }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _mainTaskList;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
        private void Remove()
        {
            foreach (var task in SelectedTasks.ToList())
            {
                if (task.ParentTask == null)
                {
                    _mainTaskList.Remove(task);
                }
                else
                {
                    task.ParentTask.Remove(task);
                }
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private void AddTaskElement() => AddTask(new TaskElement("Task element"));

        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private void AddTaskComposite() => AddTask(new TaskComposite("Task composite"));

        private void AddTask(ITask task)
        {
            if (SelectedTasks.Count == 1)
            {
                var composite = (ITaskComposite)SelectedTasks.First();
                composite.Add(task);
            }
            else
            {
                TaskListView.Add(task);
            }
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
        private async Task Edit()
        {
            Modal.Value = SelectedTasks.First();
            var result = await Modal.Invoke();
            SelectedTasks.Clear();
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private void Move()
        {
            var task = SelectedTasks.First();
            if (task.ParentTask != null)
            {
                task.ParentTask.Remove(task);
            }
            else
            {
                _mainTaskList.Remove(task);
            }
            if (SelectedTasks.Count == 0)
            {
                TaskListView.Add(task);
            }
            else
            {
                var composite = (ITaskComposite)SelectedTasks.Last();
                composite.Add(task);
            }
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
