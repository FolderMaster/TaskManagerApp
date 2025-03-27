using Autofac;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat.Autofac;
using Splat;
using System.Collections.Generic;

using Model.Interfaces;

using MachineLearning.DataProcessors;
using MachineLearning.DistanceMetrics;
using MachineLearning.Interfaces;
using MachineLearning.LearningEvaluators;
using MachineLearning.LearningModels;
using MachineLearning.ScoreMetrics;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels.Modals;
using ViewModel.Implementations.Tests;
using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Settings;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.AppStates;
using ViewModel.Implementations.DataManagers.Editors;
using ViewModel.Implementations.DataManagers.Factories;
using ViewModel.Implementations.ModelLearning.Converters;
using ViewModel.Implementations.ModelLearning;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Interfaces;
using ViewModel.ViewModels;

using Database.DbContexts;
using Database.Entities;
using Database.Factories;
using Database.Mappers;
using DataBase;

using View.Views;
using View.Views.Pages;
using View.Views.Modals;
using View.Implementations;

using ILogger = ViewModel.Interfaces.AppStates.ILogger;

namespace View.Technilcals
{
    /// <summary>
    /// Вспомогательный статичный класс для работы с контейнером зависимостей в контексте
    /// <see cref="View"/>.
    /// </summary>
    public static class ContainerHelper
    {
        /// <summary>
        /// Создаёт и возвращает контейнер зависимостей с заглушками.
        /// </summary>
        /// <returns>Возвращает контейнер зависимостей с заглушками.</returns>
        public static IContainer GetMockContainer()
        {
            var (builder, resolver) = GetContainerElements();

            builder.RegisterType<MockNotificationManager>().As<INotificationManager>().
                SingleInstance();
            builder.RegisterType<MockAppLifeState>().As<IAppLifeState>().SingleInstance();

            return CreateContainer(builder, resolver, true);
        }

