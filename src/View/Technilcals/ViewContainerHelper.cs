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
    /// <summary>
    /// Вспомогательный статичный класс для работы с контейнером зависимостей в контексте
    /// <see cref="View"/>.
    /// </summary>
    public static class ViewContainerHelper
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
            var builder = ViewModelContainerHelper.GetContainerBuilder();

            builder.RegisterType<AvaloniaResourceService>().As<IResourceService>().
                SingleInstance();
            builder.RegisterType<AvaloniaThemeManager>().As<IThemeManager>().
                As<IConfigurable>().SingleInstance();
            builder.RegisterType<AvaloniaLocalizationManager>().
                As<ILocalizationManager>().As<IConfigurable>().SingleInstance();

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

            builder.RegisterType<MainView>().SingleInstance();
            builder.RegisterType<MainWindow>().SingleInstance();

            var resolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(resolver);
            resolver.InitializeReactiveUI();

            return (builder, resolver);
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
