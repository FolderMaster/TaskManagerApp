using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;
using ViewModel.Technicals;

namespace ViewModel.ViewModels.Pages
{
    public partial class ToDoListViewModel : PageViewModel
    {
        private IList<ITask> _mainTaskList;

        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        public ToDoListViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
        {
            _mainTaskList = mainTaskList;
        }

        [ReactiveCommand]
        public void Update()
        {
            var tasks = TaskHelper.GetTaskElements(_mainTaskList);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));
            ToDoList = uncompletedTasks.OrderBy(t => t.Difficult).
                OrderBy(t => t.Priority).OrderBy(t => t.PlannedTime - t.SpentTime).Select
                (t => new ToDoListElement(t, t.Deadline + (t.PlannedTime - t.SpentTime) <
                DateTime.Now, t.Deadline < DateTime.Now));
        }
    }
}
