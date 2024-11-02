using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;
using ReactiveUI;

using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;
using Model;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private readonly Session _session;

    private AddTimeIntervalViewModel _addDialog = new();

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new ObservableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    public TimeViewModel(object metadata, Session session) : base(metadata)
    {
        _session = session;

        this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => Update());
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
                CalendarIntervals.Add(new CalendarInterval((TimeInterval)timeInterval, task));
            }
        }
    }

    [ReactiveCommand]
    private async Task Add()
    {
        _addDialog.MainList = _session.Tasks;
        if (await AddModal(_addDialog) is CalendarInterval calendarInterval)
        {
            CalendarIntervals.Add(calendarInterval);
        }
    }
}
