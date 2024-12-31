using System;
using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Windows;

/// <summary>
/// Класс программы.
/// </summary>
public class Program
{
    /// <summary>
    /// Запускает основной поток приложения.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    /// <summary>
    /// Настраивает приложение.
    /// </summary>
    /// <returns>Возвращает настройки приложения.</returns>
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure(CreateApp)
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .UseReactiveUI();

    /// <summary>
    /// Создаёт приложение.
    /// </summary>
    /// <returns>Возвращает приложение.</returns>
    private static App CreateApp()
    {
        var result = new App();
        result.ContainerBuilderCreated += App_ContainerBuilderCreated;
        return result;
    }

    private static void App_ContainerBuilderCreated(object? sender, ContainerBuilder e)
    {
        e.RegisterType<WindowsNotificationManager>().As<INotificationManager>().SingleInstance();
        e.RegisterType<DesktopAppLifeState>().As<IAppLifeState>().SingleInstance();
    }
}
