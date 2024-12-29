using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using ViewModel.Technicals;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates;
using ViewModel.Implementations.ModelLearning;

namespace ViewModel.ViewModels.Pages
{
    public partial class ToDoListViewModel : PageViewModel
    {
        private ISession _session;

        private ExecutionChanceTaskElementEvaluatorLearningController _progressLearningController;

        [Reactive]
        private bool _isLaggingFilter;

        [Reactive]
        private bool _isExpiredFilter;

        [Reactive]
        private bool _isExecutionChanceSort;

        [Reactive]
        private bool _isRealSort;

        [Reactive]
        private bool _isTimeSort;

        [Reactive]
        private bool _isDifficultSort;

        [Reactive]
        private bool _isPrioritySort;

        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        public ToDoListViewModel(ISession session, IResourceService resourceService,
            ExecutionChanceTaskElementEvaluatorLearningController progressLearningController)
        {
            _session = session;
            _progressLearningController = progressLearningController;

            this.WhenAnyValue(x => x.IsLaggingFilter).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsExpiredFilter).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsDifficultSort).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsPrioritySort).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsTimeSort).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsRealSort).Subscribe(b => Update());
            this.WhenAnyValue(x => x.IsExecutionChanceSort).Subscribe(b => Update());

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
            var tasks = TaskHelper.GetTaskElements(_session.Tasks.ToList());
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));
            var toDoList = uncompletedTasks.Select(t =>
                new ToDoListElement(t, _progressLearningController.IsValidModel ?
                _progressLearningController.Predict(t) : null, t.Deadline +
                (t.PlannedTime - t.SpentTime) < DateTime.Now, t.Deadline < DateTime.Now));
            if (IsLaggingFilter)
            {
                toDoList = toDoList.Where(e => e.IsLagging);
            }
            if (IsExpiredFilter)
            {
                toDoList = toDoList.Where(e => e.IsExpired);
            }
            if (IsTimeSort)
            {
                toDoList = toDoList.OrderBy(e =>
                    e.TaskElement.PlannedTime - e.TaskElement.SpentTime);
            }
            if (IsRealSort)
            {
                toDoList = toDoList.OrderBy(e =>
                    e.TaskElement.PlannedReal - e.TaskElement.ExecutedReal);
            }
            if (IsExecutionChanceSort)
            {
                toDoList = toDoList.OrderBy(e => e.ExecutionChance);
            }
            if (IsDifficultSort)
            {
                toDoList = toDoList.OrderBy(e => e.TaskElement.Difficult);
            }
            if (IsPrioritySort)
            {
                toDoList = toDoList.OrderBy(e => e.TaskElement.Priority);
            }
            ToDoList = toDoList;
        }

        private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();
    }
}
