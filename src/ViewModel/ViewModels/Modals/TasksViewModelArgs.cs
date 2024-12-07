using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class TasksViewModelArgs
    {
        public IEnumerable<ITask> List { get; private set; }

        public IEnumerable<ITask> MainList { get; private set; }

        public TasksViewModelArgs(IEnumerable<ITask> list, IEnumerable<ITask> mainList)
        {
            List = list;
            MainList = mainList;
        }
    }
}
