﻿using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.iOS;
using Avalonia.ReactiveUI;
using Foundation;
using System;

using ViewModel.Interfaces.AppStates;

namespace View.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
/// <summary>
/// Класс делегата приложения.
/// </summary>
/// <remarks>
/// Наследует <see cref="AvaloniaAppDelegate{App}"/>.
/// Реализует <see cref="IAppLifeState"/>.
/// </remarks>
[Register("AppDelegate")]
public partial class AppDelegate : AvaloniaAppDelegate<App>, IAppLifeState
{
    /// <inheritdoc/>
    public event EventHandler AppDeactivated;

    /// <summary>
    /// Создаёт экземпляр класса <see cref=" AppDelegate"/> по умолчанию.
    /// </summary>
    public AppDelegate()
    {
        ((IAvaloniaAppDelegate)this).Deactivated += AppDelegate_Deactivated;
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
        return AppBuilder.Configure(CreateApp).UseiOS();
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
        e.RegisterType<IOsNotificationManager>().As<INotificationManager>().SingleInstance();
        e.RegisterInstance(this).As<IAppLifeState>().SingleInstance();
    }

    private void AppDelegate_Deactivated(object? sender, ActivatedEventArgs e)
    {
        AppDeactivated?.Invoke(this, e);
    }
}
