using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model;

namespace ViewModel.ViewModels.Modals
{
    public partial class MoveTasksViewModel : DialogViewModel
    {
        private readonly IObservable<bool> _canExecuteGoToPrevious;

        private readonly IObservable<bool> _canExecuteGo;

        [Reactive]
        private IList<ITask> _items;

        [Reactive]
        private IList<ITask> _list;

        [Reactive]
        private IList<ITask> _mainList;

        [Reactive]
        private ITask? _selectedTask;

        public MoveTasksViewModel()
        {
            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.List).
                Select(i => List is ITask);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTask).
                Select(i => SelectedTask is ITaskComposite composite && CheckAccessibleToGo(Items, composite));
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)List;
            List = composite.ParentTask ?? MainList;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTask;
            SelectedTask = null;
            List = composite;
        }

        [ReactiveCommand]
        private void Ok()
        {
            foreach (var item in Items)
            {
                if (item.ParentTask != null)
                {
                    item.ParentTask.Remove(item);
                }
                else
                {
                    MainList.Remove(item);
                }
                List.Add(item);
            }
            _taskSource?.SetResult(null);
        }

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
