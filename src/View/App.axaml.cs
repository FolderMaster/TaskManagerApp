using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

using View.Views;
using View.Technilcals;

namespace View;

/// <summary>
/// Класс приложения.
/// </summary>
/// <remarks>
/// Наследует <see cref="Application"/>.
/// </remarks>
public partial class App : Application
{
    /// <summary>
    /// Событие, возникающее, когда конфигуратор контейнера зависимостей создан.
    /// </summary>
    public event EventHandler<ContainerBuilder>? ContainerBuilderCreated;

    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        var container = BuildContainer();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = container.Resolve<MainWindow>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = container.Resolve<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Создаёт контейнер зависимостей.
    /// </summary>
    /// <returns>Возвращает контейнер зависимостей.</returns>
    private IContainer BuildContainer()
    {
        var (builder, resolver) = ViewContainerHelper.GetContainerElements();
        ContainerBuilderCreated?.Invoke(this, builder);
        return ViewContainerHelper.CreateContainer(builder, resolver,  true);
    }
}
