using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model.Tasks.Times;
using Model.Technicals;
using Model;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.AppState;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly AppStateManager _appStateManager;

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new TrackableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    [Reactive]
    private DateTime _currentWeek = DateTime.Now;

    public TimeViewModel(AppStateManager appStateManager)
    {
        _appStateManager = appStateManager;

        Metadata = _appStateManager.Services.ResourceService.GetResource("TimePageMetadata");
        _appStateManager.ItemSessionChanged += AppStateManager_ItemSessionChanged;
    }

    [ReactiveCommand]
    private void Update()
    {
        if (_appStateManager.Session.Tasks == null)
        {
            return;
        }
        CalendarIntervals.Clear();
        var tasks = TaskHelper.GetTaskElements(_appStateManager.Session.Tasks);
        foreach (var task in tasks)
        {
            foreach(var timeInterval in task.TimeIntervals)
            {
                CalendarIntervals.Add(new CalendarInterval((TimeIntervalElement)timeInterval, task));
            }
        }
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
        var result = await AddModal(_appStateManager.Services.AddTimeIntervalDialog,
            new TasksViewModelArgs(_appStateManager.Session.Tasks,
            _appStateManager.Session.Tasks));
        if (result != null)
        {
            result.TaskElement.TimeIntervals.Add(result.TimeInterval);
            CalendarIntervals.Add(new CalendarInterval(result.TimeInterval, result.TaskElement));
            _appStateManager.UpdateSessionItems();
        }
        
    }

    private void AppStateManager_ItemSessionChanged(object? sender, object e) =>
        UpdateCommand.Execute();
}
