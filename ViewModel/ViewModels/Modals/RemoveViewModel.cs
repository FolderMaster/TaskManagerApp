using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class RemoveViewModel : DialogViewModel
    {
        [Reactive]
        private IList<ITask> _items;

        [Reactive]
        private IList<ITask> _mainList;

        [ReactiveCommand]
        private void Ok()
        {
            foreach (var item in Items)
            {
                if (item.ParentTask == null)
                {
                    _mainList.Remove(item);
                }
                else
                {
                    item.ParentTask.Remove(item);
                }
            }
            _taskSource?.SetResult(null);
        }

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);
    }
}
