using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

using ViewModel.ViewModels;

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
        var container = ContainerBuilder().Build();
        var mainViewModel = container.Resolve<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView()
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public ContainerBuilder ContainerBuilder()
    {
        var result = ContainerHelper.GetContainerBuilder();
        ContainerBuilderCreated?.Invoke(this, result);
        return result;
    }
}
