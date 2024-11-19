using System.Reactive.Linq;
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
            _canExecuteOk = this.WhenAnyValue(x => x.SelectedTaskElement).Select(s => s != null);
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
