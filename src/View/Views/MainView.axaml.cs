using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Splat;

using ViewModel.ViewModels;

namespace View.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public static readonly StyledProperty<bool> IsPaneOpenProperty =
        AvaloniaProperty.Register<MainView, bool>(nameof(IsPaneOpen), defaultValue: false);

    public static readonly StyledProperty<double> InputPaneHeightProperty =
        AvaloniaProperty.Register<MainView, double>(nameof(InputPaneHeight), defaultValue: 320);

    public static readonly StyledProperty<bool> IsInputPaneVisibleProperty =
        AvaloniaProperty.Register<MainView, bool>(nameof(IsInputPaneVisible), defaultValue: false);

    public bool IsPaneOpen
    {
        get => GetValue(IsPaneOpenProperty);
        set => SetValue(IsPaneOpenProperty, value);
    }

    public double InputPaneHeight
    {
        get => GetValue(InputPaneHeightProperty);
        set => SetValue(InputPaneHeightProperty, value);
    }

    public bool IsInputPaneVisible
    {
        get => GetValue(IsInputPaneVisibleProperty);
        set => SetValue(IsInputPaneVisibleProperty, value);
    }

    public MainView()
    {
        InitializeComponent();

        DataContext = Locator.Current.GetService(typeof(MainViewModel));
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel != null)
        {
            var inputPane = topLevel.InputPane;
            if (inputPane != null)
            {
                inputPane.StateChanged += InputPane_StateChanged;
                SetIsVisible(inputPane.State);
                InputPaneHeight = inputPane.OccludedRect.Height;
            }
        }
    }

    private void SetIsVisible(InputPaneState state) =>
        IsInputPaneVisible = state == InputPaneState.Open;

    private void InputPane_StateChanged(object? sender, InputPaneStateEventArgs e)
    {
        SetIsVisible(e.NewState);
        InputPaneHeight = e.EndRect.Height;
    }

    private void Button_Click(object sender, RoutedEventArgs args) => IsPaneOpen = !IsPaneOpen;
}
