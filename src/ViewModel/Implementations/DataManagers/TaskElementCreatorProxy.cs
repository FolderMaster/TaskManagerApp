using Model.Interfaces;

using TrackableFeatures;

using ViewModel.Implementations.ModelLearning;
using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers
{
    public class TaskElementCreatorProxy : TrackableObject, ITaskElementProxy
    {
        /// <summary>
        /// Элементарная задача.
        /// </summary>
        private readonly ITaskElement _taskElement;

        /// <summary>
        /// Контроллер обучения модели обучения запланнированных реальных показателей.
        /// </summary>
        private readonly BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
            _plannedRealLearningController;

        /// <summary>
        /// Контроллер обучения модели обучения запланнированного времени.
        /// </summary>
        private readonly BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, TimeSpan>
            _plannedTimeLearningController;

        /// <summary>
        /// Контроллер обучения модели обучения срока.
        /// </summary>
        private readonly BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, DateTime?>
            _deadlineLearningController;

        /// <summary>
        /// Предсказанный срок.
        /// </summary>
        private DateTime? _predictedDeadline;

        /// <summary>
        /// Предсказанное запланированное время.
        /// </summary>
        private TimeSpan _predictedPlannedTime;

        /// <summary>
        /// Предсказанный реальный запланированный показатель.
        /// </summary>
        private double _predictedPlannedReal;

        /// <inheritdoc />
        public int Difficult
        {
            get => _taskElement.Difficult;
            set => UpdateProperty(() => _taskElement.Difficult,
                (value) => _taskElement.Difficult = value, value, UpdatePredictedValues);
        }

        /// <inheritdoc />
        public int Priority
        {
            get => _taskElement.Priority;
            set => UpdateProperty(() => _taskElement.Priority,
                (value) => _taskElement.Priority = value, value, UpdatePredictedValues);
        }

        /// <inheritdoc />
        public TaskStatus Status
        {
            get => _taskElement.Status;
            set => UpdateProperty(() => _taskElement.Status,
                (value) => _taskElement.Status = value, value);
        }

        /// <inheritdoc />
        public DateTime? Deadline
        {
            get => _taskElement.Deadline;
            set => UpdateProperty(() => _taskElement.Deadline,
                (value) => _taskElement.Deadline = value, value);
        }

        /// <inheritdoc />
        public double Progress
        {
            get => _taskElement.Progress;
            set => UpdateProperty(() => _taskElement.Progress,
                (value) => _taskElement.Progress = value, value);
        }

        /// <inheritdoc />
        public TimeSpan PlannedTime
        {
            get => _taskElement.PlannedTime;
            set => UpdateProperty(() => _taskElement.PlannedTime,
                (value) => _taskElement.PlannedTime = value, value);
        }

        /// <inheritdoc />
        public TimeSpan SpentTime
        {
            get => _taskElement.SpentTime;
            set => UpdateProperty(() => _taskElement.SpentTime,
                (value) => _taskElement.SpentTime = value, value);
        }

        /// <inheritdoc />
        public double PlannedReal
        {
            get => _taskElement.PlannedReal;
            set => UpdateProperty(() => _taskElement.PlannedReal,
                (value) => _taskElement.PlannedReal = value, value);
        }

        /// <inheritdoc />
        public double ExecutedReal
        {
            get => _taskElement.ExecutedReal;
            set => UpdateProperty(() => _taskElement.ExecutedReal,
                (value) => _taskElement.ExecutedReal = value, value);
        }

        /// <inheritdoc />
        public object? Metadata
        {
            get => _taskElement.Metadata;
            set => UpdateProperty(() => _taskElement.Metadata,
                (value) => _taskElement.Metadata = value, value);
        }

        /// <inheritdoc />
        public ITimeIntervalList TimeIntervals => _taskElement.TimeIntervals;

        /// <inheritdoc />
        public ITaskComposite? ParentTask
        {
            get => _taskElement.ParentTask;
            set => UpdateProperty(() => _taskElement.ParentTask,
                (value) => _taskElement.ParentTask = value, value);
        }

        /// <inheritdoc />
        public ITaskElement Target => _taskElement;

        /// <inheritdoc />
        public DateTime? PredictedDeadline
        {
            get => _predictedDeadline;
            protected set => UpdateProperty(ref _predictedDeadline, value);
        }

        /// <inheritdoc />
        public TimeSpan PredictedPlannedTime
        {
            get => _predictedPlannedTime;
            protected set => UpdateProperty(ref _predictedPlannedTime, value);
        }

        /// <inheritdoc />
        public double PredictedPlannedReal
        {
            get => _predictedPlannedReal;
            protected set => UpdateProperty(ref _predictedPlannedReal, value);
        }

        /// <inheritdoc />
        public bool IsValidPredictedDeadline => _deadlineLearningController.IsValidModel;

        /// <inheritdoc />
        public bool IsValidPredictedPlannedTime => _plannedTimeLearningController.IsValidModel;

        /// <inheritdoc />
        public bool IsValidPredictedPlannedReal => _plannedRealLearningController.IsValidModel;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementCreatorProxy"/>.
        /// </summary>
        /// <param name="taskElement">Элементарная задача.</param>
        /// <param name="plannedRealLearningController">
        /// Контроллер обучения модели обучения запланнированных реальных показателей.
        /// </param>
        /// <param name="plannedTimeLearningController">
        /// Контроллер обучения модели обучения запланнированного времени.
        /// </param>
        /// <param name="deadlineLearningController">
        /// Контроллер обучения модели обучения срока.
        /// </param>
        public TaskElementCreatorProxy(ITaskElement taskElement,
            BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, double>
            plannedRealLearningController,
            BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, TimeSpan>
            plannedTimeLearningController,
            BaseSupervisedEvaluatorLearningController
            <IEnumerable<double>, double, ITaskElement, ITaskElement, DateTime?>
            deadlineLearningController)
        {
            _taskElement = taskElement;
            _plannedRealLearningController = plannedRealLearningController;
            _plannedTimeLearningController = plannedTimeLearningController;
            _deadlineLearningController = deadlineLearningController;
        }

        /// <summary>
        /// Обновляет предсказанные значения.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="oldValue">Старые значения.</param>
        /// <param name="newValue">Новые значения.</param>
        protected void UpdatePredictedValues<T>(T oldValue, T newValue)
        {
            if (IsValidPredictedDeadline)
            {
                PredictedDeadline = _deadlineLearningController.Predict(Target);
            }
            if (IsValidPredictedPlannedTime)
            {
                PredictedPlannedTime = _plannedTimeLearningController.Predict(Target);
            }
            if (IsValidPredictedPlannedReal)
            {
                PredictedPlannedReal = _plannedRealLearningController.Predict(Target);
            }
        }
    }
}
