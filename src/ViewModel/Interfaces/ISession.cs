using Model.Interfaces;
using ViewModel.Interfaces.Events;

namespace ViewModel.Interfaces
{
    public interface ISession : IStorageService
    {
        public IEnumerable<ITask> Tasks { get; }

        public event EventHandler<ItemsUpdatedEventArgs> ItemsUpdated;

        public void AddTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask);

        public void EditTask(ITask task);

        public void RemoveTasks(IEnumerable<ITask> tasks);

        public void MoveTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask);

        public void AddTimeInterval(ITimeIntervalElement timeIntervalElement,
            ITaskElement taskElement);

        public void EditTimeInterval(ITimeIntervalElement timeIntervalElement);

        public void RemoveTimeInterval(ITimeIntervalElement timeIntervalElement,
            ITaskElement taskElement);
    }
}
