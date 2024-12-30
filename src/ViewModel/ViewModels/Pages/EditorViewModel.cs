using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

using Model.Interfaces;

using ViewModel.ViewModels.Modals;
using ViewModel.Interfaces.DataManagers;
using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.AppStates.Sessions;

using IResourceService = ViewModel.Interfaces.AppStates.IResourceService;

namespace ViewModel.ViewModels.Pages
{
    /// <summary>
    /// Класс контроллера страницы изменения.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BasePageViewModel"/>.
    /// </remarks>
    public partial class EditorViewModel : BasePageViewModel
    {
        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="GoToPrevious"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteGoToPrevious;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Go"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteGo;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Remove"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteRemove;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Add"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteAdd;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Edit"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteEdit;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Copy"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteCopy;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Move"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteMove;

        /// <summary>
        /// Наблюдатель, который отслеживает возможность выполнения <see cref="Swap"/>.
        /// </summary>
        private readonly IObservable<bool> _canExecuteSwap;

        /// <summary>
        /// Сессия.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// Диалог удаления задач.
        /// </summary>
        private BaseDialogViewModel<IList<ITask>, bool> _removeTasksDialog;

        /// <summary>
        /// Диалог добавления задач.
        /// </summary>
        private BaseDialogViewModel<ITask, bool> _addTasksDialog;

        /// <summary>
        /// Диалог изменения задачи.
        /// </summary>
        private BaseDialogViewModel<object, bool> _editTaskDialog;

        /// <summary>
        /// Диалог перемещения задач.
        /// </summary>
        private BaseDialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> _moveTasksDialog;

        /// <summary>
        /// Диалог копирования задач.
        /// </summary>
        private BaseDialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?>
            _copyTasksDialog;

        /// <summary>
        /// Фабрика, создающая составную задачу.
        /// </summary>
        private IFactory<ITaskComposite> _taskCompositeFactory;

        /// <summary>
        /// Фабрика, создающая элементарную задачу.
        /// </summary>
        private IFactory<ITaskElementProxy> _taskElementProxyFactory;

        /// <summary>
        /// Заместитель задач для редактирования.
        /// </summary>
        private ITasksEditorProxy _tasksEditorProxy;

        /// <summary>
        /// Заместитель элементарных задач для редактирования.
        /// </summary>
        private ITaskElementsEditorProxy _taskElementsEditorProxy;

        /// <summary>
        /// Список задач для отображения.
        /// </summary>
        [Reactive]
        private IEnumerable<ITask> _taskListView;

        /// <summary>
        /// Список выбранных задач.
        /// </summary>
        [Reactive]
        private IList<ITask> _selectedTasks = new ObservableCollection<ITask>();

