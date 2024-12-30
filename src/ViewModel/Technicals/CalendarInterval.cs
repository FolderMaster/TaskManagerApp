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
        /// Возвращает элементарную задачу.
        /// </summary>
        public ITaskElement TaskElement { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="CalendarInterval"/>.
        /// </summary>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="taskElement">Элементарная задача.</param>
        public CalendarInterval(ITimeIntervalElement timeInterval, ITaskElement taskElement)
        {
            TimeInterval = timeInterval;
            TaskElement = taskElement;
        }
    }
}
