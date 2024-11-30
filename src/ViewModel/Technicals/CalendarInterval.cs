using Model.Interfaces;

namespace ViewModel.Technicals
{
    public class CalendarInterval
    {
        public ITimeIntervalElement TimeInterval { get; private set; }

        public ITaskElement TaskElement { get; private set; }

        public CalendarInterval(ITimeIntervalElement timeInterval, ITaskElement taskElement)
        {
            TimeInterval = timeInterval;
            TaskElement = taskElement;
        }
    }
}
