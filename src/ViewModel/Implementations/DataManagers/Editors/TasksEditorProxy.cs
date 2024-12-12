using Model.Interfaces;
using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers.Editors
{
    public class TasksEditorProxy : ITasksEditorProxy
    {
        public ITask _target;

        public ITask Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        public IList<ITask>? ParentTask { get; set; }

        public object? Metadata { get; set; }

        public int Difficult { get; private set; }

        public int Priority { get; private set; }

        public TaskStatus Status { get; private set; }

        public DateTime? Deadline { get; private set; }

        public double Progress { get; private set; }

        public TimeSpan PlannedTime { get; private set; }

        public TimeSpan SpentTime { get; private set; }

        public void ApplyChanges()
        {
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
            ParentTask = Target.ParentTask;
            if (Target.Metadata is ICloneable cloneable)
            {
                Metadata = cloneable.Clone();
            }
        }
    }
}
