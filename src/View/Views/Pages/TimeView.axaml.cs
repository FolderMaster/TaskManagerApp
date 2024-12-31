using Avalonia.ReactiveUI;
using SatialInterfaces.Controls.Calendar;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace View.Views.Pages;

/// <summary>
/// Класс пользовательского элемента страницы календаря.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{TimeViewModel}"/>.
/// </remarks>
public partial class TimeView : ReactiveUserControl<TimeViewModel>
{
    /// <summary>
    /// Создаёт экземпляр класса <see cref="TimeView"/> по умолчанию.
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