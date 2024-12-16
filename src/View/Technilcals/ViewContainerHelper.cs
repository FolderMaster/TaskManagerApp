using Autofac;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat.Autofac;
using Splat;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels.Modals;
using ViewModel.Implementations.Mocks;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Settings;

using View.Views;
using View.Views.Pages;
using View.Views.Modals;
using View.Implementations;

namespace View.Technilcals
{
    public static class ViewContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var (builder, resolver) = GetContainerElements();

            builder.RegisterType<MockNotificationManager>().As<INotificationManager>().
                SingleInstance();
            builder.RegisterType<MockAppLifeState>().As<IAppLifeState>().SingleInstance();

            return CreateContainer(builder, resolver, true);
        }

        public static (ContainerBuilder, AutofacDependencyResolver) GetContainerElements()
        {
            var builder = ViewModelContainerHelper.GetContainerBuilder();

            builder.RegisterType<AvaloniaResourceService>().
                As<IResourceService>().SingleInstance();
            builder.RegisterType<AvaloniaThemeManager>().As<IThemeManager>().SingleInstance();
            builder.RegisterType<AvaloniaLocalizationManager>().
                As<ILocalizationManager>().SingleInstance();

            builder.RegisterType<MainView>().SingleInstance();
            builder.RegisterType<MainWindow>().SingleInstance();

            builder.RegisterType<EditorView>().As<IViewFor<EditorViewModel>>();
            builder.RegisterType<TimeView>().As<IViewFor<TimeViewModel>>();
            builder.RegisterType<StatisticView>().As<IViewFor<StatisticViewModel>>();
            builder.RegisterType<ToDoListView>().As<IViewFor<ToDoListViewModel>>();

            builder.RegisterType<AddTaskView>().As<IViewFor<AddTaskViewModel>>();
            builder.RegisterType<AddTimeIntervalView>().As<IViewFor<AddTimeIntervalViewModel>>();
            builder.RegisterType<CopyTasksView>().As<IViewFor<CopyTasksViewModel>>();
            builder.RegisterType<EditTaskView>().As<IViewFor<EditTaskViewModel>>();
            builder.RegisterType<EditTimeIntervalView>().As<IViewFor<EditTimeIntervalViewModel>>();
            builder.RegisterType<MoveTasksView>().As<IViewFor<MoveTasksViewModel>>();
            builder.RegisterType<RemoveTasksView>().As<IViewFor<RemoveTasksViewModel>>();
            builder.RegisterType<SettingsView>().As<IViewFor<SettingsViewModel>>();

            var resolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(resolver);
            resolver.InitializeReactiveUI();

            return (builder, resolver);
        }

        public static IContainer CreateContainer(ContainerBuilder builder,
            AutofacDependencyResolver resolver, bool isSetUpLocator)
        {
            if (isSetUpLocator)
            {
                RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
                Locator.CurrentMutable.RegisterConstant(new AvaloniaActivationForViewFetcher(),
                    typeof(IActivationForViewFetcher));
                Locator.CurrentMutable.RegisterConstant(new AutoDataTemplateBindingHook(),
                    typeof(IPropertyBindingHook));
            }
            var result = builder.Build();
            resolver.SetLifetimeScope(result);
            return result;
        }
    }
}
