using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using ViewModel.Technicals;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;

namespace ViewModel.ViewModels.Pages
{
    /// <summary>
    /// Класс контроллера страницы статистики.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BasePageViewModel"/>.
    /// </remarks>
    public partial class StatisticViewModel : BasePageViewModel
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        private IResourceService _resourceService;

        /// <summary>
        /// Список возможных временных интервалов.
        /// </summary>
        [Reactive]
        private IEnumerable<TimeSpan> _times =
        [
            new TimeSpan(1, 0, 0, 0),  // День.
            new TimeSpan(7, 0, 0, 0),  // Неделя.
            new TimeSpan(30, 0, 0, 0), // Месяц.
            new TimeSpan(365, 0, 0, 0) // Год.
        ];

        /// <summary>
        /// Выбранный временной интервал
        /// </summary>
        [Reactive]
        private TimeSpan? _selectedTime;

        /// <summary>
        /// Статистика по количеству невыполненных задач по категориям.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByCategoryStatistic;

        /// <summary>
        /// Статистика по количеству невыполненных задач по тегам.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByTagsStatistic;

        /// <summary>
        /// Статистика по количеству невыполненных задач по приоритетам.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByPriorityStatistic;

        /// <summary>
        /// Статистика по количеству невыполненных задач по сложности.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByDifficultStatistic;

        /// <summary>
        /// Статистика по просроченным задачам.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _expiredTasksStatistic;

        /// <summary>
        /// Статистика по времени задач.
        /// </summary>
        [Reactive]
        private IEnumerable<StatisticElement> _tasksTimeStatistic;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StatisticViewModel"/>.
        /// </summary>
        /// <param name="session">Сессия.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        public StatisticViewModel(ISession session, IResourceService resourceService)
        {
            _session = session;
            _resourceService = resourceService;

            this.WhenAnyValue(x => x.Times).Subscribe(s => SelectedTime = s?.FirstOrDefault());
            this.WhenAnyValue(x => x.SelectedTime).Subscribe(t => UpdateExpiredTasksStatistics());

            Metadata = _resourceService.GetResource("StatisticPageMetadata");
            _session.ItemsUpdated += Session_ItemsUpdated;
        }

        /// <summary>
        /// Обновляет статистику.
        /// </summary>
        [ReactiveCommand]
        private void Update()
        {
            UpdateTasksCountStatistics();
            UpdateExpiredTasksStatistics();
            UpdateTimeTasksStatistic();
        }

        /// <summary>
        /// Обновляет статистику по количеству невыполненных задач.
        /// </summary>
        private void UpdateTasksCountStatistics()
        {
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var difficultDiagramContent = _resourceService.GetResource("DifficultDiagramContent");
            var priorityDiagramContent = _resourceService.GetResource("PriorityDiagramContent");

            UncompletedTasksCountByCategoryStatistic = uncompletedTasks.
                GroupBy(t => ((TaskMetadata)t.Metadata).Category).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByTagsStatistic = uncompletedTasks.
                SelectMany(t => ((TaskMetadata)t.Metadata).Tags, (task, tag) =>
                new { Task = task, Tag = tag }).GroupBy(e => e.Tag).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByPriorityStatistic = uncompletedTasks.GroupBy(t => t.Priority).
                Select(g => new StatisticElement(g.Count(), $"{priorityDiagramContent} {g.Key}"));
            UncompletedTasksCountByDifficultStatistic = uncompletedTasks.GroupBy(t => t.Difficult).
                Select(g => new StatisticElement(g.Count(), $"{difficultDiagramContent} {g.Key}"));
        }

        /// <summary>
        /// Обновляет статистику по просроченным задачам.
        /// </summary>
        private void UpdateExpiredTasksStatistics()
        {
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
            var expiredTasks = tasks.Where(t => !TaskHelper.HasTaskExpired(t) &&
                TaskHelper.HasTaskExpired(t, SelectedTime));

            var count = expiredTasks.Count();
            var plannedTime = expiredTasks.Aggregate(TimeSpan.Zero,
                (sum, task) => sum + task.PlannedTime).Hours;
            var spentTime = expiredTasks.Aggregate(TimeSpan.Zero,
                (sum, task) => sum + task.SpentTime).Hours;

            var countDiagramContent = _resourceService.GetResource("CountDiagramContent");
            var plannedTimeDiagramContent = _resourceService.GetResource("PlannedTimeDiagramContent");
            var spentTimeDiagramContent = _resourceService.GetResource("SpentTimeDiagramContent");

            ExpiredTasksStatistic =
            [
                new StatisticElement(count, countDiagramContent?.ToString()),
                new StatisticElement(plannedTime, plannedTimeDiagramContent?.ToString()),
                new StatisticElement(spentTime, spentTimeDiagramContent?.ToString())
            ];
        }

        /// <summary>
        /// Обновляет статистику по времени.
        /// </summary>
        private void UpdateTimeTasksStatistic()
        {
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var plannedTime = uncompletedTasks.Aggregate(TimeSpan.Zero,
                (sum, task) => sum + task.TimeIntervals.Duration).Hours;
            var unplannedTime = uncompletedTasks.Aggregate(TimeSpan.Zero,
                (sum, task) => sum + task.PlannedTime).Hours - plannedTime;

            var plannedTimeDiagramContent = _resourceService.GetResource("PlannedTimeDiagramContent");
            var unplannedTimeDiagramContent = _resourceService.GetResource("UnplannedTimeDiagramContent");

            TasksTimeStatistic =
            [
                new StatisticElement(plannedTime, plannedTimeDiagramContent?.ToString()),
                new StatisticElement(unplannedTime, unplannedTimeDiagramContent?.ToString())
            ];
        }

        private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();
    }
}
