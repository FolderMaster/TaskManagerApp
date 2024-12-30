using System.Reactive.Linq;
using System.ComponentModel;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога добавления временного интервала.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{TimeIntervalViewModelArgs,
    /// TimeIntervalViewModelResult}"/>.
    /// </remarks>
    public partial class AddTimeIntervalViewModel : TasksViewModel<TimeIntervalViewModelArgs,
        TimeIntervalViewModelResult>
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Ok"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteOk;

        /// <summary>
        /// Выбранная элементарная задача.
        /// </summary>
        [Reactive]
        private ITaskElement? _selectedTaskElement;

        /// <summary>
        /// Элементарный временной интервал.
        /// </summary>
        [Reactive]
        private ITimeIntervalElement _timeIntervalElement;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="AddTimeIntervalViewModel"/> по умолчанию.
        /// </summary>
        public AddTimeIntervalViewModel()
        {
            var hasErrors = this.WhenAnyValue(x => x.TimeIntervalElement).Select(i =>
            {
                if (i is INotifyDataErrorInfo notify)
                {
                    return Observable.FromEventPattern<DataErrorsChangedEventArgs>
                        (h => notify.ErrorsChanged += h, h => notify.ErrorsChanged -= h).
                        Select(_ => notify.HasErrors).StartWith(notify.HasErrors);
                }
                return Observable.Return(false);
            }).Switch();
            _canExecuteOk = this.WhenAnyValue(x => x.SelectedTaskElement).Select(t => t != null).
                CombineLatest(hasErrors, (t, e) => t && !e);
        }

        /// <inheritdoc/>
        protected override void GetArgs(TimeIntervalViewModelArgs args)
        {
            base.GetArgs(args);
            TimeIntervalElement = args.TimeIntervalElement;
        }

        /// <summary>
        /// Подтверждает действие.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult
            (new TimeIntervalViewModelResult(SelectedTaskElement, TimeIntervalElement));

        /// <summary>
        /// Отменяет действие.
        /// </summary>
        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
