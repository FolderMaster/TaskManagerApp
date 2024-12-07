using Avalonia.ReactiveUI;
using SatialInterfaces.Controls.Calendar;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

public partial class TimeView : ReactiveUserControl<TimeViewModel>
{
    public TimeView()
    {
        InitializeComponent();
    }

    private void CalendarControl_SelectionChanged(object? sender,
        CalendarSelectionChangedEventArgs e)
    {
        var dataContext = (TimeViewModel)DataContext;
        dataContext.SelectedCalendarInterval = (CalendarInterval?)e.SelectedItem;
    }
}