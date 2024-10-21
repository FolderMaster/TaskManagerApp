using System;
using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;

using ViewModel;

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
        var result = new App()
        {
            RegisterServicesAction = RegisterServices
        };
        return result;
    }

    private static void RegisterServices(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<DesktopNotificationManager>().
            As<INotificationManager>();
    }
}
