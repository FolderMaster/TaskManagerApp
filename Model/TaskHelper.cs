namespace Model
{
    public static class TaskHelper
    {
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

        public static bool HasTaskExpired(ITask task, TimeSpan? waningTime = null)
        {
            if (task.Deadline == null)
            {
                return false;
            }
            var dateTime = waningTime != null ? DateTime.Now + waningTime : DateTime.Now;
            return task.Deadline < dateTime;
        }

        public static IEnumerable<ITaskElement> GetTaskElements(IEnumerable<ITask> taskList) =>
            GetTasks(taskList).OfType<ITaskElement>();

        public static IEnumerable<IFullCollection<ITask>> GetTaskComposites
            (IEnumerable<ITask> taskList) => GetTasks(taskList).OfType<IFullCollection<ITask>>();

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
