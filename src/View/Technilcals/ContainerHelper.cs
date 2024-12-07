using Autofac;
using Avalonia.ReactiveUI;
using System.Collections.Generic;
using Splat;
using Splat.Autofac;
using ReactiveUI;

using Model.Interfaces;

using ViewModel.Interfaces;
using ViewModel.ViewModels;
using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels.Modals;
using ViewModel.AppStates;
using ViewModel.Implementations;
using ViewModel.Implementations.Mocks;
using ViewModel.Implementations.Factories;
using ViewModel.Implementations.Sessions;
using ViewModel.Implementations.Sessions.Database.Mappers;
using ViewModel.Implementations.Sessions.Database.Entities;
using ViewModel.Implementations.Sessions.Database.DbContexts;

using View.Views;
using View.Views.Pages;
using View.Views.Modals;
using View.Implementations;

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
            builder.RegisterType<DbContextFactory>().As<IFactory<BaseDbContext>>().SingleInstance();

            builder.RegisterType<AddTimeIntervalViewModel>().
                As<DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            builder.RegisterType<EditTimeIntervalViewModel>().
                As<DialogViewModel<ITimeIntervalElement, bool>>().SingleInstance();
            builder.RegisterType<AddTaskViewModel>().As<DialogViewModel<ITask, bool>>().
                SingleInstance();
            builder.RegisterType<RemoveTasksViewModel>().
                As<DialogViewModel<IList<ITask>, bool>>().SingleInstance();
            builder.RegisterType<MoveTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>>().SingleInstance();
            builder.RegisterType<EditTaskViewModel>().
                As<DialogViewModel<object, bool>>().SingleInstance();
            builder.RegisterType<CopyTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>>().SingleInstance();

            builder.RegisterType<TimeIntervalMapper>().
                As<IMapper<TimeIntervalEntity, ITimeIntervalElement>>().SingleInstance();
            builder.RegisterType<MetadataMapper>().
                As<IMapper<MetadataEntity, object>>().SingleInstance();
            builder.RegisterType<TaskElementMapper>().
                As<IMapper<TaskElementEntity, ITaskElement>>().SingleInstance();
            builder.RegisterType<TaskCompositeMapper>().
                As<IMapper<TaskCompositeEntity, ITaskComposite>>().SingleInstance();
            builder.RegisterType<TaskMapper>().As<IMapper<TaskEntity, ITask>>().SingleInstance();

            builder.RegisterType<DbSession>().As<ISession>().SingleInstance();
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
