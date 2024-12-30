using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс аргументов диалога списка задач.
    /// </summary>
    public class TasksViewModelArgs
    {
        /// <summary>
        /// Возвращает список.
        /// </summary>
        public IEnumerable<ITask> List { get; private set; }

        /// <summary>
        /// Возвращает основной список.
        /// </summary>
        public IEnumerable<ITask> MainList { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TasksViewModelArgs"/>.
        /// </summary>
        /// <param name="list">Список.</param>
        /// <param name="mainList">Основной список.</param>
        public TasksViewModelArgs(IEnumerable<ITask> list, IEnumerable<ITask> mainList)
        {
            List = list;
            MainList = mainList;
        }
    }
}
