using Avalonia.ReactiveUI;
using SatialInterfaces.Controls.Calendar;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// ����� ����������������� �������� �������� ���������.
/// </summary>
/// <remarks>
/// ��������� <see cref="ReactiveUserControl{TimeViewModel}"/>.
/// </remarks>
public partial class TimeView : ReactiveUserControl<TimeViewModel>
{
    /// <summary>
    /// ������ ��������� ������ <see cref="TimeView"/> �� ���������.
    /// </summary>
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