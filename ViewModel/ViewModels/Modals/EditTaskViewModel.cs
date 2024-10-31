using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class EditTaskViewModel : DialogViewModel
    {
        [Reactive]
        private ITask _item;

        [ReactiveCommand]
        private void Ok()
        {
            _taskSource?.SetResult(null);
        }

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
