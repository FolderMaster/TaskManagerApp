using ReactiveUI.SourceGenerators;
using ReactiveUI;

using ViewModel.Technicals;
using Model;

namespace ViewModel.ViewModels.Pages
{
    public partial class StatisticViewModel : PageViewModel
    {
        private IList<ITask> _mainTaskList;

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

        public StatisticViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
        {
            _mainTaskList = mainTaskList;

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
            var tasks = GetAllTasks(_mainTaskList);
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
            var tasks = GetAllTasks(_mainTaskList);
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
            var tasks = GetAllTasks(_mainTaskList);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var plannedTime = new TimeSpan(uncompletedTasks.Sum(t => t.PlannedTime.Ticks)).Hours;
            var spentTime = new TimeSpan(uncompletedTasks.Sum(t => t.SpentTime.Ticks)).Hours;

            TasksTimeStatistic =
            [
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }

        private IEnumerable<ITaskElement> GetAllElements(IEnumerable<ITask> taskList)
        {
            return GetAllTasks(taskList).OfType<ITaskElement>();
        }

        private IEnumerable<ITaskComposite> GetAllComposites(IEnumerable<ITask> taskList)
        {
            return GetAllTasks(taskList).OfType<ITaskComposite>();
        }

        private IEnumerable<ITask> GetAllTasks(IEnumerable<ITask> taskList)
        {
            foreach (var task in taskList)
            {
                yield return task;

                if (task is IEnumerable<ITask> sublist)
                {
                    foreach (var subtask in GetAllTasks(sublist))
                    {
                        yield return subtask;
                    }
                }
            }
        }
    }
}