        /// <summary>
        /// Создаёт и возвращает элементы контейнера зависимостей.
        /// </summary>
        /// <returns>Возвращает конфигуратор и решатель контейнера зависимостей.</returns>
        public static (ContainerBuilder, AutofacDependencyResolver) GetContainerElements()
        {
            var result = new ContainerBuilder();

            result.RegisterType<MetadataCategoriesTransformer>().
                As<IDataTransformer<TaskMetadata, int?>>();
            result.RegisterType<MetadataTagsTransformer>().
                As<IDataTransformer<TaskMetadata, IEnumerable<int>>>();

            result.RegisterType<InvalidValuesColumnProcessor>().
                As<IPrimaryPointDataProcessor>().SingleInstance();
            result.RegisterType<DuplicatesRowProcessor>().
                As<IPointDataProcessor>().SingleInstance();
            result.RegisterType<OutlierRowProcessor>().
                As<IPointDataProcessor>().SingleInstance();
            result.RegisterType<CorrelationColumnProcessor>().
                As<IPointDataProcessor>().SingleInstance();
            result.RegisterType<LowVariationColumnProcessor>().
                As<IPointDataProcessor>().SingleInstance();

            result.RegisterType<EuclideanDistanceMetric>().
                As<IPointDistanceMetric>().SingleInstance();

            result.RegisterType<ScalerFactory>().As<IFactory<IScaler>>().SingleInstance();

            result.RegisterType<KNearestNeighborsModel>().
                As<IClassificationModel>();
            result.RegisterType<MultipleLinearRegressionModel>().
                As<IRegressionModel>();
            result.RegisterType<KMeanLearningModel>().
                As<IClusteringModel>();

            result.RegisterType<F1ScoreMetric>().
                As<IClassificationScoreMetric>().SingleInstance();
            result.RegisterType<SmapeScoreMetric>().
                As<IRegressionScoreMetric>().SingleInstance();
            result.RegisterType<SilhouetteScoreMetric>().
                As<IDataClusteringScoreMetric>().SingleInstance();

            result.RegisterType<ClassificationCrossValidationEvaluator>().
                As<IClassificationEvaluator>();
            result.RegisterType<RegressionCrossValidationEvaluator>().
                As<IRegressionEvaluator>();
            result.RegisterType<DataClusteringCrossValidationEvaluator>().
                As<IDataClusteringEvaluator>();

            result.RegisterType<DeadlineTaskElementLearningConverter>().SingleInstance();
            result.RegisterType<PlannedRealTaskElementLearningConverter>().SingleInstance();
            result.RegisterType<PlannedTimeTaskElementLearningConverter>().SingleInstance();
            result.RegisterType<ExecutionChanceTaskElementLearningConverter>().SingleInstance();

            result.RegisterType<DeadlineTaskElementEvaluatorLearningController>().
                As<DeadlineTaskElementEvaluatorLearningController>().
                As<IModelTeacher<ITaskElement>>().SingleInstance();
            result.RegisterType<PlannedRealTaskElementEvaluatorLearningController>().
                As<PlannedRealTaskElementEvaluatorLearningController>().
                As<IModelTeacher<ITaskElement>>().SingleInstance();
            result.RegisterType<PlannedTimeTaskElementEvaluatorLearningController>().
                As<PlannedTimeTaskElementEvaluatorLearningController>().
                As<IModelTeacher<ITaskElement>>().SingleInstance();
            result.RegisterType<ExecutionChanceTaskElementEvaluatorLearningController>().
                As<ExecutionChanceTaskElementEvaluatorLearningController>().
                As<IModelTeacher<ITaskElement>>().SingleInstance();

            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            result.RegisterType<FileLogger>().As<ILogger>().SingleInstance();

            result.RegisterType<TimeScheduler>().As<ITimeScheduler>().SingleInstance();

            result.RegisterType<TaskMetadataFactory>().As<IFactory<object>>().SingleInstance();
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

            result.RegisterType<AddTimeIntervalViewModel>().As<AddTimeIntervalViewModel>().
                As<BaseDialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>>().
                SingleInstance();
            result.RegisterType<EditTimeIntervalViewModel>().As<EditTimeIntervalViewModel>().
                As<BaseDialogViewModel<ITimeIntervalElement, bool>>().SingleInstance();
            result.RegisterType<AddTaskViewModel>().As<AddTaskViewModel>().
                As<BaseDialogViewModel<ITask, bool>>().SingleInstance();
            result.RegisterType<RemoveTasksViewModel>().As<RemoveTasksViewModel>().
                As<BaseDialogViewModel<IList<ITask>, bool>>().SingleInstance();
            result.RegisterType<MoveTasksViewModel>().As<MoveTasksViewModel>().
                As<BaseDialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>>().
                SingleInstance();
            result.RegisterType<EditTaskViewModel>().As<EditTaskViewModel>().
                As<BaseDialogViewModel<object, bool>>().SingleInstance();
            result.RegisterType<CopyTasksViewModel>().As<CopyTasksViewModel>().
                As<BaseDialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?>>().
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

            result.RegisterType<DbSession>().As<ISession>().As<IConfigurable>().SingleInstance();
            result.RegisterType<AppSettings>().As<ISettings>().SingleInstance();

            result.RegisterType<EditorViewModel>().As<EditorViewModel>().
                As<BasePageViewModel>().SingleInstance();
            result.RegisterType<TimeViewModel>().As<TimeViewModel>().
                As<BasePageViewModel>().SingleInstance();
            result.RegisterType<StatisticViewModel>().As<StatisticViewModel>().
                As<BasePageViewModel>().SingleInstance();
            result.RegisterType<ToDoListViewModel>().As<ToDoListViewModel>().
                As<BasePageViewModel>().SingleInstance();
            result.RegisterType<SettingsViewModel>().As<SettingsViewModel>().
                As<BasePageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();

            result.RegisterType<AvaloniaResourceService>().As<IResourceService>().
                SingleInstance();
            result.RegisterType<AvaloniaThemeManager>().As<IThemeManager>().
                As<IConfigurable>().SingleInstance();
            result.RegisterType<AvaloniaLocalizationManager>().
                As<ILocalizationManager>().As<IConfigurable>().SingleInstance();

            result.RegisterType<EditorView>().As<IViewFor<EditorViewModel>>();
            result.RegisterType<TimeView>().As<IViewFor<TimeViewModel>>();
            result  .RegisterType<StatisticView>().As<IViewFor<StatisticViewModel>>();
            result.RegisterType<ToDoListView>().As<IViewFor<ToDoListViewModel>>();

            result.RegisterType<AddTaskView>().As<IViewFor<AddTaskViewModel>>();
            result.RegisterType<AddTimeIntervalView>().As<IViewFor<AddTimeIntervalViewModel>>();
            result.RegisterType<CopyTasksView>().As<IViewFor<CopyTasksViewModel>>();
            result.RegisterType<EditTaskView>().As<IViewFor<EditTaskViewModel>>();
            result.RegisterType<EditTimeIntervalView>().As<IViewFor<EditTimeIntervalViewModel>>();
            result.RegisterType<MoveTasksView>().As<IViewFor<MoveTasksViewModel>>();
            result.RegisterType<RemoveTasksView>().As<IViewFor<RemoveTasksViewModel>>();
            result.RegisterType<SettingsView>().As<IViewFor<SettingsViewModel>>();

            result.RegisterType<MainView>().SingleInstance();
            result.RegisterType<MainWindow>().SingleInstance();

            var resolver = result.UseAutofacDependencyResolver();
            result.RegisterInstance(resolver);
            resolver.InitializeReactiveUI();

            return (result, resolver);
        }

        /// <summary>
        /// Создаёт конфигуратор контейнера зависимостей.
        /// </summary>
        /// <param name="builder">Конфигуратор контейнера зависимостей.</param>
        /// <param name="resolver">Разрешатель контейнера зависимостей.</param>
        /// <param name="isSetUpLocator">Флаг настройки локатора.</param>
        /// <returns>Возвращает контейнер зависимостей.</returns>
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
