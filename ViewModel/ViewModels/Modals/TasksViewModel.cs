using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public partial class TasksViewModel<A, R> : DialogViewModel<A, R>
        where A : TasksViewModelArgs
    {
        protected IObservable<bool> _canExecuteGoToPrevious;

        protected IObservable<bool> _canExecuteGo;

        protected IList<ITask> _mainList;

        [Reactive]
        private IList<ITask> _items;

        [Reactive]
        private IList<ITask> _list;

        [Reactive]
        private ITask? _selectedTask;

        public TasksViewModel()
        {
            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.List).
                Select(i => List is ITask);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTask).
                Select(i => SelectedTask is ITaskComposite composite);
        }

        protected override void GetArgs(A args)
        {
            List = args.List;
            _mainList = args.MainList;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)List;
            List = composite.ParentTask ?? _mainList;
        }

        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTask;
            SelectedTask = null;
            List = composite;
        }
    }
}
