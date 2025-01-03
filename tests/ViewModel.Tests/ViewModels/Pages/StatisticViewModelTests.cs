using Autofac;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(StatisticViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(StatisticViewModel)}.")]
    public class StatisticViewModelTests
    {
        private StatisticViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<StatisticViewModel>();
        }
    }
}
