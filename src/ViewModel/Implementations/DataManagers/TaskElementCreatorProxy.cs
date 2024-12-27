using Model.Interfaces;

using TrackableFeatures;

using ViewModel.Implementations.ModelLearning;
using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers
{
    public class TaskElementCreatorProxy : TrackableObject, ITaskElementProxy
    {
        private readonly ITaskElement _taskElement;

        private readonly PlannedRealTaskElementEvaluatorLearningController
            _plannedRealLearningController;

        private readonly PlannedTimeTaskElementEvaluatorLearningController
            _plannedTimeLearningController;

        private readonly DeadlineTaskElementEvaluatorLearningController
            _deadlineLearningController;

        private DateTime? _predictedDeadline;

        private TimeSpan _predictedPlannedTime;

        private double _predictedPlannedReal;

        public int Difficult
        {
            get => _taskElement.Difficult;
            set => UpdateProperty(() => _taskElement.Difficult,
                (value) => _taskElement.Difficult = value, value, UpdatePredictedValues);
        }

        public int Priority
        {
            get => _taskElement.Priority;
            set => UpdateProperty(() => _taskElement.Priority,
                (value) => _taskElement.Priority = value, value, UpdatePredictedValues);
        }

        public TaskStatus Status
        {
            get => _taskElement.Status;
            set => UpdateProperty(() => _taskElement.Status,
                (value) => _taskElement.Status = value, value);
        }

        public DateTime? Deadline
        {
            get => _taskElement.Deadline;
            set => UpdateProperty(() => _taskElement.Deadline,
                (value) => _taskElement.Deadline = value, value);
        }

        public double Progress
        {
            get => _taskElement.Progress;
            set => UpdateProperty(() => _taskElement.Progress,
                (value) => _taskElement.Progress = value, value);
        }

        public TimeSpan PlannedTime
        {
            get => _taskElement.PlannedTime;
            set => UpdateProperty(() => _taskElement.PlannedTime,
                (value) => _taskElement.PlannedTime = value, value);
        }

        public TimeSpan SpentTime
        {
            get => _taskElement.SpentTime;
            set => UpdateProperty(() => _taskElement.SpentTime,
                (value) => _taskElement.SpentTime = value, value);
        }

        public double PlannedReal
        {
            get => _taskElement.PlannedReal;
            set => UpdateProperty(() => _taskElement.PlannedReal,
                (value) => _taskElement.PlannedReal = value, value);
        }

        public double ExecutedReal
        {
            get => _taskElement.ExecutedReal;
            set => UpdateProperty(() => _taskElement.ExecutedReal,
                (value) => _taskElement.ExecutedReal = value, value);
        }

        public object? Metadata
        {
            get => _taskElement.Metadata;
            set => UpdateProperty(() => _taskElement.Metadata,
                (value) => _taskElement.Metadata = value, value);
        }

        public ITimeIntervalList TimeIntervals => _taskElement.TimeIntervals;

        public IList<ITask>? ParentTask
        {
            get => _taskElement.ParentTask;
            set => UpdateProperty(() => _taskElement.ParentTask,
                (value) => _taskElement.ParentTask = value, value);
        }

        public ITaskElement Target => _taskElement;

        public DateTime? PredictedDeadline
        {
            get => _predictedDeadline;
            protected set => UpdateProperty(ref _predictedDeadline, value);
        }

        public TimeSpan PredictedPlannedTime
        {
            get => _predictedPlannedTime;
            protected set => UpdateProperty(ref _predictedPlannedTime, value);
        }

        public double PredictedPlannedReal
        {
            get => _predictedPlannedReal;
            protected set => UpdateProperty(ref _predictedPlannedReal, value);
        }

        public bool IsValidPredictedDeadline => _deadlineLearningController.IsValidModel;

        public bool IsValidPredictedPlannedTime => _plannedTimeLearningController.IsValidModel;

        public bool IsValidPredictedPlannedReal => _plannedRealLearningController.IsValidModel;

        public TaskElementCreatorProxy(ITaskElement taskElement,
            PlannedRealTaskElementEvaluatorLearningController plannedRealLearningController,
            PlannedTimeTaskElementEvaluatorLearningController plannedTimeLearningController,
            DeadlineTaskElementEvaluatorLearningController deadlineLearningController)
        {
            _taskElement = taskElement;
            _plannedRealLearningController = plannedRealLearningController;
            _plannedTimeLearningController = plannedTimeLearningController;
            _deadlineLearningController = deadlineLearningController;
        }

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
