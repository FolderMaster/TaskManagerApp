using Model.Interfaces;

namespace Model
{
    /// <summary>
    /// Вспомогательный статичный класс для работы с задачами.
    /// </summary>
    public static class TaskHelper
    {
        /// <summary>
        /// Проверяет, завершена ли задача.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <returns>Возвращает <c>true</c>, если задача завершена, иначе <c>false</c>.</returns>
        /// <exception cref="NotImplementedException">
        /// Выбрасывает, если статус задачи не поддерживается.
        /// </exception>
        public static bool IsTaskCompleted(ITask task)
        {
            switch (task.Status)
            {
                case TaskStatus.Cancelled:
                case TaskStatus.Deferred:
                case TaskStatus.Planned:
                case TaskStatus.Closed:
                    return true;
                case TaskStatus.Blocked:
                case TaskStatus.OnHold:
                case TaskStatus.InProgress:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Проверяет, истек ли срок выполнения задачи.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="waningTime">Дополнительное время.</param>
        /// <returns>Возвращает <c>true</c>, если срок задачи истёк, иначе <c>false</c>.</returns>
        public static bool HasTaskExpired(ITask task, TimeSpan? waningTime = null)
        {
            if (task.Deadline == null)
            {
                return false;
            }
            var dateTime = waningTime != null ? DateTime.Now + waningTime : DateTime.Now;
            return task.Deadline < dateTime;
        }

        /// <summary>
        /// Возвращает элементарные задачи из списка.
        /// </summary>
        /// <param name="taskList">Список задач.</param>
        /// <returns>Возвращает коллекцию элементарных задач.</returns>
        public static IEnumerable<ITaskElement> GetTaskElements(IEnumerable<ITask> taskList) =>
            GetTasks(taskList).OfType<ITaskElement>();

        /// <summary>
        /// Возвращает составные задачи из списка.
        /// </summary>
        /// <param name="taskList">Список задач.</param>
        /// <returns>Возвращает коллекцию составных задач.</returns>
        public static IEnumerable<ITaskComposite> GetTaskComposites
            (IEnumerable<ITask> taskList) => GetTasks(taskList).OfType<ITaskComposite>();

        /// <summary>
        /// Возвращает задачи из списка.
        /// </summary>
        /// <param name="taskList">Список задач.</param>
        /// <returns>Возвращает коллекцию задач.</returns>
        public static IEnumerable<ITask> GetTasks(IEnumerable<ITask> taskList)
        {
            foreach (var task in taskList)
            {
                yield return task;

                if (task is IEnumerable<ITask> sublist)
                {
                    foreach (var subtask in GetTasks(sublist))
                    {
                        yield return subtask;
                    }
                }
            }
        }
    }
}
