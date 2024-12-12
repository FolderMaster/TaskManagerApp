using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class MoveTasksViewModel : TasksViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?>
    {
        public MoveTasksViewModel()
        {
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTask).
                Select(i => SelectedTask is ITaskComposite composite &&
                CheckAccessibleToGo(Items, composite));
        }

        protected override void GetArgs(ItemsTasksViewModelArgs args)
        {
            base.GetArgs(args);
            Items = args.Items;
        }

        [ReactiveCommand]
        private void Ok() =>
            _taskSource?.SetResult(List);

        [ReactiveCommand]
        private void Cancel() =>
            _taskSource?.SetResult(null);

        private bool CheckAccessibleToGo(IList<ITask> tasks, ITask selectedTask)
        {
            if (selectedTask is not ITaskComposite selectedComposite)
            {
                return false;
            }
            if (tasks.Contains(selectedComposite))
            {
                return false;
            }
            foreach (var task in tasks)
            {
                if (task is ITaskComposite composite && Contains(composite, selectedComposite))
                {
                    return false;
                }
            }
            return true;
        }

        private bool Contains(ITaskComposite container, ITaskComposite? element)
        {
            while (element != null)
            {
                if (container == element)
                {
                    return true;
                }
                element = element.ParentTask as ITaskComposite;
            }
            return false;
        }
    }
}
