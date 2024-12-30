using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога удаления задач.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{IList{ITask}, bool}"/>.
    /// </remarks>
    public partial class RemoveTasksViewModel : BaseDialogViewModel<IList<ITask>, bool>
    {
        /// <summary>
        /// Элементы.
        /// </summary>
        [Reactive]
        private IList<ITask> _items;

        /// <inheritdoc/>
        protected override void GetArgs(IList<ITask> args) => Items = args;

        /// <summary>
        /// Подтверждает действие.
        /// </summary>
        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(true);

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
