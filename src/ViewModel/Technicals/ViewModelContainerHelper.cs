﻿using Autofac;

using Model.Interfaces;

using ViewModel.Interfaces;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.ViewModels.Modals;
using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels;
using ViewModel.Implementations;
using ViewModel.Implementations.Mocks;
using ViewModel.Implementations.AppStates;
using ViewModel.Implementations.DataManagers.Factories;
using ViewModel.Implementations.DataManagers.Editors;
using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Sessions.Database.Mappers;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;
using ViewModel.Implementations.AppStates.Sessions.Database.DbContexts;
using ViewModel.Implementations.AppStates.Settings;

namespace ViewModel.Technicals
{
    public static class ViewModelContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var builder = GetContainerBuilder();

            builder.RegisterType<MockNotificationManager>().As<INotificationManager>().
                SingleInstance();
            builder.RegisterType<MockAppLifeState>().As<IAppLifeState>().SingleInstance();
            builder.RegisterType<MockResourceService>().
                As<IResourceService>().SingleInstance();
            builder.RegisterType<MockThemeManager>().As<IThemeManager>().SingleInstance();
            builder.RegisterType<MockLocalizationManager>().
                As<ILocalizationManager>().SingleInstance();

            return builder.Build();
        }

        public static ContainerBuilder GetContainerBuilder()
        {
            var result = new ContainerBuilder();

            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            result.RegisterType<FileLogger>().As<ILogger>().SingleInstance();

            result.RegisterType<TimeScheduler>().As<ITimeScheduler>().SingleInstance();

            result.RegisterType<MetadataFactory>().As<IFactory<object>>().SingleInstance();
            result.RegisterType<TaskElementFactory>().As<IFactory<ITaskElement>>().
                SingleInstance();
            result.RegisterType<TaskCompositeFactory>().As<IFactory<ITaskComposite>>().
                SingleInstance();
            result.RegisterType<TimeIntervalElementFactory>().
                As<IFactory<ITimeIntervalElement>>().SingleInstance();
            result.RegisterType<DbContextFactory>().
                As<IDbContextFactory<BaseDbContext>>().SingleInstance();

            result.RegisterType<TaskElementCreatorProxyFactory>().
                As<IFactory<ITaskElementProxy>>().SingleInstance();
            result.RegisterType<TasksEditorProxy>().As<ITasksEditorProxy>().SingleInstance();
            result.RegisterType<TaskElementsEditorProxy>().
                As<ITaskElementsEditorProxy>().SingleInstance();
            result.RegisterType<TimeIntervalElementsEditorProxy>().
                As<ITimeIntervalElementsEditorProxy>().SingleInstance();
            result.RegisterType<TimeIntervalElementsEditorProxy>().
                As<ITimeIntervalElementsEditorProxy>().SingleInstance();

            result.RegisterType<AddTimeIntervalViewModel>().
                As<DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            result.RegisterType<EditTimeIntervalViewModel>().
                As<DialogViewModel<ITimeIntervalElement, bool>>().SingleInstance();
            result.RegisterType<AddTaskViewModel>().
                As<DialogViewModel<ITask, bool>>().SingleInstance();
            result.RegisterType<RemoveTasksViewModel>().
                As<DialogViewModel<IList<ITask>, bool>>().SingleInstance();
            result.RegisterType<MoveTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>>().
                SingleInstance();
            result.RegisterType<EditTaskViewModel>().
                As<DialogViewModel<object, bool>>().SingleInstance();
            result.RegisterType<CopyTasksViewModel>().
                As<DialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?>>().
                SingleInstance();

            result.RegisterType<TimeIntervalMapper>().
                As<IMapper<TimeIntervalEntity, ITimeIntervalElement>>().SingleInstance();
            result.RegisterType<MetadataMapper>().
                As<IMapper<MetadataEntity, object>>().SingleInstance();
            result.RegisterType<TaskElementMapper>().
                As<IMapper<TaskElementEntity, ITaskElement>>().SingleInstance();
            result.RegisterType<TaskCompositeMapper>().
                As<IMapper<TaskCompositeEntity, ITaskComposite>>().SingleInstance();
            result.RegisterType<TaskMapper>().As<IMapper<TaskEntity, ITask>>().SingleInstance();

            result.RegisterType<DbSession>().As<ISession>().SingleInstance();
            result.RegisterType<AppSettings>().As<ISettings>().SingleInstance();

            result.RegisterType<EditorViewModel>().As<EditorViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<TimeViewModel>().As<TimeViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<StatisticViewModel>().As<StatisticViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<ToDoListViewModel>().As<ToDoListViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<SettingsViewModel>().As<SettingsViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();

            return result;
        }
    }
}
