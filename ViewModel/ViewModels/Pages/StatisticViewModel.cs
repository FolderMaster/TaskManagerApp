using ReactiveUI.SourceGenerators;
using ReactiveUI;

using ViewModel.Technicals;
using Model;

namespace ViewModel.ViewModels.Pages
{
    public partial class StatisticViewModel : PageViewModel
    {
        private readonly Session _session;

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

        public StatisticViewModel(object metadata, Session session) : base(metadata)
        {
            _session = session;

            this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => Update());
            this.WhenAnyValue(x => x.Times).Subscribe(s => SelectedTime = s?.FirstOrDefault());
            this.WhenAnyValue(x => x.SelectedTime).Subscribe(t => UpdateExpiredTasksStatistics());
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
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
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
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
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
            if (_session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var plannedTime = new TimeSpan(uncompletedTasks.Sum(t => t.PlannedTime.Ticks)).Hours;
            var spentTime = new TimeSpan(uncompletedTasks.Sum(t => t.SpentTime.Ticks)).Hours;

            TasksTimeStatistic =
            [
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }
    }
}
