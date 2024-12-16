using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using ViewModel.Technicals;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates;

namespace ViewModel.ViewModels.Pages
{
    public partial class ToDoListViewModel : PageViewModel
    {
        private ISession _session;

        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        public ToDoListViewModel(ISession session, IResourceService resourceService)
        {
            _session = session;

            Metadata = resourceService.GetResource("ToDoListPageMetadata");
            _session.ItemsUpdated += Session_ItemsUpdated;
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

        private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();
    }
}
