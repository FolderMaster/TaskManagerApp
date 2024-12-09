using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System;

namespace View.Controls;

public partial class DateTimePicker : UserControl
{
    public static readonly StyledProperty<DateTime?> DateTimeProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTime?>(nameof(DateTime),
            defaultBindingMode: BindingMode.TwoWay);

    public static readonly StyledProperty<TimeSpan?> TimeSpanProperty =
        AvaloniaProperty.Register<DateTimePicker, TimeSpan?>(nameof(TimeSpan),
            defaultBindingMode: BindingMode.TwoWay);

    public DateTime? DateTime
    {
        get => GetValue(DateTimeProperty);
        set => SetValue(DateTimeProperty, value);
    }

    public TimeSpan? TimeSpan
    {
        get => GetValue(TimeSpanProperty);
        set => SetValue(TimeSpanProperty, value);
    }

    public DateTimePicker()
    {
        InitializeComponent();

        DateTimeProperty.Changed.AddClassHandler<DateTimePicker, DateTime?>
            ((o, e) => o.OnDateTimeChanged(e));
        TimeSpanProperty.Changed.AddClassHandler<DateTimePicker, TimeSpan?>
            ((o, e) => o.OnTimeSpanChanged(e));
    }

    private void OnDateTimeChanged(AvaloniaPropertyChangedEventArgs<DateTime?> args)
    {
        TimeSpan = DateTime?.TimeOfDay;
    }

    private void OnTimeSpanChanged(AvaloniaPropertyChangedEventArgs<TimeSpan?> args)
    {
        if (DateTime != null && TimeSpan != null)
        {
            DateTime = new DateTime(DateOnly.FromDateTime(DateTime.Value),
                TimeOnly.FromTimeSpan(TimeSpan.Value));
        }
        else
        {
            DateTime = null;
        }
    }
}