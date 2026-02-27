using Model.Interfaces;
using Model.Tasks;

using Database.Entities;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений выполнения элементарной задачи
    /// между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{TaskElementExecutionEntity, ITaskElementExecution}"/>.
    /// </remarks>
    public class TaskElementExecutionMapper :
        IMapper<TaskElementExecutionEntity, ITaskElementExecution>
    {
        /// <summary>
        /// Преобразование значений между сущностью временных интервалов и
        /// элементарными временными интервалами.
        /// </summary>
        private readonly IMapper<TimeIntervalEntity, ITimeIntervalElement> _timeIntervalMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementExecutionMapper"/>.
        /// </summary>
        /// <param name="timeIntervalMapper">
        /// Преобразование значений между сущностью временных интервалов и
        /// элементарными временными интервалами.
        /// </param>
        public TaskElementExecutionMapper
            (IMapper<TimeIntervalEntity, ITimeIntervalElement> timeIntervalMapper)
        {
            _timeIntervalMapper = timeIntervalMapper;
        }

        /// <inheritdoc/>
        public ITaskElementExecution Map(TaskElementExecutionEntity value)
        {
            var result = new TaskElementExecution()
            {
                Status = value.Status,
                Progress = value.Progress,
                SpentTime = value.SpentTime,
                ExecutedReal = value.ExecutedReal
            };
            foreach (var timeInterval in value.TimeIntervals)
            {
                var interval = _timeIntervalMapper.Map(timeInterval);
                result.TimeIntervals.Add(interval);
            }
            return result;
        }

        /// <inheritdoc/>
        public TaskElementExecutionEntity MapBack(ITaskElementExecution value)
        {
        }
    }
}
