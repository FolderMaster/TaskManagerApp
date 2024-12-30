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
        public ITaskElement TaskElement { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalViewModelResult"/>.
        /// </summary>
        /// <param name="taskElement">Элементарная задача.</param>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        public TimeIntervalViewModelResult(ITaskElement taskElement,
            ITimeIntervalElement timeIntervalElement)
        {
            TaskElement = taskElement;
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
