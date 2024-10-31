using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class AddTaskViewModel : DialogViewModel
    {
        [Reactive]
        private ITask _item;

        [Reactive]
        private IList<ITask> _list;

        [ReactiveCommand]
        private void Ok()
        {
            List.Add(Item);
            _taskSource?.SetResult(null);
        }

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
