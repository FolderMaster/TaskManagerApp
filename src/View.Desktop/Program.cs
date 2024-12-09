using System;
using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;

using ViewModel.Interfaces.AppStates;

namespace View.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure(CreateApp)
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
        e.RegisterType<DesktopNotificationManager>().As<INotificationManager>();
    }
}
