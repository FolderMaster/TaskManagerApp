﻿using Autofac;
using ReactiveUI;

using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Technicals;
using ViewModel.ViewModels;
using Common.Tests;

using CategoryAttribute = Common.Tests.CategoryAttribute;

namespace ViewModel.Tests.ViewModels
{
    [Level(TestLevel.Integration)]
    [Category(TestCategory.Functional)]
    [Severity(SeverityLevel.Critical)]
    [Priority(PriorityLevel.High)]
    [Reproducibility(ReproducibilityType.Stable)]
    [Time(TestTime.Medium)]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture(TestOf = typeof(MainViewModel),
        Description = $"Тестирование класса {nameof(MainViewModel)}.")]
    public class MainViewModelTests
    {
        private static string _dbPath = "MainViewModel_database.db";

        private MainViewModel _viewModel;

        private DbSession _session;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _session = (DbSession)mockContainer.Resolve<ISession>();
            _session.ConnectionString = $"Data Source={_dbPath};Pooling=false";
            _viewModel = mockContainer.Resolve<MainViewModel>();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_dbPath);
        }

        [Test(Description = $"Тестирование метода {nameof(ViewModelActivator.Activate)} " +
            $"свойства {nameof(MainViewModel.Activator)}.")]
        public void Activate_Activator_SessionLoad()
        {
            var result = false;

            _session.ItemsUpdated += (sender, args) =>
            {
                if (args.State == UpdateItemsState.Reset && args.ItemsType == typeof(ITask) &&
                    args.Items.SequenceEqual([]))
                {
                    result = true;
                }
            };
            _viewModel.Activator.Activate();

            Assert.That(result, "Должно отработать событие!");
        }
    }
}
