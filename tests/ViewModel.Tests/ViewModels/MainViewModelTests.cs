using Autofac;

using ViewModel.Technicals;
using ViewModel.ViewModels;

namespace ViewModel.Tests.ViewModels
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(MainViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(MainViewModel)}.")]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<MainViewModel>();
        }
    }
}
