using Model;

namespace ViewModel.ViewModels.Modals
{
    public class TasksViewModelArgs
    {
        public IList<ITask> List { get; private set; }

        public IList<ITask> MainList { get; private set; }

        public TasksViewModelArgs(IList<ITask> list, IList<ITask> mainList)
        {
            List = list;
            MainList = mainList;
        }
    }
}
