using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model.Technicals;
using Model;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.AppStates;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly AppState _appState;

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new TrackableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    [Reactive]
    private DateTime _currentWeek = DateTime.Now;

    public TimeViewModel(AppState appState)
    {
        _appState = appState;

        Metadata = _appState.Services.ResourceService.GetResource("TimePageMetadata");
        _appState.ItemSessionChanged += AppStateManager_ItemSessionChanged;
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
            result.TaskElement.TimeIntervals.Add(result.TimeIntervalElement);
            CalendarIntervals.Add(new CalendarInterval(result.TimeIntervalElement, result.TaskElement));
            _appState.UpdateSessionItems();
        }
    }

    private void AppStateManager_ItemSessionChanged(object? sender, object e) =>
        UpdateCommand.Execute();
}
