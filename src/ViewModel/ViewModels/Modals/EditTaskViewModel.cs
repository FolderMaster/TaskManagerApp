using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.ComponentModel;
using System.Reactive.Linq;

namespace ViewModel.ViewModels.Modals
{
    public partial class EditTaskViewModel : DialogViewModel<object, bool>
    {
        private readonly IObservable<bool> _canExecuteOk;

        [Reactive]
        private object _item;

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

        protected override void GetArgs(object args) => Item = args;

        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
