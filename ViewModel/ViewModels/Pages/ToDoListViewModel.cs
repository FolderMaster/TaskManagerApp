using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;
using ViewModel.Technicals;

namespace ViewModel.ViewModels.Pages
{
    public partial class ToDoListViewModel : PageViewModel
    {
        private readonly Session _session;

        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        public ToDoListViewModel(object metadata, Session session) : base(metadata)
        {
            _session = session;

            this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => Update());
        }

        [ReactiveCommand]
        public void Update()
        {
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));
            ToDoList = uncompletedTasks.OrderBy(t => t.Difficult).
                OrderBy(t => t.Priority).OrderBy(t => t.PlannedTime - t.SpentTime).Select
                (t => new ToDoListElement(t, t.Deadline + (t.PlannedTime - t.SpentTime) <
                DateTime.Now, t.Deadline < DateTime.Now));
        }
    }
}
