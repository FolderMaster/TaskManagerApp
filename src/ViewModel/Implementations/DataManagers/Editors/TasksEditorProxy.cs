using Model.Interfaces;

using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers.Editors
{
    /// <summary>
    /// Класс заместитель задач для редактирования.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ITasksEditorProxy"/>.
    /// </remarks>
    public class TasksEditorProxy : ITasksEditorProxy
    {
        /// <summary>
        /// Заменяемый объект.
        /// </summary>
        private ITask _target;

        /// <inheritdoc/>
        public ITask Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        /// <inheritdoc/>
        public ITaskComposite? ParentTask { get; set; }

        /// <inheritdoc/>
        public object? Metadata { get; set; }

        /// <inheritdoc/>
        public int Difficult { get; private set; }

        /// <inheritdoc/>
        public int Priority { get; private set; }

        /// <inheritdoc/>
        public TaskStatus Status { get; private set; }

        /// <inheritdoc/>
        public DateTime? Deadline { get; private set; }

        /// <inheritdoc/>
        public double Progress { get; private set; }

        /// <inheritdoc/>
        public TimeSpan PlannedTime { get; private set; }

        /// <inheritdoc/>
        public TimeSpan SpentTime { get; private set; }

        /// <inheritdoc/>
        public void ApplyChanges()
        {
            Target.ParentTask = ParentTask;
            Target.Metadata = Metadata;
        }

        /// <summary>
        /// Обновляет свойства.
        /// </summary>
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
