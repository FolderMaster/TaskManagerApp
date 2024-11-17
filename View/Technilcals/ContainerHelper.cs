using Autofac;
using System.Collections.Generic;

using Model.Interfaces;

using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels;
using ViewModel.Interfaces;
using ViewModel.ViewModels.Modals;
using ViewModel.Implementations;
using ViewModel.Implementations.Mocks;
using ViewModel.AppState;

using View.Implementations;

namespace View.Technilcals
{
    public static class ContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var result = new ContainerBuilder();

            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            result.RegisterType<MockNotificationManager>().As<INotificationManager>().
                SingleInstance();
            result.RegisterType<AvaloniaResourceService>().As<IResourceService>().SingleInstance();

            result.RegisterType<MetadataFactory>().As<IFactory<object>>().SingleInstance();
            result.RegisterType<TaskElementFactory>().As<IFactory<ITaskElement>>().
                SingleInstance();
            result.RegisterType<TaskCompositeFactory>().As<IFactory<ITaskComposite>>().
                SingleInstance();

            result.RegisterType<AddTimeIntervalViewModel>().
                As<DialogViewModel<TasksViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            result.RegisterType<AddTaskViewModel>().As<DialogViewModel<ITask, bool>>().
                SingleInstance();
            result.RegisterType<RemoveTasksViewModel>().
                As<DialogViewModel<IList<ITask>, bool>>().SingleInstance();
            result.RegisterType<MoveTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();
            result.RegisterType<EditTaskViewModel>().
                As<DialogViewModel<object, bool>>().SingleInstance();
            result.RegisterType<CopyTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();

            result.RegisterType<Session>().SingleInstance();
            result.RegisterType<ServicesCollection>().SingleInstance();
            result.RegisterType<AppStateManager>().As<AppStateManager>().SingleInstance();

            result.RegisterType<EditorViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<TimeViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<StatisticViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<ToDoListViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result.Build();
        }

        public static ContainerBuilder GetContainerBuilder()
        {
            var result = new ContainerBuilder();
            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            result.RegisterType<AvaloniaResourceService>().As<IResourceService>().SingleInstance();

            result.RegisterType<MetadataFactory>().As<IFactory<object>>().SingleInstance();
            result.RegisterType<TaskElementFactory>().As<IFactory<ITaskElement>>().
                SingleInstance();
            result.RegisterType<TaskCompositeFactory>().As<IFactory<ITaskComposite>>().
                SingleInstance();

            result.RegisterType<AddTimeIntervalViewModel>().
                As<DialogViewModel<TasksViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            result.RegisterType<AddTaskViewModel>().As<DialogViewModel<ITask, bool>>().
                SingleInstance();
            result.RegisterType<RemoveTasksViewModel>().
                As<DialogViewModel<IList<ITask>, bool>>().SingleInstance();
            result.RegisterType<MoveTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();
            result.RegisterType<EditTaskViewModel>().
                As<DialogViewModel<object, bool>>().SingleInstance();
            result.RegisterType<CopyTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?>>().SingleInstance();

            result.RegisterType<Session>().SingleInstance();
            result.RegisterType<ServicesCollection>().SingleInstance();
            result.RegisterType<AppStateManager>().As<AppStateManager>().SingleInstance();

            result.RegisterType<EditorViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<TimeViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<StatisticViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<ToDoListViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result;
        }
    }
}
