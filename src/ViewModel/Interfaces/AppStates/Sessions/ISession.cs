using Model.Interfaces;

namespace ViewModel.Interfaces.AppStates.Sessions
{
    /// <summary>
    /// Интерфейс сессии для хранения и изменения данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IStorageService"/>.
    /// </remarks>
    public interface ISession : IStorageService
    {
        /// <summary>
        /// Возвращает и задаёт путь сохранения.
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// Возвращает задачи.
        /// </summary>
        public IEnumerable<ITask> Tasks { get; }

        /// <summary>
        /// Событие, которое возникает при обновлении данных.
        /// </summary>
        public event EventHandler<ItemsUpdatedEventArgs> ItemsUpdated;

        /// <summary>
        /// Добавляет задачи.
        /// </summary>
        /// <param name="tasks">Задачи.</param>
        /// <param name="parentTask">Родительская задача.</param>
        public void AddTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask);

        /// <summary>
        /// Изменяет задачу.
        /// </summary>
        /// <param name="task">Задача.</param>
        public void EditTask(ITask task);

        /// <summary>
        /// Удаляет задачи.
        /// </summary>
        /// <param name="tasks">Задачи.</param>
        public void RemoveTasks(IEnumerable<ITask> tasks);

        /// <summary>
        /// Перемещает задачи.
        /// </summary>
        /// <param name="tasks">Задачи.</param>
        /// <param name="parentTask">Родительская задача.</param>
        public void MoveTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask);

        /// <summary>
        /// Добавляет временной интервал.
        /// </summary>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        /// <param name="taskElement">Элементарная задача.</param>
        public void AddTimeInterval(ITimeIntervalElement timeIntervalElement,
            ITaskElement taskElement);

        /// <summary>
        /// Изменяет временной интервал.
        /// </summary>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        public void EditTimeInterval(ITimeIntervalElement timeIntervalElement);

        /// <summary>
        /// Удаляет временной интервал.
        /// </summary>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        /// <param name="taskElement">Элементарная задача.</param>
        public void RemoveTimeInterval(ITimeIntervalElement timeIntervalElement,
            ITaskElement taskElement);
    }
}