        /// <summary>
        /// Создаёт экземпляр класса <see cref="EditorViewModel"/>.
        /// </summary>
        /// <param name="session">Сессия.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="removeTasksDialog">Диалог удаления задач.</param>
        /// <param name="addTasksDialog">Диалог добавления задач.</param>
        /// <param name="editTaskDialog">Диалог изменения задачи.</param>
        /// <param name="moveTasksDialog">Диалог перемещения задач.</param>
        /// <param name="copyTasksDialog">Диалог копирования задач.</param>
        /// <param name="taskCompositeFactory">Фабрика, создающая составную задачу.</param>
        /// <param name="taskElementProxyFactory">Фабрика, создающая элементарную задачу.</param>
        /// <param name="tasksEditorProxy">Заместитель задач для редактирования.</param>
        /// <param name="taskElementsEditorProxy">
        /// Заместитель элементарных задач для редактирования.
        /// </param>
        public EditorViewModel(ISession session, IResourceService resourceService,
            BaseDialogViewModel<IList<ITask>, bool> removeTasksDialog,
            BaseDialogViewModel<ITask, bool> addTasksDialog,
            BaseDialogViewModel<object, bool> editTaskDialog,
            BaseDialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> moveTasksDialog,
            BaseDialogViewModel<ItemsTasksViewModelArgs, CopyTasksViewModelResult?> copyTasksDialog,
            IFactory<ITaskComposite> taskCompositeFactory,
            IFactory<ITaskElementProxy> taskElementProxyFactory,
            ITasksEditorProxy tasksEditorProxy,
            ITaskElementsEditorProxy taskElementsEditorProxy)
        {
            _session = session;
            _removeTasksDialog = removeTasksDialog;
            _addTasksDialog = addTasksDialog;
            _editTaskDialog = editTaskDialog;
            _moveTasksDialog = moveTasksDialog;
            _copyTasksDialog = copyTasksDialog;
            _taskCompositeFactory = taskCompositeFactory;
            _taskElementProxyFactory = taskElementProxyFactory;
            _tasksEditorProxy = tasksEditorProxy;
            _taskElementsEditorProxy = taskElementsEditorProxy;

            TaskListView = _session.Tasks;
            this.WhenAnyValue(x => x._session.Tasks).Subscribe(t => TaskListView = t);

            _canExecuteGoToPrevious = this.WhenAnyValue(x => x.TaskListView).
                Select(i => TaskListView is ITask).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteGo = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 1 && SelectedTasks.First() is ITaskComposite).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteRemove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteAdd = this.WhenAnyValue(x => x.SelectedTasks.Count).
                Select(i => i == 0 || (i == 1 && SelectedTasks.First() is ITaskComposite)).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteEdit = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 1).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteCopy = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteMove = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i > 0).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);
            _canExecuteSwap = this.WhenAnyValue(x => x.SelectedTasks.Count).Select(i => i == 2 &&
                SelectedTasks.First().ParentTask == SelectedTasks.Last().ParentTask).
                CombineLatest(_modalsObservable, (r1, r2) => r1 && r2);

            Metadata = resourceService.GetResource("EditorPageMetadata");
        }

        /// <summary>
        /// Переходит к предыдущей задаче по иерархии.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteGoToPrevious))]
        private void GoToPrevious()
        {
            var composite = (ITask)TaskListView;
            TaskListView = composite.ParentTask ?? _session.Tasks;
        }

        /// <summary>
        /// Переходит в выбранную задачу.
        /// </summary>
        [ReactiveCommand(CanExecute = nameof(_canExecuteGo))]
        private void Go()
        {
            var composite = (ITaskComposite)SelectedTasks.First();
            SelectedTasks.Clear();
            TaskListView = composite;
        }

        /// <summary>
        /// Удаляет выбранные задачи.
        /// </summary>
        /// <returns>Возвращет задачу процесса удаления.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteRemove))]
        private async Task Remove()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var result = await AddModal(_removeTasksDialog, items);
            if (result)
            {
                _session.RemoveTasks(items);
            }
        }

        /// <summary>
        /// Добавляет элементарную задачу.
        /// </summary>
        /// <returns>Возвращет задачу процесса добавления.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskElement() => await Add(_taskElementProxyFactory.Create());

        /// <summary>
        /// Добавляет составную задачу.
        /// </summary>
        /// <returns>Возвращет задачу процесса добавления.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteAdd))]
        private async Task AddTaskComposite() => await Add(_taskCompositeFactory.Create());

        /// <summary>
        /// Добавляет задачу.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <returns>Возвращет задачу процесса добавления.</returns>
        private async Task Add(ITask task)
        {
            var taskComposite = TaskListView as ITaskComposite;
            if (SelectedTasks.Count == 1)
            {
                taskComposite = (ITaskComposite)SelectedTasks.First();
                SelectedTasks.Clear();
            }
            var result = await AddModal(_addTasksDialog, task);
            if (result)
            {
                _session.AddTasks([task is IProxy<ITask> proxy ? proxy.Target : task],
                    taskComposite);
            }
        }

        /// <summary>
        /// Изменяет выбранную задачу.
        /// </summary>
        /// <returns>Возвращет задачу процесса изменения.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteEdit))]
        private async Task Edit()
        {
            var item = SelectedTasks.First();
            SelectedTasks.Clear();
            var editorService = (IEditorService)null;
            if (item is ITaskElement taskElement)
            {
                var taskElementsEditorService = _taskElementsEditorProxy;
                taskElementsEditorService.Target = taskElement;
                editorService = taskElementsEditorService;
            }
            else
            {
                var tasksEditorService = _tasksEditorProxy;
                tasksEditorService.Target = item;
                editorService = tasksEditorService;
            }
            var result = await AddModal(_editTaskDialog, editorService);
            if (result)
            {
                editorService.ApplyChanges();
                _session.EditTask(item);
            }
        }

        /// <summary>
        /// Перемещает выбранные задачи.
        /// </summary>
        /// <returns>Возвращет задачу процесса перестановки.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteMove))]
        private async Task Move()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var list = await AddModal(_moveTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _session.Tasks));
            if (list != null)
            {
                _session.MoveTasks(items, (ITaskComposite?)list);
            }
        }

        /// <summary>
        /// Копирует выбранные задачи.
        /// </summary>
        /// <returns>Возвращет задачу процесса копирования.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteCopy))]
        private async Task Copy()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();
            var result = await AddModal(_copyTasksDialog,
                new ItemsTasksViewModelArgs(items, TaskListView, _session.Tasks));
            if (result != null)
            {
                var copyList = new List<ITask>();
                foreach (var task in items)
                {
                    if (task is ICloneable cloneable)
                    {
                        for (var i = 0; i < result.Count; ++i)
                        {
                            copyList.Add((ITask)cloneable.Clone());
                        }
                    }
                }
                _session.AddTasks(copyList, result.List as ITaskComposite);
            }
        }

        /// <summary>
        /// Переставляет местами выбранные задачи.
        /// </summary>
        /// <returns>Возвращет задачу процесса перестановки.</returns>
        [ReactiveCommand(CanExecute = nameof(_canExecuteSwap))]
        private void Swap()
        {
            var items = SelectedTasks.ToList();
            SelectedTasks.Clear();

            var item1 = items.First();
            var item2 = items.Last();

            var list = item1.ParentTask ?? _session.Tasks as IList<ITask>;

            var index1 = list.IndexOf(item1);
            var index2 = list.IndexOf(item2);

            list[index1] = item2;
            list[index2] = item1;
        }
    }
}
