using Autofac;
using Avalonia.ReactiveUI;
using System.Collections.Generic;
using Splat;
using Splat.Autofac;
using ReactiveUI;

using Model.Interfaces;

using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels;
using ViewModel.Interfaces;
using ViewModel.ViewModels.Modals;
using ViewModel.Implementations;
using ViewModel.Implementations.Mocks;
using ViewModel.AppStates;
using ViewModel.Implementations.Factories;

using View.Implementations;
using View.Views;
using View.Views.Pages;
using View.Views.Modals;

namespace View.Technilcals
{
    public static class ContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var (builder, resolver) = GetContainerElements();

            builder.RegisterType<MockNotificationManager>().As<INotificationManager>().
                SingleInstance();

            return CreateContainer(builder, resolver, true);
        }

        public static (ContainerBuilder, AutofacDependencyResolver) GetContainerElements()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            builder.RegisterType<AvaloniaResourceService>().
                As<IResourceService>().SingleInstance();
            builder.RegisterType<AvaloniaThemeManager>().As<IThemeManager>().SingleInstance();
            builder.RegisterType<AvaloniaLocalizationManager>().
                As<ILocalizationManager>().SingleInstance();

            builder.RegisterType<MetadataFactory>().As<IFactory<object>>().SingleInstance();
            builder.RegisterType<TaskElementFactory>().As<IFactory<ITaskElement>>().
                SingleInstance();
            builder.RegisterType<TaskCompositeFactory>().As<IFactory<ITaskComposite>>().
                SingleInstance();
            builder.RegisterType<TimeIntervalElementFactory>().
                As<IFactory<ITimeIntervalElement>>().SingleInstance();

            builder.RegisterType<AddTimeIntervalViewModel>().
                As<DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            builder.RegisterType<AddTaskViewModel>().As<DialogViewModel<ITask, bool>>().
                SingleInstance();
            builder.RegisterType<RemoveTasksViewModel>().
                As<DialogViewModel<IList<ITask>, bool>>().SingleInstance();
            builder.RegisterType<MoveTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();
            builder.RegisterType<EditTaskViewModel>().
                As<DialogViewModel<object, bool>>().SingleInstance();
            builder.RegisterType<CopyTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();

            builder.RegisterType<Session>().SingleInstance();
            builder.RegisterType<Settings>().SingleInstance();
            builder.RegisterType<ServicesCollection>().SingleInstance();
            builder.RegisterType<AppState>().As<AppState>().SingleInstance();

            builder.RegisterType<EditorViewModel>().As<EditorViewModel>().
                As<PageViewModel>().SingleInstance();
            builder.RegisterType<TimeViewModel>().As<TimeViewModel>().
                As<PageViewModel>().SingleInstance();
            builder.RegisterType<StatisticViewModel>().As<StatisticViewModel>().
                As<PageViewModel>().SingleInstance();
            builder.RegisterType<ToDoListViewModel>().As<ToDoListViewModel>().
                As<PageViewModel>().SingleInstance();
            builder.RegisterType<SettingsViewModel>().As<SettingsViewModel>().
                As<PageViewModel>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();

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
