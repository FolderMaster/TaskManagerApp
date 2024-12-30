using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога копирования задач.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TasksViewModel{ItemsTasksViewModelArgs, CopyTasksViewModelResult?}"/>.
    /// </remarks>
    public partial class CopyTasksViewModel :
        TasksViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?>
    {
        /// <summary>
        /// Количество.
        /// </summary>
        [Reactive]
        private int _count = 1;

        /// <summary>
        /// Элементы.
        /// </summary>
        [Reactive]
        private IEnumerable<ITask> _items;

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
        private void Ok() => _taskSource?.SetResult(new CopyTasksViewModelResult(List, Count));

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(null);
    }
}
