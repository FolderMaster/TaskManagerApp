using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

using View.Views;
using View.Technilcals;

namespace View;

public partial class App : Application
{
    public event EventHandler<ContainerBuilder>? ContainerBuilderCreated;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

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

    private IContainer BuildContainer()
    {
        var (builder, resolver) = ContainerHelper.GetContainerElements();
        ContainerBuilderCreated?.Invoke(this, builder);
        return ContainerHelper.CreateContainer(builder, resolver,  true);
    }
}
