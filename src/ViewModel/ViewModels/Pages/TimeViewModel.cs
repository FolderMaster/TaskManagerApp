using ReactiveUI.SourceGenerators;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using DynamicData;

using Model;
using Model.Interfaces;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.Interfaces;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.DataManagers;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly IObservable<bool> _canExecuteRemove;

    private readonly IObservable<bool> _canExecuteEdit;

    private ISession _session;

    private IResourceService _resourceService;

    private ITimeScheduler _timeScheduler;

    private INotificationManager _notificationManager;

    private DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>
        _addTimeIntervalDialog;

    private DialogViewModel<ITimeIntervalElement, bool> _editTimeIntervalDialog;

    private IFactory<ITimeIntervalElement> _timeIntervalElementFactory;

    private ITimeIntervalElementsEditorProxy _timeIntervalElementsEditorProxy;

    private Dictionary<DateTime, IEnumerable<ITaskElement>> _tasksSchedulerDictionary;

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new ObservableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    [Reactive]
    private DateTime _currentWeek = DateTime.Now;

    public TimeViewModel(ISession session, IResourceService resourceService,
        ITimeScheduler timeScheduler, INotificationManager notificationManager,
        DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>
            addTimeIntervalDialog,
        DialogViewModel<ITimeIntervalElement, bool> editTimeIntervalDialog,
        IFactory<ITimeIntervalElement> timeIntervalElementFactory,
        ITimeIntervalElementsEditorProxy timeIntervalElementsEditorProxy)
    {
        _session = session;
        _resourceService = resourceService;
        _timeScheduler = timeScheduler;
        _notificationManager = notificationManager;
        _addTimeIntervalDialog = addTimeIntervalDialog;
        _editTimeIntervalDialog = editTimeIntervalDialog;
        _timeIntervalElementFactory = timeIntervalElementFactory;
        _timeIntervalElementsEditorProxy = timeIntervalElementsEditorProxy;

        _canExecuteRemove = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null).CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
        _canExecuteEdit = this.WhenAnyValue(c => c.SelectedCalendarInterval).
            Select(c => c != null).CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);

        Metadata = resourceService.GetResource("TimePageMetadata");
        _session.ItemsUpdated += Session_ItemsUpdated;
        _timeScheduler.TimepointReached += TimeScheduler_TimepointReached;
    }

    [ReactiveCommand]
    private void Update()
    {
        if (_session.Tasks == null)
        {
            return;
        }
        CalendarIntervals.Clear();
        var tasks = TaskHelper.GetTaskElements(_session.Tasks);
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
        _timeScheduler.Timepoints.Clear();
        _timeScheduler.Timepoints.AddRange(_tasksSchedulerDictionary.Keys);
    }

    [ReactiveCommand]
    private void GoToNext() => CurrentWeek = CurrentWeek.AddDays(7);

    [ReactiveCommand]
    private void GoToNow() => CurrentWeek = DateTime.Now;

    [ReactiveCommand]
    private void GoToPrevious() => CurrentWeek = CurrentWeek.AddDays(-7);

    [ReactiveCommand(CanExecute = nameof(_modalsObservable))]
    private async Task Add()
    {
        var timeIntervalElement = _timeIntervalElementFactory.Create();
        var result = await AddModal(_addTimeIntervalDialog,
            new TimeIntervalViewModelArgs(_session.Tasks, _session.Tasks, timeIntervalElement));
        if (result != null)
        {
            _session.AddTimeInterval(result.TimeIntervalElement, result.TaskElement);
        }
    }

    [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
    private void Remove()
    {
        _session.RemoveTimeInterval(SelectedCalendarInterval.TimeInterval,
            SelectedCalendarInterval.TaskElement);
    }

    [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
    private async Task Edit()
    {
        var timeInterval = SelectedCalendarInterval.TimeInterval;
        var editorService = _timeIntervalElementsEditorProxy;
        editorService.Target = timeInterval;
        var result = await AddModal(_editTimeIntervalDialog, editorService);
        if (result)
        {
            editorService.ApplyChanges();
            _session.EditTimeInterval(timeInterval);
        }
    }

    private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();

    private void TimeScheduler_TimepointReached(object? sender, DateTime e)
    {
        var titleResource = _resourceService.GetResource("TimeSchedulerNotificationTitle");
        var contentResource = _resourceService.GetResource("TimeSchedulerNotificationContent");
        
        var content = $"{contentResource}\n";
        foreach (var task in _tasksSchedulerDictionary[e])
        {
            content += $"- {task.Metadata}\n";
        }
        _notificationManager.SendNotification(content, $"{titleResource}");
    }
}
