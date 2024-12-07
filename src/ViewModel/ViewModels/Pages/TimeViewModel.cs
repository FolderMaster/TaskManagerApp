using ReactiveUI.SourceGenerators;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

using Model;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using ViewModel.AppStates;
using ViewModel.Interfaces.Events;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly IObservable<bool> _canExecuteRemove;

    private readonly IObservable<bool> _canExecuteEdit;

    private readonly AppState _appState;

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
        var result = await AddModal(_appState.Services.EditTimeIntervalDialog,
            SelectedCalendarInterval.TimeInterval);
        if (result)
        {
            _appState.Session.EditTimeInterval(SelectedCalendarInterval.TimeInterval);
        }
    }

    private void Session_ItemsUpdated(object? sender, ItemsUpdatedEventArgs e) => Update();
}
