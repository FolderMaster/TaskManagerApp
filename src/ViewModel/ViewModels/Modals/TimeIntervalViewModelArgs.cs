using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс аргументов диалога <see cref="AddTimeIntervalViewModel"/>.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TasksViewModelArgs"/>.
    /// </remarks>
    public class TimeIntervalViewModelArgs : TasksViewModelArgs
    {
        /// <summary>
        /// Возвращает элементарный временной интервал.
        /// </summary>
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TimeIntervalViewModelResult"/>.
        /// </summary>
        /// <param name="list">Список.</param>
        /// <param name="mainList">Основной список.</param>
        /// <param name="timeIntervalElement">Элементарный временной интервал.</param>
        public TimeIntervalViewModelArgs(IEnumerable<ITask> list, IEnumerable<ITask> mainList,
            ITimeIntervalElement timeIntervalElement) : base(list, mainList)
        {
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
