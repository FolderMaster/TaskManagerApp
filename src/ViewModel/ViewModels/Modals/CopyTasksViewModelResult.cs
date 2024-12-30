using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс результата диалога <see cref="CopyTasksViewModel"/>.
    /// </summary>
    public class CopyTasksViewModelResult
    {
        /// <summary>
        /// Возвращает список.
        /// </summary>
        public IEnumerable<ITask> List { get; private set; }

        /// <summary>
        /// Возвращает количество.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="CopyTasksViewModelResult"/>.
        /// </summary>
        /// <param name="list">Список.</param>
        /// <param name="count">Количество.</param>
        public CopyTasksViewModelResult(IEnumerable<ITask> list, int count)
        {
            List = list;
            Count = count;
        }
    }
}
