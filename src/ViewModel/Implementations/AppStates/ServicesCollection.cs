using Model.Interfaces;

using ViewModel.Interfaces;
using ViewModel.ViewModels.Modals;
using ViewModel.ViewModels;
using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
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
            AddTimeIntervalDialog
        { get; private set; }

        public DialogViewModel<ITask, bool> AddTaskDialog { get; private set; }

        public DialogViewModel<IList<ITask>, bool> RemoveTasksDialog { get; private set; }

        public DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> MoveTasksDialog
        { get; private set; }

        public DialogViewModel<object, bool> EditTaskDialog { get; private set; }

        public DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> CopyTasksDialog
        { get; private set; }

        public DialogViewModel<ITimeIntervalElement, bool> EditTimeIntervalDialog
        { get; private set; }
        
        public ITimeScheduler TimeScheduler { get; private set; }

        //public IEnumerable<PageViewModel> Pages { get; private set; }

        public ServicesCollection(INotificationManager notificationManager,
            IFileService fileService, ISerializer serializer, IResourceService resourceService,
            DialogViewModel<TimeIntervalViewModelArgs,
            TimeIntervalViewModelResult> addTimeIntervalDialog,
            DialogViewModel<ITask, bool> addTaskDialog,
            DialogViewModel<IList<ITask>, bool> removeTasksDialog,
            DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> moveTasksDialog,
            DialogViewModel<object, bool> editTaskDialog,
            DialogViewModel<ItemsTasksViewModelArgs, IEnumerable<ITask>?> copyTasksDialog,
            DialogViewModel<ITimeIntervalElement, bool> editTimeIntervalDialog,
            IFactory<ITaskComposite> taskCompositeFactory,
            IFactory<ITaskElement> taskElementFactory,
            IFactory<ITimeIntervalElement> timeIntervalElementFactory, ITimeScheduler timeScheduler
            /**, IEnumerable<PageViewModel> pages**/)
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
            EditTimeIntervalDialog = editTimeIntervalDialog;
            TimeScheduler = timeScheduler;
            //Pages = pages;
        }
    }
}
