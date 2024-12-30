using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    /// <summary>
    /// Класс диалога задач.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDialogViewModel{A, R}"/>.
    /// </remarks>
    public partial class TasksViewModel<A, R> : BaseDialogViewModel<A, R>
        where A : TasksViewModelArgs
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="GoToPrevious"/>.
        /// </summary>
        protected IObservable<bool> _canExecuteGoToPrevious;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Go"/>.
        /// </summary>
        protected IObservable<bool> _canExecuteGo;

        /// <summary>
        /// Основной список.
        /// </summary>
        protected IEnumerable<ITask> _mainList;

        /// <summary>
        /// Элементы.
        /// </summary>
        [Reactive]
        private IList<ITask> _items;

        /// <summary>
        /// Список.
        /// </summary>
        [Reactive]
        private IEnumerable<ITask> _list;

        /// <summary>
        /// Выбранная задача.
        /// </summary>
        [Reactive]
        private ITask? _selectedTask;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TasksViewModel{A, R}"/> по умолчанию.
        /// </summary>
        public TasksViewModel()
        {
            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.List).
                Select(i => List is ITaskComposite);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTask).
                Select(i => SelectedTask is ITaskComposite composite);
        }

        /// <inheritdoc/>
        protected override void GetArgs(A args)
        {
            List = args.List;
            _mainList = args.MainList;
        }

        /// <summary>
        /// Переходит к предыдущей задаче по иерархии.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITaskComposite)List;
            List = composite.ParentTask ?? _mainList;
        }

        /// <summary>
        /// Переходит в выбранную задачу.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTask;
            SelectedTask = null;
            List = composite;
        }
    }
}
