using Model.Interfaces;

using ViewModel.Interfaces;
using ViewModel.ViewModels.Modals;
using ViewModel.ViewModels;

namespace ViewModel.AppStates
{
    public class ServicesCollection
    {
        public INotificationManager NotificationManager { get; private set; }

        public IFileService FileService { get; private set; }

        public ISerializer Serializer { get; private set; }

        public IResourceService ResourceService { get; private set; }

        public IFactory<ITaskComposite> TaskCompositeFactory { get; private set; }

        public IFactory<ITaskElement> TaskElementFactory { get; private set; }

        public IFactory<ITimeIntervalElement> TimeIntervalElementFactory { get; private set; }

        public DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult>
            AddTimeIntervalDialog { get; private set; }

        public DialogViewModel<ITask, bool> AddTaskDialog { get; private set; }

        public DialogViewModel<IList<ITask>, bool> RemoveTasksDialog { get; private set; }

        public DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?> MoveTasksDialog
            { get; private set; }

        public DialogViewModel<object, bool> EditTaskDialog { get; private set; }

        public DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?> CopyTasksDialog
            { get; private set; }

        public ServicesCollection(INotificationManager notificationManager,
            IFileService fileService, ISerializer serializer, IResourceService resourceService,
            DialogViewModel<TimeIntervalViewModelArgs, TimeIntervalViewModelResult> addTimeIntervalDialog,
            DialogViewModel<ITask, bool> addTaskDialog,
            DialogViewModel<IList<ITask>, bool> removeTasksDialog,
            DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?> moveTasksDialog,
            DialogViewModel<object, bool> editTaskDialog,
            DialogViewModel<ItemsTasksViewModelArgs, IList<ITask>?> copyTasksDialog,
            IFactory<ITaskComposite> taskCompositeFactory, IFactory<ITaskElement> taskElementFactory,
            IFactory<ITimeIntervalElement> timeIntervalElementFactory)
        {
            NotificationManager = notificationManager;
            FileService = fileService;
            Serializer = serializer;
            ResourceService = resourceService;
            TaskCompositeFactory = taskCompositeFactory;
            TaskElementFactory = taskElementFactory;
            TimeIntervalElementFactory = timeIntervalElementFactory;
            AddTimeIntervalDialog = addTimeIntervalDialog;
            AddTaskDialog = addTaskDialog;
            RemoveTasksDialog = removeTasksDialog;
            CopyTasksDialog = copyTasksDialog;
            EditTaskDialog = editTaskDialog;
            MoveTasksDialog = moveTasksDialog;
        }
    }
}
