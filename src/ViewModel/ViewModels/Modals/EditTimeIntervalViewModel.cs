using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;
using System.ComponentModel;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class EditTimeIntervalViewModel : DialogViewModel<ITimeIntervalElement, bool>
    {
        private readonly IObservable<bool> _canExecuteOk;

        public ITimeIntervalElement TimeIntervalElement { get; private set; }

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

        protected override void GetArgs(ITimeIntervalElement args)
        {
            TimeIntervalElement = args;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() =>
            _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(false);
    }
}
