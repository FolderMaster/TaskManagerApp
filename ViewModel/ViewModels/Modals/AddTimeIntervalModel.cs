using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;
using Model.Tasks.Times;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTimeIntervalViewModel : TasksViewModel<TasksViewModelArgs, TimeIntervalViewModelResult>
    {
        private readonly IObservable<bool> _canExecuteOk;

        [Reactive]
        private ITaskElement? _selectedTaskElement;

        [Reactive]
        private DateTime _start = DateTime.Now;

        [Reactive]
        private DateTime _end = DateTime.Now + new TimeSpan(1, 0, 0);

        public AddTimeIntervalViewModel()
        {
            _canExecuteOk = this.WhenAnyValue(x => x.SelectedTaskElement).Select(s => s != null);
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteOk))]
        private void Ok() => _taskSource?.SetResult
            (new TimeIntervalViewModelResult(SelectedTaskElement, new TimeIntervalElement(Start, End)));

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
