using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;
using System.ComponentModel;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога изменения временного интервала.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{ITimeIntervalElement, bool}"/>.
    /// </remarks>
    public partial class EditTimeIntervalViewModel : BaseDialogViewModel<ITimeIntervalElement, bool>
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Ok"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteOk;

        /// <summary>
        /// Возвращает элементарнай временной интервал.
        /// </summary>
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="EditTimeIntervalViewModel"/> по умолчанию.
        /// </summary>
        public EditTimeIntervalViewModel()
        {
            _canExecuteOk = this.WhenAnyValue(x => x.TimeIntervalElement).Select(i =>
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
        protected override void GetArgs(ITimeIntervalElement args)
        {
            TimeIntervalElement = args;
        }

        /// <summary>
        /// Подтверждает действие.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() =>
            _taskSource?.SetResult(true);

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(false);
    }
}
