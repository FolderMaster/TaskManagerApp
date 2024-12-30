using ReactiveUI.SourceGenerators;
using System.ComponentModel;
using System.Reactive.Linq;
using ReactiveUI;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога добавления задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{ITask, bool}"/>.
    /// </remarks>
    public partial class AddTaskViewModel : BaseDialogViewModel<ITask, bool>
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Ok"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteOk;

        /// <summary>
        /// Элемент.
        /// </summary>
        [Reactive]
        private ITask _item;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AddTaskViewModel"/> по умолчанию.
        /// </summary>
        public AddTaskViewModel()
        {
            _canExecuteOk = this.WhenAnyValue(x => x.Item).Select(i =>
            {
                if (i is INotifyDataErrorInfo notify)
                {
                    return Observable.FromEventPattern<DataErrorsChangedEventArgs>
                        (h => notify.ErrorsChanged += h, h => notify.ErrorsChanged -= h).
                        Select(_ => !notify.HasErrors).StartWith(!notify.HasErrors);
                }
                return Observable.Return(true);
            }).Switch();
        }

        /// <inheritdoc/>
        protected override void GetArgs(ITask args) => Item = args;

        /// <summary>
        /// Подтверждает действие.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult(true);

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
