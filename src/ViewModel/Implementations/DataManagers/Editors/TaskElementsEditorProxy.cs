using Model.Interfaces;
using ViewModel.Interfaces.DataManagers;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.DataManagers.Editors
{
    /// <summary>
    /// Класс заместитель элементарых задач для редактирования.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ITaskElementsEditorProxy"/>.
    /// </remarks>
    public class TaskElementsEditorProxy : ITaskElementsEditorProxy
    {
        /// <summary>
        /// Заменяемый объект.
        /// </summary>
        private ITaskElement _target;

        /// <inheritdoc/>
        public ITaskElement Target
        {
            get => _target;
            set
            {
                _target = value;
                UpdateProperties();
            }
        }

        /// <inheritdoc/>
        public int Difficult { get; set; }

        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <inheritdoc/>
        public TaskStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTime? Deadline { get; set; }

        /// <inheritdoc/>
        public double Progress { get; set; }

        /// <inheritdoc/>
        public TimeSpan PlannedTime { get; set; }

        /// <inheritdoc/>
        public TimeSpan SpentTime { get; set; }

        /// <inheritdoc/>
        public double PlannedReal { get; set; }

        /// <inheritdoc/>
        public double ExecutedReal { get; set; }

        /// <inheritdoc/>
        public ITimeIntervalList TimeIntervals =>
            throw new NotImplementedException();

        /// <inheritdoc/>
        public IEnumerable<ITaskElementExecution> Executions =>
            throw new NotImplementedException();

        /// <inheritdoc/>
        public ITaskComposite? ParentTask { get; set; }

        /// <inheritdoc/>
        public object? Metadata { get; set; }

        /// <inheritdoc/>
        public void ApplyChanges()
        {
            Target.Difficult = Difficult;
            Target.Priority = Priority;
            Target.Deadline = Deadline;
            Target.PlannedTime = PlannedTime;
            Target.PlannedReal = PlannedReal;
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
            PlannedReal = Target.PlannedReal;
            ParentTask = Target.ParentTask;
            if (Target.Metadata is ICloneable cloneable)
            {
                Metadata = cloneable.Clone();
            }
        }
    }
}
