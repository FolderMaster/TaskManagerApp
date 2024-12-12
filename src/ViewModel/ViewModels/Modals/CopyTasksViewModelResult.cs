using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class CopyTasksViewModelResult
    {
        public IEnumerable<ITask> List { get; private set; }

        public int Count { get; private set; }

        public CopyTasksViewModelResult(IEnumerable<ITask> list, int count)
        {
            List = list;
            Count = count;
        }
    }
}
