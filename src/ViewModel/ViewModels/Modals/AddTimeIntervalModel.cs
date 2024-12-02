using System.Reactive.Linq;
using System.ComponentModel;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTimeIntervalViewModel : TasksViewModel<TimeIntervalViewModelArgs,
        TimeIntervalViewModelResult>
    {
        private readonly IObservable<bool> _canExecuteOk;

        [Reactive]
        private ITaskElement? _selectedTaskElement;

        [Reactive]
        private ITimeIntervalElement _timeIntervalElement;

        public AddTimeIntervalViewModel()
        {
            var hasErrors = this.WhenAnyValue(x => x.TimeIntervalElement).Select(i =>
            {
                if (i is INotifyDataErrorInfo notify)
                {
                    return Observable.FromEventPattern<DataErrorsChangedEventArgs>
                        (h => notify.ErrorsChanged += h, h => notify.ErrorsChanged -= h).
                        Select(_ => notify.HasErrors);
                }
                return Observable.Return(false);
            }).Switch().DistinctUntilChanged();
            _canExecuteOk = this.WhenAnyValue(x => x.SelectedTaskElement).Select(t => t != null).
                CombineLatest(hasErrors, (t, e) => t && !e).DistinctUntilChanged();
        }

        protected override void GetArgs(TimeIntervalViewModelArgs args)
        {
            base.GetArgs(args);
            TimeIntervalElement = args.TimeIntervalElement;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult
            (new TimeIntervalViewModelResult(SelectedTaskElement, TimeIntervalElement));

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
