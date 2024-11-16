using ReactiveUI.SourceGenerators;

namespace ViewModel.ViewModels.Modals
{
    public partial class EditTaskViewModel : DialogViewModel<object, bool>
    {
        [Reactive]
        private object _item;

        protected override void GetArgs(object args) => Item = args;

        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(true);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(false);
    }
}
