using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTaskViewModel : DialogViewModel<ITask, bool>
    {
        [Reactive]
        private ITask _item;

        protected override void GetArgs(ITask args) => Item = args;

        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
