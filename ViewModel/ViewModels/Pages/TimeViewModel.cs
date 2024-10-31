using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;
using ReactiveUI;

using Model;
using ViewModel.Technicals;
using ViewModel.ViewModels.Modals;

namespace ViewModel.ViewModels.Pages;

public partial class TimeViewModel : PageViewModel
{
    private IList<ITask> _mainTaskList;

    private AddTimeIntervalViewModel _addDialog = new();

    [Reactive]
    private IList<CalendarInterval> _calendarIntervals =
        new ObservableCollection<CalendarInterval>();

    [Reactive]
    private CalendarInterval? _selectedCalendarInterval;

    public TimeViewModel(object metadata, IList<ITask> mainTaskList) : base(metadata)
    {
        _mainTaskList = mainTaskList;
    }

    [ReactiveCommand]
    private async Task Add()
    {
        _addDialog.MainList = _mainTaskList;
        if (await AddModal(_addDialog) is CalendarInterval calendarInterval)
        {
            CalendarIntervals.Add(calendarInterval);
        }
    }
}
