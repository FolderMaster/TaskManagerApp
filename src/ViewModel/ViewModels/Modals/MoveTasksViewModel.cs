using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога перемещения задач.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{ItemsTasksViewModelArgs, IEnumerable{ITask}?}"/>.
    /// </remarks>
    public partial class MoveTasksViewModel :
        TasksViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="MoveTasksViewModel"/> по умолчанию.
        /// </summary>
        public MoveTasksViewModel()
        {
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTask).
                Select(i => SelectedTask is ITaskComposite composite &&
                CheckAccessibleToGo(Items, composite));
        }

        /// <inheritdoc/>
        protected override void GetArgs(ItemsTasksViewModelArgs args)
        {
            base.GetArgs(args);
            Items = args.Items;
        }

        /// <summary>
        /// Подтверждает действие.
        /// </summary>
        [ReactiveCommand]
        private void Ok() =>
            _taskSource?.SetResult(List);

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);

        /// <summary>
        /// Проверяет, доступна ли выбранная задача для перемещения.
        /// </summary>
        /// <param name="tasks">Задачи.</param>
        /// <param name="selectedTask">Выбранная задача.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если задача доступна для перемещения, иначе <c>false</c>.
        /// </returns>
        private bool CheckAccessibleToGo(IList<ITask> tasks, ITask selectedTask)
        {
            if (selectedTask is not ITaskComposite selectedComposite)
            {
                return false;
            }
            if (tasks.Contains(selectedComposite))
            {
                return false;
            }
            foreach (var task in tasks)
            {
                if (task is ITaskComposite composite && Contains(composite, selectedComposite))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет, содержится ли задача внутри составной задачи.
        /// </summary>
        /// <param name="container">Составная задача.</param>
        /// <param name="element">Элемент.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если элемент содержится внутри контейнера, иначе <c>false</c>.
        /// </returns>
        private bool Contains(ITaskComposite container, ITaskComposite? element)
        {
            while (element != null)
            {
                if (container == element)
                {
                    return true;
                }
                element = element.ParentTask;
            }
            return false;
        }
    }
}
