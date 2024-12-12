using ReactiveUI.SourceGenerators;

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

        }

        protected override void GetArgs(ITask args) => Item = args;

        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
