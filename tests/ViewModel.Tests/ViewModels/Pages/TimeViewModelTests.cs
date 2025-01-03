using Autofac;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(TimeViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(TimeViewModel)}.")]
    public class TimeViewModelTests
    {
        private TimeViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<TimeViewModel>();
        }
    }
}
