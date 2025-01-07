using Avalonia.Controls;
using Avalonia.Layout;
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

    /// <inheritdoc/>
    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        var leftElementRight = leftTopElement.Bounds.Width + leftTopElement.Margin.Right;
        var rightElementLeft = Bounds.Width - (rightTopElement.Bounds.Width +
            rightTopElement.Margin.Right);
        if (leftElementRight > rightElementLeft)
        {
            Grid.SetRow(rightTopElement, 1);
            rightTopElement.HorizontalAlignment = HorizontalAlignment.Left;
        }
        else
        {
            Grid.SetRow(rightTopElement, 0);
            rightTopElement.HorizontalAlignment = HorizontalAlignment.Right;
        }
    }

    private void CalendarControl_SelectionChanged(object? sender,
        CalendarSelectionChangedEventArgs e)
    {
        var dataContext = (TimeViewModel)DataContext;
        dataContext.SelectedCalendarInterval = (CalendarInterval?)e.SelectedItem;
    }
}