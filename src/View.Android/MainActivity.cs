using Android.App;
using Android.Content.PM;
using Autofac;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using System;

using ViewModel.Interfaces.AppStates;

namespace View.Android;

[Activity(
    Label = "TaskManager",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>, IAppLifeState
{
    public event EventHandler AppDeactivated;

    public MainActivity()
    {
        ((IAvaloniaActivity)this).Deactivated += MainActivity_Deactivated;
    }

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

    private App CreateApp()
    {
        var result = new App();
        result.ContainerBuilderCreated += App_ContainerBuilderCreated;
        return result;
    }

    private void App_ContainerBuilderCreated(object? sender, ContainerBuilder e)
    {
        e.RegisterType<AndroidNotificationManager>().As<INotificationManager>().SingleInstance();
        e.RegisterInstance(this).As<IAppLifeState>().SingleInstance();
    }

    private void MainActivity_Deactivated(object? sender, ActivatedEventArgs e)
    {
        AppDeactivated?.Invoke(this, EventArgs.Empty);
    }
}
