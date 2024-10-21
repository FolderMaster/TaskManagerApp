using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

using View.Views;

using ViewModel.ViewModels;
using ViewModel.ViewModels.Pages;

namespace View;

public partial class App : Application
{
    public Action<ContainerBuilder>? RegisterServicesAction { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var container = RegisterServices().Build();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = container.Resolve<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = container.Resolve<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public ContainerBuilder RegisterServices()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<TaskEditorViewModel>().As<PageViewModel>().SingleInstance();
        builder.RegisterType<TaskViewModel>().As<PageViewModel>().SingleInstance();
        builder.RegisterType<MainViewModel>().SingleInstance();
        RegisterServicesAction?.Invoke(builder);
        return builder;
    }
}
