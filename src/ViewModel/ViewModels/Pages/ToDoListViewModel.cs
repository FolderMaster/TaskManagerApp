using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using ViewModel.Technicals;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates;
using ViewModel.Implementations.ModelLearning;

namespace ViewModel.ViewModels.Pages
{
    /// <summary>
    /// Класс контроллера страницы списка задач для выполнения.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BasePageViewModel"/>.
    /// </remarks>
    public partial class ToDoListViewModel : BasePageViewModel
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// Контроллер обучения модели выполнения шанса.
        /// </summary>
        private ExecutionChanceTaskElementEvaluatorLearningController _progressLearningController;

        /// <summary>
        /// Флаг для фильтрации задач по отставанию.
        /// </summary>
        [Reactive]
        private bool _isLaggingFilter;

        /// <summary>
        /// Флаг для фильтрации задач по истечению срока.
        /// </summary>
        [Reactive]
        private bool _isExpiredFilter;

        /// <summary>
        /// Флаг для сортировки задач по вероятности выполнения.
        /// </summary>
        [Reactive]
        private bool _isExecutionChanceSort;

        /// <summary>
        /// Флаг для сортировки задач по реальному времени.
        /// </summary>
        [Reactive]
        private bool _isRealSort;

        /// <summary>
        /// Флаг для сортировки задач по времени.
        /// </summary>
        [Reactive]
        private bool _isTimeSort;

        /// <summary>
        /// Флаг для сортировки задач по сложности.
        /// </summary>
        [Reactive]
        private bool _isDifficultSort;

        /// <summary>
        /// Флаг для сортировки задач по приоритету.
        /// </summary>
        [Reactive]
        private bool _isPrioritySort;

        /// <summary>
        /// Список задач для выполнения.
        /// </summary>
        [Reactive]
        private IEnumerable<ToDoListElement>? _toDoList;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ToDoListViewModel"/>.
        /// </summary>
        /// <param name="session">Сессия.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="progressLearningController">
        /// Контроллер обучения модели выполнения шанса.
        /// </param>
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

        /// <summary>
        /// Обновляет список задач для выполнения.
        /// </summary>
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
                _progressLearningController.Predict(t) : null, t.Deadline < DateTime.Now +
                (t.PlannedTime - t.SpentTime), t.Deadline < DateTime.Now));
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
