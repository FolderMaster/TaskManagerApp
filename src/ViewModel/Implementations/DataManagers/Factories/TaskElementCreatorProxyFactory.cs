using Model.Interfaces;

using ViewModel.Implementations.ModelLearning;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    public class TaskElementCreatorProxyFactory : IFactory<ITaskElementProxy>
    {
        private IFactory<ITaskElement> _factory;

        private PlannedRealTaskElementEvaluatorLearningController _plannedRealLearningController;

        private PlannedTimeTaskElementEvaluatorLearningController _plannedTimeLearningController;

        private DeadlineTaskElementEvaluatorLearningController _deadlineLearningController;

        public TaskElementCreatorProxyFactory(IFactory<ITaskElement> factory,
            PlannedRealTaskElementEvaluatorLearningController plannedRealLearningController,
            PlannedTimeTaskElementEvaluatorLearningController plannedTimeLearningController,
            DeadlineTaskElementEvaluatorLearningController deadlineLearningController)
        {
            _factory = factory;
            _plannedTimeLearningController = plannedTimeLearningController;
            _deadlineLearningController = deadlineLearningController;
            _plannedRealLearningController = plannedRealLearningController;
        }

        public ITaskElementProxy Create() =>
            new TaskElementCreatorProxy(_factory.Create(), _plannedRealLearningController,
                _plannedTimeLearningController, _deadlineLearningController);
    }
}
