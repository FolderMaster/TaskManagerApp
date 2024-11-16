using Model;

namespace ViewModel.ViewModels.Modals
{
    public class TimeIntervalViewModelResult
    {
        public TimeInterval TimeInterval { get; private set; }

        public ITaskElement TaskElement { get; private set; }

        public TimeIntervalViewModelResult(ITaskElement taskElement, TimeInterval timeInterval)
        {
            TaskElement = taskElement;
            TimeInterval = timeInterval;
        }
    }
}
