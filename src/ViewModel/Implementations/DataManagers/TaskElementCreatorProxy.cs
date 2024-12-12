using Model.Interfaces;
using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers
{
    public class TaskElementCreatorProxy : ITaskElementProxy
    {
        private ITaskElement _taskElement;

        public int Difficult
        {
            get => _taskElement.Difficult;
            set => _taskElement.Difficult = value;
        }

        public int Priority
        {
            get => _taskElement.Priority;
            set => _taskElement.Priority = value;
        }

        public TaskStatus Status
        {
            get => _taskElement.Status;
            set => _taskElement.Status = value;
        }

        public DateTime? Deadline
        {
            get => _taskElement.Deadline;
            set => _taskElement.Deadline = value;
        }

        public double Progress
        {
            get => _taskElement.Progress;
            set => _taskElement.Progress = value;
        }

        public TimeSpan PlannedTime
        {
            get => _taskElement.PlannedTime;
            set => _taskElement.PlannedTime = value;
        }

        public TimeSpan SpentTime
        {
            get => _taskElement.SpentTime;
            set => _taskElement.SpentTime = value;
        }

        public double PlannedReal
        {
            get => _taskElement.PlannedReal;
            set => _taskElement.PlannedReal = value;
        }

        public double ExecutedReal
        {
            get => _taskElement.ExecutedReal;
            set => _taskElement.ExecutedReal = value;
        }

        public object? Metadata
        {
            get => _taskElement.Metadata;
            set => _taskElement.Metadata = value;
        }

        public ITimeIntervalList TimeIntervals => _taskElement.TimeIntervals;

        public IList<ITask>? ParentTask
        {
            get => _taskElement.ParentTask;
            set => _taskElement.ParentTask = value;
        }

        public ITaskElement Target => _taskElement;

        public TaskElementCreatorProxy(ITaskElement taskElement)
        {
            _taskElement = taskElement;
        }
    }
}
