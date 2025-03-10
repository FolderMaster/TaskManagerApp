﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Splat;

using ViewModel.ViewModels;

namespace View.Views;

/// <summary>
/// Класс основного пользовательского элемента приложения.
/// </summary>
/// <remarks>
/// Наследует <see cref="ReactiveUserControl{MainViewModel}"/>.
/// </remarks>
public partial class MainView : ReactiveUserControl<MainViewModel>
{
    /// <summary>
    /// Свойство стиля <see cref="IsPaneOpen"/>.
    /// </summary>
    public static readonly StyledProperty<bool> IsPaneOpenProperty =
        AvaloniaProperty.Register<MainView, bool>(nameof(IsPaneOpen), defaultValue: false);

    /// <summary>
    /// Свойство стиля <see cref="InputPaneHeight"/>.
    /// </summary>
    public static readonly StyledProperty<double> InputPaneHeightProperty =
        AvaloniaProperty.Register<MainView, double>(nameof(InputPaneHeight), defaultValue: 320);

    /// <summary>
    /// Свойство стиля <see cref="IsInputPaneVisible"/>.
    /// </summary>
    public static readonly StyledProperty<bool> IsInputPaneVisibleProperty =
        AvaloniaProperty.Register<MainView, bool>(nameof(IsInputPaneVisible), defaultValue: false);

    /// <summary>
    /// Возвращает и задаёт логическкое значение, указывающее, открыта ли панель меню.
    /// </summary>
    public bool IsPaneOpen
    {
        get => GetValue(IsPaneOpenProperty);
        set => SetValue(IsPaneOpenProperty, value);
    }

    /// <summary>
    /// Возвращает и задаёт высоту панели ввода.
    /// </summary>
    public double InputPaneHeight
    {
        get => GetValue(InputPaneHeightProperty);
        set => SetValue(InputPaneHeightProperty, value);
    }

    /// <summary>
    /// Возвращает и задаёт логическкое значение, указывающее, видна ли панель ввода.
    /// </summary>
    public bool IsInputPaneVisible
    {
        get => GetValue(IsInputPaneVisibleProperty);
        set => SetValue(IsInputPaneVisibleProperty, value);
    }

    /// <summary>
    /// Создаёт экземпляр класса <see cref="MainView"/> по умолчанию.
    /// </summary>
    public MainView()
    {
        InitializeComponent();

        DataContext = Locator.Current.GetService(typeof(MainViewModel));
    }

    /// <inheritdoc/>
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

    /// <summary>
    /// Устанавливает значение <see cref="IsInputPaneVisible"/>.
    /// </summary>
    /// <param name="state">Состояние панели ввода.</param>
    private void SetIsVisible(InputPaneState state) =>
        IsInputPaneVisible = state == InputPaneState.Open;

    private void InputPane_StateChanged(object? sender, InputPaneStateEventArgs e)
    {
        SetIsVisible(e.NewState);
        InputPaneHeight = e.EndRect.Height;
    }

    private void Button_Click(object sender, RoutedEventArgs args) => IsPaneOpen = !IsPaneOpen;
}
