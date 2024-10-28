using Autofac;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Model;
using ViewModel.ViewModels.Pages;
using ViewModel.ViewModels;
using ViewModel;

namespace View
{
    public static class ContainerHelper
    {
        public static IContainer GetMockContainer()
        {
            var result = new ContainerBuilder();
            result.RegisterType<MockNotificationManager>().
                As<INotificationManager>().SingleInstance();
            result.RegisterType<ObservableCollection<ITask>>().
                As<IList<ITask>>().SingleInstance();
            result.Register((c) => new EditorViewModel("Editor",
                c.Resolve<IList<ITask>>())).As<EditorViewModel>().
                As<PageViewModel>().SingleInstance();
            result.Register((c) => new TimeViewModel("Time",
                c.Resolve<IList<ITask>>())).As<TimeViewModel>().
                As<PageViewModel>().SingleInstance();
            result.Register((c) => new StatisticViewModel("Statistic",
                c.Resolve<IList<ITask>>())).As<StatisticViewModel>().
                As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result.Build();
        }

        public static ContainerBuilder GetContainerBuilder()
        {
            var result = new ContainerBuilder();
            result.RegisterType<ObservableCollection<ITask>>().
                As<IList<ITask>>().SingleInstance();
            result.Register((c) => new EditorViewModel("Editor",
                c.Resolve<IList<ITask>>())).As<PageViewModel>().SingleInstance();
            result.Register((c) => new TimeViewModel("Time",
                c.Resolve<IList<ITask>>())).As<PageViewModel>().SingleInstance();
            result.Register((c) => new StatisticViewModel("Statistic",
                c.Resolve<IList<ITask>>())).As<PageViewModel>().SingleInstance();
            result.RegisterType<MainViewModel>().SingleInstance();
            return result;
        }
    }
}
