using Autofac;

using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels;
using ViewModel.Technicals;

namespace View
{
    public static class ContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var result = new ContainerBuilder();
            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            result.RegisterType<MockNotificationManager>().
                As<INotificationManager>().SingleInstance();

            result.RegisterType<Session>().As<Session>().SingleInstance();

            result.Register((c) => new EditorViewModel("Editor", c.Resolve<Session>())).
                As<EditorViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new TimeViewModel("Time", c.Resolve<Session>())).
                As<TimeViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new StatisticViewModel("Statistic", c.Resolve<Session>())).
                As<StatisticViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new ToDoListViewModel("To-do list", c.Resolve<Session>())).
                As<ToDoListViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result.Build();
        }

        public static ContainerBuilder GetContainerBuilder()
        {
            var result = new ContainerBuilder();
            result.RegisterType<FileService>().As<IFileService>().SingleInstance();
            result.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();

            result.RegisterType<Session>().As<Session>().SingleInstance();

            result.Register((c) => new EditorViewModel("Editor", c.Resolve<Session>())).
                As<EditorViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new TimeViewModel("Time", c.Resolve<Session>())).
                As<TimeViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new StatisticViewModel("Statistic", c.Resolve<Session>())).
                As<StatisticViewModel>().As<PageViewModel>().SingleInstance();
            result.Register((c) => new ToDoListViewModel("To-do list", c.Resolve<Session>())).
                As<ToDoListViewModel>().As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result;
        }
    }
}
