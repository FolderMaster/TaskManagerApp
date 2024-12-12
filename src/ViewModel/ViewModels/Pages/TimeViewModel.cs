using ReactiveUI.SourceGenerators;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using DynamicData;

using Model;
using Model.Interfaces;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.Interfaces.AppStates.Events;
using ViewModel.Implementations.AppStates;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly IObservable<bool> _canExecuteRemove;

    private readonly IObservable<bool> _canExecuteEdit;

    private readonly AppState _appState;

    private Dictionary<DateTime, IEnumerable<ITaskElement>> _tasksSchedulerDictionary;

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new ObservableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    [Reactive]
    private DateTime _currentWeek = DateTime.Now;

    public TimeViewModel(AppState appState)
    {
        _appState = appState;

        _canExecuteRemove = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null);
        _canExecuteEdit = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null);

        Metadata = _appState.Services.ResourceService.GetResource("TimePageMetadata");
        _appState.Session.ItemsUpdated += Session_ItemsUpdated;
        _appState.Services.TimeScheduler.TimepointReached += TimeScheduler_TimepointReached;
    }

    [ReactiveCommand]
    private void Update()
    {
        if (_appState.Session.Tasks == null)
        {
            return;
        }
        CalendarIntervals.Clear();
        var tasks = TaskHelper.GetTaskElements(_appState.Session.Tasks);
        foreach (var task in tasks)
        {
            foreach(var timeInterval in task.TimeIntervals)
            {
                CalendarIntervals.Add(new CalendarInterval(timeInterval, task));
            }
        }
        _tasksSchedulerDictionary = tasks.SelectMany(t => t.TimeIntervals).GroupBy(i => i.Start).
            Where(g => g.Key > DateTime.Now).ToDictionary(g => g.Key,
            g => tasks.Where(task => task.TimeIntervals.Any(i => i.Start == g.Key)));
        _appState.Services.TimeScheduler.Timepoints.Clear();
        _appState.Services.TimeScheduler.Timepoints.AddRange
            (_tasksSchedulerDictionary.Keys);
    }

    [ReactiveCommand]
    private void GoToNext() => CurrentWeek = CurrentWeek.AddDays(7);

    [ReactiveCommand]
    private void GoToNow() => CurrentWeek = DateTime.Now;

    [ReactiveCommand]
    private void GoToPrevious() => CurrentWeek = CurrentWeek.AddDays(-7);

    [ReactiveCommand]
    private async Task Add()
    {
        var timeIntervalElement = _appState.Services.TimeIntervalElementFactory.Create();
        var result = await AddModal(_appState.Services.AddTimeIntervalDialog,
            new TimeIntervalViewModelArgs(_appState.Session.Tasks,
            _appState.Session.Tasks, timeIntervalElement));
        if (result != null)
        {
            _appState.Session.AddTimeInterval(result.TimeIntervalElement, result.TaskElement);
        }
    }

    [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
    private void Remove()
    {
        _appState.Session.RemoveTimeInterval(SelectedCalendarInterval.TimeInterval,
            SelectedCalendarInterval.TaskElement);
    }

    [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
    private async Task Edit()
    {
        var timeInterval = SelectedCalendarInterval.TimeInterval;
        var editorService = _appState.Services.TimeIntervalElementsEditorProxy;
        editorService.Target = timeInterval;
        var result = await AddModal(_appState.Services.EditTimeIntervalDialog, editorService);
        if (result)
        {
            editorService.ApplyChanges();
            _appState.Session.EditTimeInterval(timeInterval);
        }
    }

    private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();

    private void TimeScheduler_TimepointReached(object? sender, DateTime e)
    {
        var titleResource = _appState.Services.ResourceService.
            GetResource("TimeSchedulerNotificationTitle");
        var contentResource = _appState.Services.ResourceService.
            GetResource("TimeSchedulerNotificationContent");
        
        var content = $"{contentResource}\n";
        foreach (var task in _tasksSchedulerDictionary[e])
        {
            content += $"- {task.Metadata}\n";
        }
        _appState.Services.NotificationManager.SendNotification(content, $"{titleResource}");
    }
}
