using System;
using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop.Windows;

public class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure(CreateApp)
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .UseReactiveUI();

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
