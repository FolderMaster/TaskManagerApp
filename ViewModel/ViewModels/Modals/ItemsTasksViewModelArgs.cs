using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class ItemsTasksViewModelArgs : TasksViewModelArgs
    {
        public IList<ITask> Items { get; private set; }

        public ItemsTasksViewModelArgs(IList<ITask> items, IList<ITask> list,
            IList<ITask> mainList) : base(list, mainList)
        {
            Items = items;
        }
    }
}
