using Model.Interfaces;

namespace ViewModel.Technicals
{
    /// <summary>
    /// Класс элемента списка задач для выполнения.
    /// </summary>
    public class ToDoListElement
    {
        /// <summary>
        /// Возвращает элементарную задачу.
        /// </summary>
        public ITaskElement TaskElement { get; private set; }

        /// <summary>
        /// Возвращает шанс выполнения.
        /// </summary>
        public double? ExecutionChance { get; private set; }

        /// <summary>
        /// Возвращает логическое значение, указывающее отстаёт ли задача.
        /// </summary>
        public bool IsLagging { get; private set; }

        /// <summary>
        /// Возвращает логическое значение, указывающее просрочена ли задача.
        /// </summary>
        public bool IsExpired { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ToDoListElement"/>.
        /// </summary>
        /// <param name="taskElement">Элементарная задача.</param>
        /// <param name="executionChance">Шанс выполнения.</param>
        /// <param name="isLagging">Логическое значение, указывающее отстаёт ли задача.</param>
        /// <param name="isExpired">Логическое значение, указывающее просрочена ли задача.</param>
        public ToDoListElement(ITaskElement taskElement,
            double? executionChance, bool isLagging, bool isExpired)
        {
            TaskElement = taskElement;
            ExecutionChance = executionChance;
            IsLagging = isLagging;
            IsExpired = isExpired;
        }
    }
}
