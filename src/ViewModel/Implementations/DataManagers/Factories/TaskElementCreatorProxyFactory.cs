using Model.Interfaces;

using ViewModel.Implementations.ModelLearning;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    /// <summary>
    /// Класс фабрики, создающая заместителей элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFactory{ITaskElementProxy}"/>.
    /// </remarks>
    public class TaskElementCreatorProxyFactory : IFactory<ITaskElementProxy>
    {
        /// <summary>
        /// Фабрика, создающая элементарные задачи.
        /// </summary>
        private IFactory<ITaskElement> _factory;

        /// <summary>
        /// Контроллер обучения модели обучения запланнированных реальных показателей.
        /// </summary>
        private PlannedRealTaskElementEvaluatorLearningController _plannedRealLearningController;

        /// <summary>
        /// Контроллер обучения модели обучения запланнированного времени.
        /// </summary>
        private PlannedTimeTaskElementEvaluatorLearningController _plannedTimeLearningController;

        /// <summary>
        /// Контроллер обучения модели обучения срока.
        /// </summary>
        private DeadlineTaskElementEvaluatorLearningController _deadlineLearningController;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementCreatorProxyFactory"/>.
        /// </summary>
        /// <param name="factory">Фабрика, создающая элементарные задачи.</param>
        /// <param name="plannedRealLearningController">
        /// Контроллер обучения модели обучения запланнированных реальных показателей.
        /// </param>
        /// <param name="plannedTimeLearningController">
        /// Контроллер обучения модели обучения запланнированного времени.
        /// </param>
        /// <param name="deadlineLearningController">
        /// Контроллер обучения модели обучения срока.
        /// </param>
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

        /// <inheritdoc/>
        public ITaskElementProxy Create() =>
            new TaskElementCreatorProxy(_factory.Create(), _plannedRealLearningController,
                _plannedTimeLearningController, _deadlineLearningController);
    }
}
