using Model.Interfaces;

using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers.Editors
{
    public class TaskElementsEditorProxy : ITaskElementsEditorProxy
    {
        public ITaskElement _target;

        public ITaskElement Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        public int Difficult { get; set; }

        public int Priority { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? Deadline { get; set; }

        public double Progress { get; set; }

        public TimeSpan PlannedTime { get; set; }

        public TimeSpan SpentTime { get; set; }

        public double PlannedReal { get; set; }

        public double ExecutedReal { get; set; }

        public ITimeIntervalList TimeIntervals =>
            throw new NotImplementedException();

        public IList<ITask>? ParentTask { get; set; }

        public object? Metadata { get; set; }

        public void ApplyChanges()
        {
            Target.Difficult = Difficult;
            Target.Priority = Priority;
            Target.Status = Status;
            Target.Deadline = Deadline;
            Target.Progress = Progress;
            Target.PlannedTime = PlannedTime;
            Target.SpentTime = SpentTime;
            Target.ExecutedReal = ExecutedReal;
            Target.SpentTime = SpentTime;
            Target.PlannedReal = PlannedReal;
            Target.ExecutedReal = ExecutedReal;
            Target.ExecutedReal = ExecutedReal;
            Target.ParentTask = ParentTask;
            Target.Metadata = Metadata;
        }

        private void UpdateProperties()
        {
            Difficult = Target.Difficult;
            Priority = Target.Priority;
            Status = Target.Status;
            Deadline = Target.Deadline;
            Progress = Target.Progress;
            PlannedTime = Target.PlannedTime;
            SpentTime = Target.SpentTime;
            PlannedReal = Target.PlannedReal;
            ExecutedReal = Target.ExecutedReal;
            ParentTask = Target.ParentTask;
            if (Target.Metadata is ICloneable cloneable)
            {
                Metadata = cloneable.Clone();
            }
        }
    }
}
