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
    }
}
