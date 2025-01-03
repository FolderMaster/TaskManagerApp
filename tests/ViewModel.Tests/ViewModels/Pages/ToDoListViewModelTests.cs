using Autofac;

using ViewModel.Technicals;
using ViewModel.ViewModels.Pages;

namespace ViewModel.Tests.ViewModels.Pages
{
    [NonParallelizable]
    [TestFixture(TestOf = typeof(ToDoListViewModel), Category = "Integration",
        Description = $"Тестирование класса {nameof(ToDoListViewModel)}.")]
    public class ToDoListViewModelTests
    {
        private ToDoListViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _viewModel = mockContainer.Resolve<ToDoListViewModel>();
        }
    }
}
