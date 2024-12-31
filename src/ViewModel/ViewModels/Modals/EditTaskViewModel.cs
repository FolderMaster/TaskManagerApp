using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.ComponentModel;
using System.Reactive.Linq;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога изменения задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{object, bool}"/>.
    /// </remarks>
    public partial class EditTaskViewModel : BaseDialogViewModel<object, bool>
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Ok"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteOk;

        /// <summary>
        /// Элемент.
        /// </summary>
        [Reactive]
        private object _item;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="EditTaskViewModel"/> по умолчанию.
        /// </summary>
        public EditTaskViewModel()
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
        protected override void GetArgs(object args) => Item = args;

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
