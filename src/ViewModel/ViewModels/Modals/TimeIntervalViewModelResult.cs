using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс результата диалога <see cref="AddTimeIntervalViewModel"/>.
    /// </summary>
    public class TimeIntervalViewModelResult
    {
        /// <summary>
        /// Возвращает элементарный временной интервал.
        /// </summary>
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        /// <summary>
        /// Возвращает элементарную задачу.
        /// </summary>
        public ITaskElementExecution TaskElementExecution { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalViewModelResult"/>.
        /// </summary>
        /// <param name="taskElementExecution">Выполнение элементарной задачи.</param>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        public TimeIntervalViewModelResult(ITaskElementExecution taskElementExecution,
            ITimeIntervalElement timeIntervalElement)
        {
            TaskElementExecution = taskElementExecution;
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
