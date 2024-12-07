using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class TimeIntervalViewModelArgs : TasksViewModelArgs
    {
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        public TimeIntervalViewModelArgs(IEnumerable<ITask> list, IEnumerable<ITask> mainList,
            ITimeIntervalElement timeIntervalElement) : base(list, mainList)
        {
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
