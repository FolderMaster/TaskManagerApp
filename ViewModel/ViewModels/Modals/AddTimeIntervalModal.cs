using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model;
using ViewModel.Technicals;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTimeIntervalViewModel : DialogViewModel
    {
        private readonly IObservable<bool> _canExecuteOk;

        [Reactive]
        private IList<ITask> _mainList;

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
        private void Ok()
        {
            var timeInterval = new TimeInterval(Start, End);
            SelectedTaskElement.TimeIntervals.Add(timeInterval);
            _taskSource?.SetResult(new CalendarInterval(timeInterval, SelectedTaskElement));
        }

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
