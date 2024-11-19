using Model.Interfaces;

namespace ViewModel.ViewModels.Modals
{
    public class TimeIntervalViewModelResult
    {
        public ITimeIntervalElement TimeIntervalElement { get; private set; }

        public ITaskElement TaskElement { get; private set; }

        public TimeIntervalViewModelResult(ITaskElement taskElement,
            ITimeIntervalElement timeIntervalElement)
        {
            TaskElement = taskElement;
            TimeIntervalElement = timeIntervalElement;
        }
    }
}
