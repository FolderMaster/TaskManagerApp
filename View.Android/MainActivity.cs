using Android.App;
using Android.Content.PM;
using Autofac;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;

using ViewModel;

namespace View.Android;

[Activity(
    Label = "TaskManager",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }

    protected override AppBuilder CreateAppBuilder()
    {
        return AppBuilder.Configure(CreateApp).UseAndroid();
    }

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
        containerBuilder.RegisterType<AndroidNotificationManager>().
            As<INotificationManager>();
    }
}
