using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Implementations.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Settings;
using ViewModel.Implementations.DataManagers.Factories;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Technicals;

namespace ViewModel.Tests.AppStates.Settings
{
    [TestFixture(TestOf = typeof(AppSettings), Category = "Integration",
        Description = $"Тестирование класса {nameof(AppSettings)}.")]
    public class AppSettingsTests
    {
        [SetUp]
        public void Setup()
        {
            var mockContainer = ViewModelContainerHelper.GetMockContainer();
            _taskElementFactory = mockContainer.Resolve<IFactory<ITaskElement>>();
            _taskCompositeFactory = mockContainer.Resolve<IFactory<ITaskComposite>>();
            _timeIntervalElementFactory = mockContainer.Resolve<IFactory<ITimeIntervalElement>>();
            _session = (DbSession)mockContainer.Resolve<ISession>();
        }
    }
}
