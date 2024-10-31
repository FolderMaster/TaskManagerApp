using Model;

namespace ViewModel.Technicals
{
    public class CalendarInterval
    {
        public TimeInterval TimeInterval { get; private set; }

        public ITaskElement TaskElement { get; private set; }

        public CalendarInterval(TimeInterval timeInterval, ITaskElement taskElement)
        {
            TimeInterval = timeInterval;
            TaskElement = taskElement;
        }
    }
}
