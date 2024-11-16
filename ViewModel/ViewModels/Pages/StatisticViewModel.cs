using ReactiveUI.SourceGenerators;
using ReactiveUI;

using ViewModel.Technicals;
using Model;
using ViewModel.AppState;

namespace ViewModel.ViewModels.Pages
{
    public partial class StatisticViewModel : PageViewModel
    {
        private readonly AppStateManager _appStateManager;

        [Reactive]
        private IEnumerable<TimeSpan> _times =
        [
            new TimeSpan(1, 0, 0, 0),
            new TimeSpan(7, 0, 0, 0),
            new TimeSpan(30, 0, 0, 0),
            new TimeSpan(365, 0, 0, 0)
        ];

        [Reactive]
        private TimeSpan? _selectedTime;

        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByCategoryStatistic;

        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByTagsStatistic;

        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByPriorityStatistic;

        [Reactive]
        private IEnumerable<StatisticElement> _uncompletedTasksCountByDifficultStatistic;

        [Reactive]
        private IEnumerable<StatisticElement> _expiredTasksStatistic;

        [Reactive]
        private IEnumerable<StatisticElement> _tasksTimeStatistic;

        public StatisticViewModel(object metadata, AppStateManager appStateManager) : base(metadata)
        {
            _appStateManager = appStateManager;

            this.WhenAnyValue(x => x.Times).Subscribe(s => SelectedTime = s?.FirstOrDefault());
            this.WhenAnyValue(x => x.SelectedTime).Subscribe(t => UpdateExpiredTasksStatistics());

            _appStateManager.ItemSessionChanged += AppStateManager_ItemSessionChanged;
        }

        [ReactiveCommand]
        private void Update()
        {
            UpdateTasksCountStatistics();
            UpdateExpiredTasksStatistics();
            UpdateTimeTasksStatistic();
        }

        private void UpdateTasksCountStatistics()
        {
            if (_appStateManager.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appStateManager.Session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            UncompletedTasksCountByCategoryStatistic = uncompletedTasks.
                GroupBy(t => ((Metadata)t.Metadata).Category).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByTagsStatistic = uncompletedTasks.
                SelectMany(t => ((Metadata)t.Metadata).Tags, (task, tag) => new { Task = task, Tag = tag }).
                GroupBy(e => e.Tag).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByPriorityStatistic = uncompletedTasks.GroupBy(t => t.Priority).
                Select(g => new StatisticElement(g.Count(), $"Priority {g.Key}"));
            UncompletedTasksCountByDifficultStatistic = uncompletedTasks.GroupBy(t => t.Difficult).
                Select(g => new StatisticElement(g.Count(), $"Difficult {g.Key}"));
        }

        private void UpdateExpiredTasksStatistics()
        {
            if (_appStateManager.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appStateManager.Session.Tasks);
            var where = tasks.Where(t => !TaskHelper.HasTaskExpired(t) &&
                TaskHelper.HasTaskExpired(t, SelectedTime));

            var count = where.Count();
            var plannedTime = new TimeSpan(tasks.Sum(t => t.PlannedTime.Ticks)).Hours;
            var spentTime = new TimeSpan(tasks.Sum(t => t.SpentTime.Ticks)).Hours;

            ExpiredTasksStatistic =
            [
                new StatisticElement(count, "Count"),
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }

        private void UpdateTimeTasksStatistic()
        {
            if (_appStateManager.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appStateManager.Session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var plannedTime = new TimeSpan(uncompletedTasks.Sum(t => t.PlannedTime.Ticks)).Hours;
            var spentTime = new TimeSpan(uncompletedTasks.Sum(t => t.SpentTime.Ticks)).Hours;

            TasksTimeStatistic =
            [
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }

        private void AppStateManager_ItemSessionChanged(object? sender, object e) =>
            UpdateCommand.Execute();
    }
}
