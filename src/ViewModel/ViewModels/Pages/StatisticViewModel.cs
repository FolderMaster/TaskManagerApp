using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;

using MachineLearning;
using MachineLearning.LearningModels;

using ViewModel.Technicals;
using ViewModel.AppStates;
using ViewModel.Interfaces.Events;

namespace ViewModel.ViewModels.Pages
{
    public partial class StatisticViewModel : PageViewModel
    {
        private readonly AppState _appState;

        private readonly KMeanLearningModel _learningModel = new();

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

        [Reactive]
        private IEnumerable<GroupPoint> _clustersElements;

        public StatisticViewModel(AppState appState)
        {
            _appState = appState;

            this.WhenAnyValue(x => x.Times).Subscribe(s => SelectedTime = s?.FirstOrDefault());
            this.WhenAnyValue(x => x.SelectedTime).Subscribe(t => UpdateExpiredTasksStatistics());

            Metadata = _appState.Services.ResourceService.GetResource("StatisticPageMetadata");
            _appState.Session.ItemsUpdated += Session_ItemsUpdated;
        }

        [ReactiveCommand]
        private void Update()
        {
            UpdateTasksCountStatistics();
            UpdateExpiredTasksStatistics();
            UpdateTimeTasksStatistic();
        }

        [ReactiveCommand]
        private async Task Predict()
        {
            if (_appState.Session.Tasks == null)
            {
                return;
            }
            _learningModel.NumbersOfClusters = 3;
            var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
            var inputs = tasks.Select(t => new double[] {t.Difficult, t.Priority, (int)t.Status});
            await _learningModel.Train(inputs);
            var clusters = _learningModel.Predict(inputs);
            ClustersElements = tasks.Zip(clusters).Select(g => new GroupPoint(g.First.Priority,
                g.First.Difficult, g.First.Metadata.ToString(), g.Second));
        }

        private void UpdateTasksCountStatistics()
        {
            if (_appState.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            UncompletedTasksCountByCategoryStatistic = uncompletedTasks.
                GroupBy(t => ((Metadata)t.Metadata).Category).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByTagsStatistic = uncompletedTasks.
                SelectMany(t => ((Metadata)t.Metadata).Tags, (task, tag) =>
                new { Task = task, Tag = tag }).GroupBy(e => e.Tag).
                Select(g => new StatisticElement(g.Count(), $"{g.Key}"));
            UncompletedTasksCountByPriorityStatistic = uncompletedTasks.GroupBy(t => t.Priority).
                Select(g => new StatisticElement(g.Count(), $"Priority {g.Key}"));
            UncompletedTasksCountByDifficultStatistic = uncompletedTasks.GroupBy(t => t.Difficult).
                Select(g => new StatisticElement(g.Count(), $"Difficult {g.Key}"));
        }

        private void UpdateExpiredTasksStatistics()
        {
            if (_appState.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
            var where = tasks.Where(t => !TaskHelper.HasTaskExpired(t) &&
                TaskHelper.HasTaskExpired(t, SelectedTime));

            var count = where.Count();
            var plannedTime = tasks.Aggregate(TimeSpan.Zero,
                (sum, interval) => sum + interval.PlannedTime).Hours;
            var spentTime = tasks.Aggregate(TimeSpan.Zero,
                (sum, interval) => sum + interval.SpentTime).Hours;

            ExpiredTasksStatistic =
            [
                new StatisticElement(count, "Count"),
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }

        private void UpdateTimeTasksStatistic()
        {
            if (_appState.Session.Tasks == null)
            {
                return;
            }
            var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
            var uncompletedTasks = tasks.Where(t => !TaskHelper.IsTaskCompleted(t));

            var plannedTime = uncompletedTasks.Aggregate(TimeSpan.Zero,
                (sum, interval) => sum + interval.PlannedTime).Hours;
            var spentTime = uncompletedTasks.Aggregate(TimeSpan.Zero,
                (sum, interval) => sum + interval.SpentTime).Hours;

            TasksTimeStatistic =
            [
                new StatisticElement(plannedTime, "PlannedTime"),
                new StatisticElement(spentTime, "SpentTime")
            ];
        }

        private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();
    }
}
