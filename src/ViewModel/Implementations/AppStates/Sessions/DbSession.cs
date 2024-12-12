using System.Collections.ObjectModel;

using TrackableFeatures;

using Model.Interfaces;

using ViewModel.Interfaces;
using ViewModel.Interfaces.AppStates.Events;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Sessions.Database.DbContexts;
using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;
using ViewModel.Implementations.AppStates.Sessions.Database.Mappers;
using Model.Tasks;
using Model.Times;

namespace ViewModel.Implementations.AppStates.Sessions
{
    public class DbSession : TrackableObject, ISession
    {
        private readonly IFactory<BaseDbContext> _dbContextFactory;

        private readonly IMapper<TaskEntity, ITask> _taskMapper;

        private readonly IMapper<TimeIntervalEntity, ITimeIntervalElement> _timeIntervalMapper;

        private BaseDbContext? _dbContext;

        private ObservableCollection<ITask> _tasks = new();

        public event EventHandler<ItemsUpdatedEventArgs> ItemsUpdated;

        public IEnumerable<ITask> Tasks
        {
            get => _tasks;
            private set => UpdateProperty(ref _tasks, (ObservableCollection<ITask>)value);
        }

        public DbSession(IFactory<BaseDbContext> contextFactory,
            IMapper<TaskEntity, ITask> taskMapper,
            IMapper<TimeIntervalEntity, ITimeIntervalElement> timeIntervalMapper)
        {
            _dbContextFactory = contextFactory;
            _taskMapper = taskMapper;
            _timeIntervalMapper = timeIntervalMapper;
        }

        public async Task Load()
        {
            _dbContext = _dbContextFactory.Create();
            await _dbContext.Database.EnsureCreatedAsync();
            var taskEntities = _dbContext.Tasks.Where(t => t.ParentTaskId == null);
            var tasks = new ObservableCollection<ITask>();
            foreach (var taskEntity in taskEntities)
            {
                var task = _taskMapper.Map(taskEntity);
                tasks.Add(task);
            }
            Tasks = tasks;
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Reset, Tasks, typeof(ITask)));
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void AddTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask)
        {
            var list = parentTask ?? (IList<ITask>)_tasks;
            foreach (var task in tasks)
            {
                list.Add(task);
                var taskEntity = _taskMapper.MapBack(task);
                _dbContext.Add(taskEntity);
            }
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Add, tasks, typeof(ITask)));
        }

        public void EditTask(ITask task)
        {
            if (task is not IDomain domain)
            {
                throw new ArgumentException(nameof(task));
            }
            var taskEntity = _taskMapper.MapBack(task);
            _dbContext.Entry(taskEntity).CurrentValues.SetValues(taskEntity);
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Edit, [task], task.GetType()));
        }

        public void RemoveTasks(IEnumerable<ITask> tasks)
        {
            foreach (var task in tasks)
            {
                if (task.ParentTask != null)
                {
                    task.ParentTask.Remove(task);
                }
                else
                {
                    _tasks.Remove(task);
                }
                if (task is not IDomain domain)
                {
                    throw new ArgumentException(nameof(tasks));
                }
                var taskEntity = _dbContext.Tasks.
                    Where(t => t == domain.EntityId).FirstOrDefault();
                if (taskEntity != null)
                {
                    _dbContext.Remove(taskEntity);
                }
            }
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Remove, tasks, typeof(ITask)));
        }

        public void MoveTasks(IEnumerable<ITask> tasks, ITaskComposite? parentTask)
        {
            var list = parentTask ?? (IList<ITask>)_tasks;
            foreach (var task in tasks)
            {
                if (task.ParentTask != null)
                {
                    task.ParentTask.Remove(task);
                }
                else
                {
                    _tasks.Remove(task);
                }
                list.Add(task);
            }
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Move, tasks, typeof(ITask)));
        }

        public void AddTimeInterval(ITimeIntervalElement timeIntervalElement,
            ITaskElement taskElement)
        {
            if (taskElement is not TaskElementDomain taskElementDomain)
            {
                throw new ArgumentException(nameof(taskElement));
            }
            taskElement.TimeIntervals.Add(timeIntervalElement);
            var timeIntervalElementEntity = _timeIntervalMapper.MapBack(timeIntervalElement);
            timeIntervalElementEntity.TaskElement = taskElementDomain.Entity;
            _dbContext.Add(timeIntervalElementEntity);
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Add, [timeIntervalElement], timeIntervalElement.GetType()));
        }

        public void EditTimeInterval(ITimeIntervalElement timeIntervalElement)
        {
            var timeIntervalElementEntity = _timeIntervalMapper.MapBack(timeIntervalElement);
            _dbContext.Entry(timeIntervalElementEntity).CurrentValues.SetValues(timeIntervalElementEntity);
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Edit, [timeIntervalElement], timeIntervalElement.GetType()));
        }

        public void RemoveTimeInterval(ITimeIntervalElement timeIntervalElement, ITaskElement taskElement)
        {
            taskElement.TimeIntervals.Remove(timeIntervalElement);
            var timeIntervalElementEntity = _timeIntervalMapper.MapBack(timeIntervalElement);
            _dbContext.Remove(timeIntervalElementEntity);
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Remove, [timeIntervalElement], timeIntervalElement.GetType()));
        }
    }
}
