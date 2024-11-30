using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class CopyTasksViewModel : TasksViewModel<ItemsTasksViewModelArgs, IList<ITask>?>
    {
        [Reactive]
        private IList<ITask> _items;

        protected override void GetArgs(ItemsTasksViewModelArgs args)
        {
            base.GetArgs(args);
            Items = args.Items;
        }

        [ReactiveCommand]
        private void Ok() => _taskSource?.SetResult(List);

        [ReactiveCommand]
        private void Cancel() => _taskSource?.SetResult(null);
    }
}
