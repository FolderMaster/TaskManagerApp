using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

using Model;

namespace ViewModel.ViewModels.Pages
{
    public class TaskEditorViewModel : PageViewModel
    {
        private IList<ITask> _mainTaskList;

        private IList<ITask> _taskListView;

        private ITask? _editedTask;

        public ITask? EditedTask
        {
            get => _editedTask;
            private set => this.RaiseAndSetIfChanged(ref _editedTask, value);
        }

        public IList<ITask> TaskListView
        {
            get => _taskListView;
            private set => this.RaiseAndSetIfChanged(ref _taskListView, value);
        }

        public IList<ITask> SelectedTasks { get; }

        public ReactiveCommand<Unit, Unit> GoToPreviousCommand { get; }

        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

        public ReactiveCommand<Unit, Unit> AddTaskElementCommand { get; }

        public ReactiveCommand<Unit, Unit> AddTaskCompositeCommand { get; }

        public ReactiveCommand<Unit, Unit> EditCommand { get; }

        public ReactiveCommand<Unit, Unit> MoveCommand { get; }

        public ReactiveCommand<Unit, Unit> GoCommand { get; }

        public TaskEditorViewModel(object metadata) : base(metadata)
        {
            _mainTaskList = new ObservableCollection<ITask>() 
            {
                new TaskComposite("TaskComposite1", []),
                new TaskComposite("TaskComposite2")
                {
                    new TaskElement("Task1"),
                    new TaskElement("Task2")
                },
                new TaskElement("Task1")
                {
                    Progress = 0.75
                }
            };
            TaskListView = _mainTaskList;
            SelectedTasks = new ObservableCollection<ITask>();
            GoToPreviousCommand = ReactiveCommand.Create(GoToPrevious,
                this.WhenAnyValue(x => x.TaskListView).Select(i => TaskListView is ITask));
            RemoveCommand = ReactiveCommand.Create(Remove,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0));
            AddTaskElementCommand = ReactiveCommand.Create(AddTaskElement,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 0 ||
                (i == 1 && SelectedTasks.First() is ITaskComposite)));
            AddTaskCompositeCommand = ReactiveCommand.Create(AddTaskComposite,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 0 ||
                (i == 1 && SelectedTasks.First() is ITaskComposite)));
            EditCommand = ReactiveCommand.Create(Edit,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1));
            MoveCommand = ReactiveCommand.Create(Move,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1 ||
                (i == 2 && SelectedTasks.Last() is ITaskComposite)));
            GoCommand = ReactiveCommand.Create(Go,
                this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1 &&
                SelectedTasks.First() is ITaskComposite));
        }

        public TaskEditorViewModel() : this("Task tree") { }

        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _mainTaskList;
        }

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

        private void AddTaskElement() => AddTask(new TaskElement("Task element"));

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

        private void Edit()
        {
            EditedTask = SelectedTasks.First();
            SelectedTasks.Clear();
        }

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

        private void Go()
        {
            var composite = (ITaskComposite)SelectedTasks.First();
            TaskListView = composite;
        }
    }
}
