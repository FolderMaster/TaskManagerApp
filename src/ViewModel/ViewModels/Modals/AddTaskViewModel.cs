using ReactiveUI.SourceGenerators;
using System.ComponentModel;
using System.Reactive.Linq;
using ReactiveUI;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTaskViewModel : DialogViewModel<ITask, bool>
    {
        private readonly IObservable<bool> _canExecuteOk;

        [Reactive]
        private ITask _item;

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

        protected override void GetArgs(ITask args) => Item = args;

        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
