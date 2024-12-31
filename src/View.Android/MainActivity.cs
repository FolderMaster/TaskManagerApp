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

/// <summary>
/// Класс основной активности.
/// </summary>
/// <remarks>
/// Наследует <see cref="AvaloniaMainActivity{App}"/>.
/// Реализует <see cref="IAppLifeState"/>.
/// </remarks>
[Activity(
    Label = "TaskManager",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>, IAppLifeState
{
    /// <inheritdoc/>
    public event EventHandler AppDeactivated;

    /// <summary>
    /// Создаёт экземпляр класса <see cref="MainActivity"/> по умолчанию.
    /// </summary>
    public MainActivity()
    {
        ((IAvaloniaActivity)this).Deactivated += MainActivity_Deactivated;
    }

    /// <inheritdoc/>
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }

    /// <inheritdoc/>
    protected override AppBuilder CreateAppBuilder()
    {
        return AppBuilder.Configure(CreateApp).UseAndroid();
    }

    /// <summary>
    /// Создаёт приложение.
    /// </summary>
    /// <returns>Возвращает приложение.</returns>
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
