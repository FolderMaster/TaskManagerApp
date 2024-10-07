using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class TaskElement : ITaskElement, INotifyPropertyChanged
    {
        private ITaskCollection? _parentTask;

        private int _difficult;

        private int _priority;

        private DateTime? _deadline;

        private TaskStatus _status = TaskStatus.Planned;

        private double _progress;

        private TimeSpan _plannedTime;

        private TimeSpan _spentTime;

        private double _totalReal;

        private double _executedReal;

        public ITaskCollection? ParentTask
        {
            get => _parentTask;
            set
            {
                if (ParentTask != value)
                {
                    _parentTask = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Difficult
        {
            get => _difficult;
            set
            {
                if (Difficult != value)
                {
                    _difficult = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Priority
        {
            get => _priority;
            set
            {
                if (Priority != value)
                {
                    _priority = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? Deadline
        {
            get => _deadline;
            set
            {
                if (Deadline != value)
                {
                    _deadline = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskStatus Status
        {
            get => _status;
            set
            {
                if (Status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Progress
        {
            get => _progress;
            set
            {
                if (Progress != value)
                {
                    _progress = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan PlannedTime
        {
            get => _plannedTime;
            set
            {
                if (PlannedTime != value)
                {
                    _plannedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan SpentTime
        {
            get => _spentTime;
            set
            {
                if (SpentTime != value)
                {
                    _spentTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TotalReal
        {
            get => _totalReal;
            set
            {
                if (TotalReal != value)
                {
                    _totalReal = value;
                    OnPropertyChanged();
                }
            }
        }

        public double ExecutedReal
        {
            get => _executedReal;
            set
            {
                if (ExecutedReal != value)
                {
                    _executedReal = value;
                    OnPropertyChanged();
                }
            }
        }

        public object Metadata { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TaskElement(object metadata) => Metadata = metadata;

        public TaskElement() : this("Task element") { }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
