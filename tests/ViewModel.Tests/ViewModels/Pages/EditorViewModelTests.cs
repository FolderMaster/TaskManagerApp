using Autofac;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(EditorViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(EditorViewModel)}.")]
    public class EditorViewModelTests
    {
        private EditorViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<EditorViewModel>();
        }
    }
}
