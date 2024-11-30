using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.Editors
{
    public class TaskElementEditorPrototype : TaskEditorPrototype
    {
        public int Difficult { get; set; }

        public int Priority { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? Deadline { get; set; }

        public double TotalReal { get; set; }

        public double ExecutedReal { get; set; }

        public double Progress { get; set; }

        public TimeSpan PlannedTime { get; set; }

        public TimeSpan SpentTime { get; set; }
    }
}
