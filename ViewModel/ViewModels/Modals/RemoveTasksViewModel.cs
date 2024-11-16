using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class RemoveTasksViewModel : DialogViewModel<IList<ITask>, bool>
    {
        [Reactive]
        private IList<ITask> _items;

        protected override void GetArgs(IList<ITask> args) => Items = args;

        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
