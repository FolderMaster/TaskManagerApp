using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class TimeIntervalViewModelArgs : TasksViewModelArgs
    {
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        public TimeIntervalViewModelArgs(IList<ITask> list, IList<ITask> mainList,
            ITimeIntervalElement timeIntervalElement) : base(list, mainList)
        {
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
