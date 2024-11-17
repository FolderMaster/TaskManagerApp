using Model.Interfaces;
using Model.Tasks.Times;

namespace ViewModel.ViewModels.Modals
{
    public class TimeIntervalViewModelResult
    {
        public TimeIntervalElement TimeInterval { get; private set; }

        public ITaskElement TaskElement { get; private set; }

        public TimeIntervalViewModelResult(ITaskElement taskElement, TimeIntervalElement timeInterval)
        {
            TaskElement = taskElement;
            TimeInterval = timeInterval;
        }
    }
}
