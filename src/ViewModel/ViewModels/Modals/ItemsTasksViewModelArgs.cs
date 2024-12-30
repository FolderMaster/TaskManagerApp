using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс аргументов диалога элементов списка.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TasksViewModelArgs"/>.
    /// </remarks>
    public class ItemsTasksViewModelArgs : TasksViewModelArgs
    {
        /// <summary>
        /// Возвращает элементы.
        /// </summary>
        public IList<ITask> Items { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ItemsTasksViewModelArgs"/>.
        /// </summary>
        /// <param name="items">Элеметоы.</param>
        /// <param name="list">Список.</param>
        /// <param name="mainList">Основной список.</param>
        public ItemsTasksViewModelArgs(IList<ITask> items, IEnumerable<ITask> list,
            IEnumerable<ITask> mainList) : base(list, mainList)
        {
            Items = items;
        }
    }
}
