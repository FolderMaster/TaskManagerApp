using Model.Interfaces;

namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс интервала календаря.
    /// </summary>
    public class CalendarInterval
    {
        /// <summary>
        /// Возвращает временной интервал.
        /// </summary>
        public ITimeIntervalElement TimeInterval { get; private set; }

        /// <summary>
        /// Возвращает выполнение элементарной задачи.
        /// </summary>
        public ITaskElementExecution TaskElementExecution { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="CalendarInterval"/>.
        /// </summary>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="taskElementExecution">Выполнение элементарной задачи.</param>
        public CalendarInterval(ITimeIntervalElement timeInterval,
            ITaskElementExecution taskElementExecution)
        {
            TimeInterval = timeInterval;
            TaskElementExecution = taskElementExecution;
        }
    }
}
