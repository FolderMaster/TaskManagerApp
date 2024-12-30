using System.Collections.ObjectModel;

using TrackableFeatures;

using Model.Interfaces;

using ViewModel.Interfaces.AppStates;
using ViewModel.Interfaces.AppStates.Sessions;
using ViewModel.Implementations.AppStates.Sessions.Database.DbContexts;
using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;
using ViewModel.Implementations.AppStates.Sessions.Database.Mappers;

namespace ViewModel.Implementations.AppStates.Sessions
{
    /// <summary>
    /// Класс сессии базы данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ISession"/>.
    /// </remarks>
    public class DbSession : TrackableObject, ISession
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private static readonly string _filePath = "database.db";

        /// <summary>
        /// Фабрика, создающая контексты базы данных.
        /// </summary>
        private readonly IDbContextFactory<BaseDbContext> _dbContextFactory;

        /// <summary>
        /// Преобразование значений между сущностью задачи и задачей.
        /// </summary>
        private readonly IMapper<TaskEntity, ITask> _taskMapper;

        /// <summary>
        /// Преобразование значений между сущностью временного интервала и
        /// элементарным временным интервалом.
        /// </summary>
        private readonly IMapper<TimeIntervalEntity, ITimeIntervalElement> _timeIntervalMapper;

        /// <summary>
        /// Путь к сохранению.
        /// </summary>
        private string _savePath;

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private BaseDbContext? _dbContext;

        /// <summary>
        /// Задачи.
        /// </summary>
        private ObservableCollection<ITask> _tasks = new();

        /// <inheritdoc/>
        public string SavePath
        {
            get => _savePath;
            set => UpdateProperty(ref _savePath, value);
        }

        /// <inheritdoc/>
        public IEnumerable<ITask> Tasks
        {
            get => _tasks;
            private set => UpdateProperty(ref _tasks, (ObservableCollection<ITask>)value);
        }

        /// <inheritdoc/>
        public event EventHandler<ItemsUpdatedEventArgs> ItemsUpdated;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DbSession"/>.
        /// </summary>
        /// <param name="contextFactory">Фабрика, создающая контексты базы данных.</param>
        /// <param name="taskMapper">
        /// Преобразование значений между сущностью задачи и задачей.
        /// </param>
        /// <param name="timeIntervalMapper">
        /// Преобразование значений между сущностью временного интервала и
        /// элементарным временным интервалом.
        /// </param>
        /// <param name="fileService">Файловый сервис.</param>
        public DbSession(IDbContextFactory<BaseDbContext> contextFactory,
            IMapper<TaskEntity, ITask> taskMapper,
            IMapper<TimeIntervalEntity, ITimeIntervalElement> timeIntervalMapper,
            IFileService fileService)
        {
            _savePath = $"Data Source={fileService.CombinePath
                (fileService.PersonalDirectoryPath, _filePath)}";
            _dbContextFactory = contextFactory;
            _taskMapper = taskMapper;
            _timeIntervalMapper = timeIntervalMapper;
        }

        /// <inheritdoc/>
        public async Task Load()
        {
            _dbContextFactory.ConnectionString = SavePath;
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

        /// <inheritdoc/>
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
                var taskEntity = _taskMapper.MapBack(task);
                _dbContext.Entry(taskEntity).CurrentValues.SetValues(taskEntity);
            }
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Move, tasks, typeof(ITask)));
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void EditTimeInterval(ITimeIntervalElement timeIntervalElement)
        {
            var timeIntervalElementEntity = _timeIntervalMapper.MapBack(timeIntervalElement);
            _dbContext.Entry(timeIntervalElementEntity).CurrentValues.SetValues(timeIntervalElementEntity);
            ItemsUpdated?.Invoke(this, new ItemsUpdatedEventArgs
                (UpdateItemsState.Edit, [timeIntervalElement], timeIntervalElement.GetType()));
        }

        /// <inheritdoc/>
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
